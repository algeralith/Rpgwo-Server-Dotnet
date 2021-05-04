using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Text : Packet
    {
        public byte TextLength { get; set; } = 0;
        public byte Channel { get; set; } = 0;
        public int UUID { get; set; } = 0;
        public int ClientID { get; set; } = 0;
        public TextContent TextContent { get; set; }

        // Not part of the Packet Structure.
        private bool TextPart { get; set; }

        public Text() : base((byte)PacketTypes.Text, 10)
        {
            IsMultiPart = true; // Comes in as two packets.
            MultiComplete = false;
            TextPart = false;

            // Add TextContent as a sub packet.
            // AddSubPacket(TextContent);
        }

        public override byte[] GetBytes()
        {
            AddByte(TextLength); // TODO :: Enforce text limit of 255.
            AddByte(Channel);
            AddInt32(UUID);
            AddInt32(ClientID);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            TextLength = ReadByte();
            Channel = ReadByte();
            UUID = ReadInt32();
            ClientID = ReadInt32();

            TextContent = new TextContent(TextLength);
            AddSubPacket(TextContent);

            return true;
        }
    }

    public class TextHeader : Packet
    {
        public byte TextLength { get; set; } = 0;
        public byte Channel { get; set; } = 0;
        public int UUID { get; set; } = 0;
        public int ClientID { get; set; } = 0;

        public TextHeader() : base(10)
        {

        }

        public override byte[] GetBytes()
        {
            AddByte(TextLength); // TODO :: Enforce text limit of 255.s
            AddByte(Channel);
            AddInt32(UUID);
            AddInt32(ClientID);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            TextLength = ReadByte();
            Channel = ReadByte();
            UUID = ReadInt32();
            ClientID = ReadInt32();

            return true;
        }
    }

    public class TextContent : Packet
    {
        public string Text { get; set; }
        public TextContent(byte textLength) : base(textLength)
        {

        }

        public override byte[] GetBytes()
        {
            AddString(Text, buffer.Length);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            Text = ReadString(buffer.Length);

            return true;
        }
    }
}
