using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class PlayerList : Packet
    {
        public String[] PlayerNames = new String[5];

        public PlayerList() : base((byte)PacketTypes.PlayerList, 250)
        {

        }

        public override byte[] GetBytes()
        {
            for (int i = 0; i < PlayerNames.Length; i++)
            {
                if (PlayerNames[i] != null)
                    AddString(PlayerNames[i], 50);
            }

            return base.GetBytes();
        }

        public override bool Receive()
        {
            for (int i = 0; i < PlayerNames.Length; i++)
            {
                PlayerNames[i] = ReadString(50);
            }

            return true;
        }
    }
}
