using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Delete : Packet
    {
        // 50 Characters long
        public string Name { get; set; }

        public Delete() : base((byte)PacketTypes.Delete, 50)
        {

        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }

        public override byte[] GetBytes()
        {
            AddString(Name, 50);
            return base.GetBytes();
        }
    }
}
