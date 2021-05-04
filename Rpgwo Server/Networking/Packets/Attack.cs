using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class Attack : Packet
    {
        public int PlayerIndex { get; set; }

        public Attack() : base ((byte)PacketTypes.Attack, 4)
        {

        }

        public override byte[] GetBytes()
        {
            AddIntAsString(PlayerIndex, 4);

            return base.GetBytes();
        }

        public override bool Receive()
        {
            throw new NotImplementedException();
        }
    }
}
