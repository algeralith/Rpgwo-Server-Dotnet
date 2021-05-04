using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class Appeal : Packet
    {
        /*
        560 bytes

        Topic 30 Bytes
        Proi -- 31
        AppealType -- 32
        Age  -- 33
        Status -- 35
        Index -- 37
        Owner --  20 bytes
        Admin 58 -- Probably Int16

        AppealBody 500 Bytes
        */

        public string Topic { get; set; } // 30 characters.
        public byte Priority { get; set; } // Low, Normal, High.
        public byte Type { get; set; } // Bug, Request, Suggest, Player, Admin, Comment.
        public Int16 Age { get; set; }
        public byte Status { get; set; } // Open, Pending, Close
        public Int16 Index { get; set; }
        public string Owner { get; set; }  // 20 characters.
        public Int16 Admin { get; set; } // Not sure what this does. I assume it is 16 bytes since no other field follows except text.
        public string Text { get; set; } // 500 characters.

        public Appeal() : base((byte)PacketTypes.Appeal, 560)
        {

        }

        public override byte[] GetBytes()
        {
            return base.GetBytes();
        }

        public override bool Receive()
        {
            Topic = ReadString(30);

            Priority = ReadByte();
            Type = ReadByte();
            Age = ReadInt16();
            Status = ReadByte();
            Index = ReadInt16();
            Owner = ReadString(20);
            Admin = ReadInt16();

            Text = ReadString(500);

            return true;
        }
    }
}
