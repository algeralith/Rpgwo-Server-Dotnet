using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpgwo_Server.Networking.Packets;
using Rpgwo_Server.Events;

namespace Rpgwo_Server.Networking.Handlers
{
    public class EnterHandler : PacketHandler
    {
        public EnterHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
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

            var enter = (Enter)packet;

            EnterEventArgs enterEventArgs = new EnterEventArgs(client, enter);
            ServerEvents.InvokeEnter(enterEventArgs);

            if (!enterEventArgs.Result)
            {
                // For some reason we could not log in. // TODO :: Print.
                client.Nack();

                // TODO :: Reset State.
            }

            // Associate player with client.
            client.Character = enterEventArgs.Character;

            // All is good. Time to start sending in packets needed.
            client.Ack();


            client.WorldState();
            /*
            client.StartDisplay();

            client.PlayerLocation();

            client.MapData();

            client.StopDisplay();
*/
        }
    }
}
