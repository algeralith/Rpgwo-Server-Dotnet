using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpgwo_Server.Networking.Packets;
using Rpgwo_Server.Events;

namespace Rpgwo_Server.Networking.Handlers
{
    public class EnterFinalHandler : PacketHandler
    {
        public EnterFinalHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {

        }

        public override void Handle(NetClient client, Packet packet)
        {
            // TODO :: State.

            /*
             * ItemLocations
             * Rune Bag
             * Spell Def
             * Ack
             * World State
             * PlayerStats2
             * Attributes
             * Skill
             * Text (Various)
             * WorldState
             * StartDisplay
             * MoreItems
             * PlayerLocation
             * MapData
             * StopDisplay
             */
            
            client.StartDisplay();

            client.PlayerLocation();

            client.MapData();

            client.StopDisplay();
        }
    }
}
