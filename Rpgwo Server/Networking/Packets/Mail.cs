using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class Mail : Packet
    {
        public string Subject { get; set; }  // 30 characters.
        public bool NewMail { get; set; }
        public DateTime Date { get; set; } // I know this is 8 bytes, I'm not not sure how to convert it. TODO ::
        public string ToFrom { get; set; } // 50 characters.
        public string Text { get; set; } // 200 characters.
        public Int32 Index { get; set; }

        public Mail() : base((byte)PacketTypes.Mail, 294)
        {

        }

        public override byte[] GetBytes()
        {
            return base.GetBytes();
        }

        public override bool Receive()
        {
            Subject = ReadString(30);
            NewMail = ReadBool();
            ReadBytes(8); // Not sure how to parse date.
            ReadByte(); // Terminate string? Is the date a string?
            ToFrom = ReadString(50);
            Text = ReadString(200);
            Index = ReadInt32();

            return true;
        }
    }
}
