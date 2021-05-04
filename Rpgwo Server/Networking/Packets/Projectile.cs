using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking.Packets
{
    public class Projectile : Packet
    {
        public Int16 AnimationID { get; set; }
        public Int16 StartXpos { get; set; }
        public Int16 StartYPos { get; set; }
        public Int16 StopXpos { get; set; }
        public Int16 StopYPos { get; set; }

        public Projectile() : base((byte)PacketTypes.Projectile, 10)
        {

        }

        public override bool Receive()
        {
            AnimationID = ReadInt16();
            StartXpos = ReadInt16();
            StartYPos = ReadInt16();
            StopXpos = ReadInt16();
            StopYPos = ReadInt16();

            return true;
        }
    }
}
