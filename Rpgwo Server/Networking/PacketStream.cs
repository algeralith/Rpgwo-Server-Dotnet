using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking.Packets;

namespace Rpgwo_Server.Networking
{
    public class PacketStream
    {
        private NetClient _netClient = null;

        private Queue<Packet> _queuedPackets = new Queue<Packet>();

        private Packet _workingPacket = null;

        private byte[] _buffer = new byte[32 * 1024]; // Maximum packet size in RPGWO seems to be about 21k.
        private int _bufferHead = 0;

        public PacketStream(NetClient netClient)
        {
            _netClient = netClient;
        }

        public void AddBytes(byte[] buffer, int length)
        {
            // Ensure that we have enough buffer space.
            // This should never happen. Buffer is by default bigger than the maximum packet size in rpgwo.
            if (_bufferHead + length >= _buffer.Length)
            {
                Console.WriteLine("Buffer not big enough for packet stream. Considering increasing."); // TODO :: Logging

                // Double buffer in size.
                var newBuffer = new byte[_buffer.Length * 2];
                Array.Copy(_buffer, newBuffer, _buffer.Length);
                _buffer = newBuffer;
            }

            // Copy new data over to the buffer.
            Buffer.BlockCopy(buffer, 0, _buffer, _bufferHead, length);

            _bufferHead += length;

            // Try to parse out packets from the stream.
            Process();
        }

        private void Process()
        {
            while (_bufferHead > 0)
            {
                // Set up the packet we are currently building if it does not exist.
                if (_workingPacket == null)
                {
                    var packetHandler = PacketHandlers.Packets[_buffer[0]];

                    if (packetHandler == null)
                    {
                        Console.WriteLine("Unknown Packet Type."); // TODO :: Logging and disconnect client.
                    }

                    _workingPacket = (Packet)Activator.CreateInstance(packetHandler.PacketType);
                }

                // Figure out if we are expecting any security bytes.
                var securitySize = 0;

                switch (_netClient.ReceiveMode)
                {
                    case PacketSecurity.None:
                        break;
                    case PacketSecurity.Checksum:
                        securitySize += 1;
                        break;
                    case PacketSecurity.ChecksumRnd:
                        securitySize += 2;
                        break;
                }

                var totalSize = _workingPacket.Remaining() + securitySize;

                // Does the buffer contain the packet?
                // Packet Size + Security.
                if (_bufferHead >= totalSize) // TODO :: This. Make sure its right. Just changed from > to >= in a moment of tiredness.
                {
                    // We have the entire packet. Let's build it.
                    if (_workingPacket.HasID)
                        _workingPacket.AddBytes(_buffer, 1, _workingPacket.Size);
                    else
                        _workingPacket.AddBytes(_buffer, 0, _workingPacket.Size);

                    // Add any security bytes. TODO ::
                    switch (_netClient.ReceiveMode)
                    {
                        case PacketSecurity.None:
                            break;
                        case PacketSecurity.Checksum:
                            break;
                        case PacketSecurity.ChecksumRnd:
                            break;
                    }

                    // Populate the Packet with it's data.
                    bool packetedComplete = _workingPacket.Receive();

                    if (packetedComplete)
                    {
                        // Shift Buffer.
                        int bufRemaining;
                        if (_workingPacket.HasID)
                            bufRemaining = _bufferHead - (totalSize + 1); // Need to add back the header byte.
                        else
                            bufRemaining = _bufferHead - totalSize;

                        Buffer.BlockCopy(_buffer, (_bufferHead - bufRemaining), _buffer, 0, bufRemaining);

                        // Update new buffer head.
                        _bufferHead = bufRemaining;


                        if (_workingPacket.IsMultiPart)
                            _workingPacket = _workingPacket.NextSubPacket();
                        else
                        {
                            // Packet should be complete by this point. Enqueue.
                            if (_workingPacket.Parent == null)
                                AddPacket(_workingPacket);
                            else
                                AddPacket(_workingPacket.Parent);

                            _workingPacket = null;
                        }
                    }
                }
                else
                {
                    // Not enough data to continue building packets.
                    break;
                }
            }
        }

        private void AddPacket(Packet packet)
        {
            lock(_queuedPackets)
            {
                _queuedPackets.Enqueue(packet);
            }
        }

        public Packet NextPacket()
        {
            lock(_queuedPackets)
            {
                Packet packet = null;

                var success =_queuedPackets.TryDequeue(out packet);

                if (success)
                {
                    return packet;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
