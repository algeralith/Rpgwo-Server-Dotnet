using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpgwo_Server.Networking.Packets;
using Rpgwo_Server.Events;

namespace Rpgwo_Server.Networking.Handlers
{
    public class CreateHandler : PacketHandler
    {
        public CreateHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {

        }

        public override void Handle(NetClient client, Packet packet)
        {
            var createPacket = (Create)packet;
            // TODO :: Check State

            CreateEventArgs createEventArgs = new CreateEventArgs(client, createPacket);

            ServerEvents.InvokeCreate(createEventArgs);

            if (createEventArgs.Result)
            {
                // TODO :: Update state?
                client.Ack();
            }
            else
            {
                // TODO :: Update state?
                client.Nack();
            }

            // Let's update the client list despite the result. TODO :: Techincally, this should be called anytime we go to the character select screen.
            client.ClientList();
        }
    }
}
