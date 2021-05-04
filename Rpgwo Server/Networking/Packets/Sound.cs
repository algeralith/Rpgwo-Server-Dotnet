using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Sound : Packet
    {

        public string SoundName { get; set; }
        public bool Loop { get; set; } // TODO :: Verify

        public Sound() : base((byte)PacketTypes.Sound, 21)
        {

        }

        public override bool Receive()
        {
            SoundName = ReadString(20);
            Loop = ReadBool();

            return true;
        }
    }
}
