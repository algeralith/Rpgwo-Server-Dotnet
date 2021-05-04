using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using Rpgwo_Server.Networking.Packets;
using Rpgwo_Server.Accounts;
using Rpgwo_Server.Mob;

namespace Rpgwo_Server.Networking
{
    public partial class NetClient : IDisposable
    {
        private static readonly List<NetClient> _Clients = new List<NetClient>();
        public static NetClient[] Clients => _Clients.ToArray();

        private Socket _socket;
        private byte[] _buffer = new byte[8096];

        private AsyncCallback _OnReceive;
        private AsyncCallback _OnSend;

        private NetState _NetState = NetState.None;
        public NetState NetState => _NetState;

        // Security Modes for sending and receiving.
        // First packet from the client has no security.
        private PacketSecurity _receiveMode = PacketSecurity.None;
        public PacketSecurity ReceiveMode => _receiveMode;

        // Server will always send a packet with a checksum.
        private PacketSecurity _sendMode = PacketSecurity.Checksum;
        public PacketSecurity SendMode => _sendMode;

        // Random Byte Security.
        private RSecurity _rSecurity;

        private PacketStream _packetStream;

        // Send Queue.
        private Queue<Packet> _sendQueue = new Queue<Packet>();

        // Sending locks.
        private bool _sending = false;
        private object _sendLock = new object();

        // Game Account.
        public Account Account { get; set; }

        // PlayerMob.
        public PlayerMob Character { get; set; }

        public NetClient(Socket socket)
        {
            _socket = socket;

            _packetStream = new PacketStream(this);

            _OnReceive = OnReceive;
            _OnSend = OnSend;

            _Clients.Add(this);

            BeginReceive();
        }

        public void Start()
        {

        }

        private void BeginReceive()
        {
            // Go  back to receiving.
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, _OnReceive, _socket);
        }

        private void OnReceive(IAsyncResult asyncResult)
        {
            try
            {
                var socket = (Socket)asyncResult.AsyncState;

                var count = socket.EndReceive(asyncResult);

                if (count > 0)
                {
                    // Pass the data to packet stream.
                    _packetStream.AddBytes(_buffer, count);

                    BeginReceive();
                }
                else
                {
                    // Socket has disconnected.
                    Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // TODO :: Logging.
            }
        }

        public void Send(Packet packet, PacketSecurity packetSecurity)
        {
            packet.Security = packetSecurity;

            SendPacket(packet);
        }

        public void Send(Packet packet)
        {
            Send(packet, _sendMode);
        }

        private void SendPacket(Packet packet)
        {
            try
            {
                lock (_sendLock)
                {
                    if (packet.Security == PacketSecurity.ChecksumRnd)
                    {
                        // Add the random byte.
                        packet.Rnd = _rSecurity.NextServerByte();
                    }

                    lock (_sendQueue)
                    {
                        _sendQueue.Enqueue(packet);

                        if (packet.IsMultiPart && packet.SubPackets != null)
                        {
                            foreach (Packet subpacket in packet.SubPackets)
                            {
                                // Subpacket should inherit parent's security.
                                subpacket.Security = packet.Security;

                                if (subpacket.Security == PacketSecurity.ChecksumRnd)
                                    subpacket.Rnd = _rSecurity.NextServerByte();

                                _sendQueue.Enqueue(subpacket);
                            }
                        }
                    }

                    if (!_sending)
                    {
                        packet = null;

                        lock (_sendQueue)
                        {
                            _sendQueue.TryDequeue(out packet);
                        }

                        _sending = true;

                        var buffer = packet.GetBytes();

                        Console.WriteLine("Sending Packet :: " + packet.GetType());
                        _socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, _OnSend, _socket);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // TODO :: Error / Logging.
            }
        }

        private void OnSend(IAsyncResult asyncResult)
        {
            try
            {
                var socket = (Socket)asyncResult.AsyncState;

                var sentBytes = socket.EndSend(asyncResult);

                // Something happened during send. Client is probably disconnected.
                if (sentBytes <= 0)
                {
                    // TODO :: Disconnect.
                }

                Packet packet = null;

                // Check to see if any other packets got queued during the send.
                lock(_sendQueue)
                {
                    _sendQueue.TryDequeue(out packet);
                }

                if (packet != null)
                {
                    Console.WriteLine("Sending Packet :: " + packet.GetType());
                    var buffer = packet.GetBytes();

                    _socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, _OnSend, _socket);
                }

                else
                {
                    lock(_sendLock)
                    {
                        _sending = false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e); // TODO :: Error / Logging.
            }
        }

        public void Disconnect()
        {

        }

        public void ProcessPackets()
        {
            var packet = _packetStream.NextPacket();

            while (packet != null)
            {
                var packetHandler = PacketHandlers.GetPacketHandler(packet.PacketID);

                packetHandler.Handle(this, packet);

                // Process Packet.
                packet = _packetStream.NextPacket();
            }
        }

        public void Verified(bool verified)
        {
            if (verified)
            {
                Ack();

                // Setup Random Byte.
                InitRandomByte();

                // Client should be at the Login screen.
                _NetState = NetState.LoginScreen;
            }
            else
            {
                Nack();
                Disconnect(); // TODO :: Handle Nack
            }
        }

        static Random random = new Random();

        private void InitRandomByte()
        {
            // Generate a random Int16
            var rnd = (Int16)random.Next(Int16.MaxValue);

            _rSecurity = new RSecurity(rnd, 1001);

            // Send the seed to the client.
            RandomByte(rnd);

            // Update Send / Receive modes.
            _sendMode = PacketSecurity.ChecksumRnd;
            _receiveMode = PacketSecurity.ChecksumRnd;
        }

        public void Dispose()
        {

        }
    }
}
