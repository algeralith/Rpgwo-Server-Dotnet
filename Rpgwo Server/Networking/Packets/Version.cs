using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public sealed class Version : Packet
    {
        public string Name { get; set; }
        public string Number { get; set; }

        public Version() : base((byte)PacketTypes.Version, 41)
        {

        }

        public override bool Receive()
        {
            Name = ReadString(20);
            Number = ReadString(20);

            return true;
        }

        public override byte[] GetBytes()
        {
            AddString(Name, 20); // Should be position 1
            AddString(Number, 20); // Should be position 21

            return buffer;
        } 
    }
}
