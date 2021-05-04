using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Enter : Packet
    {
        // 50 Characters
        public string Name { get; set; }

        public Enter() : base((byte)PacketTypes.Enter, 50)
        {

        }

        public override bool Receive()
        {
            Name = ReadString(50);

            return true;
        }

        public override byte[] GetBytes()
        {
            AddString(Name, 50);
            return base.GetBytes();
        }
    }
}
