using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class AppealDel : Packet
    {
        public Int16 AppealIndex { get; set; }

        public AppealDel() : base((byte)PacketTypes.AppealDel, 2)
        {

        }

        public override byte[] GetBytes()
        {
            AddInt16(AppealIndex);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
