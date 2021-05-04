using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class ClientList : Packet
    {
        public String Text { get; set; }

        public bool ClearList { get; set; }

        public ClientList() : base((byte)PacketTypes.ClientList, 21)
        {

        }

        public override bool Receive()
        {
            Text = ReadString(20);

            return true;
        }
    }
}
