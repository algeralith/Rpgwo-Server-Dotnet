using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public abstract class Packet
    {
        public byte PacketID { get; }

        public bool HasID { get; } = false;

        // Security Fields
        public byte CRC { get; set; }
        public byte Rnd { get; set; }

        // Packet contents
        protected byte[] buffer { get; private set; }

        // Multipart
        public Packet Parent { get; private set; }
        public List<Packet> SubPackets { get; private set; }
        public bool IsMultiPart { get; protected set; }
        public bool MultiComplete { get; protected set; }

        // Security
        public PacketSecurity Security { get; set; } = PacketSecurity.None;

        private int _writeHead = 0;
        private int _readHead = 0;

        public Packet(byte packetID, int size)
        {
            this.PacketID = packetID;
            this.buffer = new byte[size];
            HasID = true;
        }

        // Not every packet will have a header. Mostly for TextContent
        public Packet(int size)
        {
            this.buffer = new byte[size];
            HasID = false;
        }

        public void AddSubPacket(Packet packet)
        {
            if (SubPackets == null)
                SubPackets = new List<Packet>();

            // Inherit security from parent.
            packet.Security = Security;
            packet.Parent = this;

            SubPackets.Add(packet);
        }

        private int packetPos = 0;
        public Packet NextSubPacket()
        {
            if (SubPackets[packetPos] != null)
            {
                var packet = SubPackets[packetPos];
                packetPos++;
                return packet;
            }

            return null;
        }

        public virtual byte[] GetBytes()
        {
            byte[] tmp;
            if (HasID)
            {
                switch (Security)
                {
                    case PacketSecurity.None:
                        tmp = new byte[buffer.Length + 1];
                        tmp[0] = PacketID;
                        Buffer.BlockCopy(buffer, 0, tmp, 1, buffer.Length);
                        return tmp;

                    case PacketSecurity.Checksum:
                        CalculateChecksum(PacketID);
                        tmp = new byte[buffer.Length + 2];
                        tmp[0] = PacketID;
                        Buffer.BlockCopy(buffer, 0, tmp, 1, buffer.Length);
                        tmp[^1] = CRC;
                        return tmp;

                    case PacketSecurity.ChecksumRnd:
                        CalculateChecksumRnd(PacketID);
                        tmp = new byte[buffer.Length + 3];
                        tmp[0] = PacketID;
                        Buffer.BlockCopy(buffer, 0, tmp, 1, buffer.Length);
                        tmp[^2] = Rnd;
                        tmp[^1] = CRC;
                        return tmp;
                }
            }
            else
            {
                switch (Security)
                {
                    case PacketSecurity.None:
                        tmp = new byte[buffer.Length];
                        Buffer.BlockCopy(buffer, 0, tmp, 0, buffer.Length);
                        return tmp;

                    case PacketSecurity.Checksum:
                        CalculateChecksum();
                        tmp = new byte[buffer.Length + 1];
                        Buffer.BlockCopy(buffer, 0, tmp, 0, buffer.Length);
                        tmp[^1] = CRC;
                        return tmp;

                    case PacketSecurity.ChecksumRnd:
                        CalculateChecksumRnd();
                        tmp = new byte[buffer.Length + 2];
                        Buffer.BlockCopy(buffer, 0, tmp, 0, buffer.Length);
                        tmp[^2] = Rnd;
                        tmp[^1] = CRC;
                        return tmp;
                }
            }

            return buffer;
        }

        public abstract bool Receive();

        public int Size
        {
            get
            {
                if (buffer == null)
                    return 0;
                else
                    return buffer.Length;
            }
        }
        
        public virtual int Remaining()
        {
            return Math.Max(buffer.Length - _writeHead, 0);
        }
        
        public void AdvanceWriteHead(int length)
        {
            _writeHead += length; // TODO :: I hate this type of function, redo it at some point.
        }

        protected bool ResizeBuffer(int length)
        {
            if (length < buffer.Length)
            {
                // We can not decrease size, only increase.
                return false;
            }

            byte[] tmpBuff = new byte[length];

            buffer.CopyTo(tmpBuff, 0);

            buffer = tmpBuff;

            return true;
        }

        // Add Methods
        public void AddBool(bool b)
        {
            if (b == true)
                AddByte(0xFF);
            else
                AddByte(0x00);
        }

        public void AddByte(byte b)
        {
            buffer[_writeHead] = b;
            _writeHead++;
        }

        public virtual void AddBytes(byte[] data)
        {
            // TODO :: Make sure not to overflow past end of buffer
            data.CopyTo(buffer, _writeHead);

            _writeHead += data.Length;
        }

        public void AddBytes(byte[] data, int pos, int count)
        {
            // TODO :: Make sure not to overflow past end of buffer
            Buffer.BlockCopy(data, pos, buffer, _writeHead, count);

            _writeHead += count;
        }

        public void AddInt16(Int16 i)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(i), 0, buffer, _writeHead, 2);
            _writeHead += 2;
        }

        public void AddInt32(int i)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(i), 0, buffer, _writeHead, 4);
            _writeHead += 4;
        }

        public void AddIntAsString(int i, int length)
        {
            String s = i.ToString();
            char c = s[0];

            byte[] b = new byte[length];

            for (int j = b.Length - 1; j >= b.Length - s.Length; j--)
            {
                b[j] = (byte)(s[s.Length - (b.Length - j)] - 0x30);
            }

            AddBytes(b);
        }

        public void AddString(String s, int maxLength, char initial)
        {
            if (s.Length > maxLength)
            {
                s = s.Substring(0, maxLength);
            }
            
            byte[] tmpBuff = Encoding.ASCII.GetBytes(new string(initial, maxLength));
            tmpBuff.CopyTo(buffer, _writeHead);

            byte[] stringBytes = Encoding.UTF8.GetBytes(s);
            stringBytes.CopyTo(buffer, _writeHead);

            _writeHead += maxLength;
        }

        public void AddString(String s, int maxLength)
        {
            if (s.Length > maxLength)
            {
                s = s.Substring(0, maxLength);
            }

            byte[] stringBytes = Encoding.UTF8.GetBytes(s);
            stringBytes.CopyTo(buffer, _writeHead);

            _writeHead += maxLength;
        }

        // Read Methods
        public bool ReadBool()
        {
            byte b = ReadByte();

            if (b == 0XFF)
                return true;
            else
                return false;
        }

        public byte ReadByte()
        {
            byte b = buffer[_readHead];
            _readHead++;
            return b;
        }

        public byte[] ReadBytes(int length)
        {
            byte[] b = new byte[length];
            Array.Copy(buffer, _readHead, b, 0, length);
            _readHead += length;

            return b;
        }

        public int ReadStringAsInt(int length)
        {
            // For playerstats the each byte represents its value in a string
            // i.e { 0x0, 0x0, 0x1, 0x2 } -> 0012 -> 12.
            byte[] buff = null;
            try
            {
                StringBuilder stringBuilder = new StringBuilder(9); // Most usages are 9 characters

                buff = ReadBytes(length);

                for (int i = 0; i < buff.Length; i++)
                    stringBuilder.Append(buff[i]);

                String s = stringBuilder.ToString();
                int value = Convert.ToInt32(s);
                return value;
            }
            catch (Exception e)
            {
                Console.Write(e);
                return 0;
            }
        }

        public Int16 ReadInt16()
        {
            Int16 i = BitConverter.ToInt16(buffer, _readHead);
            _readHead += 2;
            return i;
        }

        public int ReadInt32()
        {
            int i = BitConverter.ToInt32(buffer, _readHead);
            _readHead += 4;
            return i;
        }

        public String ReadString(int maxLength)
        {
            String tmp = Encoding.UTF8.GetString(ReadBytes(maxLength)).TrimEnd('\0');

            return tmp;
        }

        public void CalculateChecksum()
        {
            bool sw = false;
            Int16 num1 = 0;
            Int16 num2 = 0;

            for (int i = 0; i < buffer.Length; i++)
            {
                char value = Convert.ToString(buffer[i]).ElementAt<char>(0);

                if (sw == false)
                {
                    num1 += Convert.ToByte(value);
                    sw = true;
                }
                else
                {
                    num2 += Convert.ToByte(value);
                    sw = false;
                }
            }

            CRC = (byte)((num1 + (num2 * 3)) % 255);
        }

        private void CalculateChecksum(byte packetID)
        {
            bool sw = true;
            Int16 num1 = 0;
            Int16 num2 = 0;

            char value = Convert.ToString(packetID).ElementAt<char>(0);
            num1 += Convert.ToByte(value);

            for (int i = 0; i < buffer.Length; i++)
            {
                value = Convert.ToString(buffer[i]).ElementAt<char>(0);

                if (sw == false)
                {
                    num1 += Convert.ToByte(value);
                    sw = true;
                }
                else
                {
                    num2 += Convert.ToByte(value);
                    sw = false;
                }
            }

            CRC = (byte)((num1 + (num2 * 3)) % 255);
        }

        public void CalculateChecksumRnd()
        {
            bool sw = false;
            Int16 num1 = 0;
            Int16 num2 = 0;

            for (int i = 0; i < buffer.Length; i++)
            {
                char value = Convert.ToString(buffer[i]).ElementAt<char>(0);

                if (sw == false)
                {
                    num1 += Convert.ToByte(value);
                    sw = true;
                }
                else
                {
                    num2 += Convert.ToByte(value);
                    sw = false;
                }
            }

            CRC = (byte)((num1 + Rnd + (num2 * 3)) % 255);
        }

        private void CalculateChecksumRnd(byte packetID)
        {
            bool sw = true;
            Int16 num1 = 0;
            Int16 num2 = 0;

            char value = Convert.ToString(packetID).ElementAt<char>(0);
            num1 += Convert.ToByte(value);

            for (int i = 0; i < buffer.Length; i++)
            {
                value = Convert.ToString(buffer[i]).ElementAt<char>(0);

                if (sw == false)
                {
                    num1 += Convert.ToByte(value);
                    sw = true;
                }
                else
                {
                    num2 += Convert.ToByte(value);
                    sw = false;
                }
            }

            CRC = (byte)((num1 + Rnd + (num2 * 3)) % 255);
        }
    }
}
