using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpgwo_Server.Networking.Packets;
using Rpgwo_Server.Events;

namespace Rpgwo_Server.Networking.Handlers
{
    public class MovePlayerHandler : PacketHandler
    {
        public MovePlayerHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {

        }

        public override void Handle(NetClient client, Packet packet)
        {
            // TODO :: All of this.

            var move = (MovePlayer)packet;
            client.StartLineDisplay((CardinalDirection)move.Direction);

            switch ((CardinalDirection)move.Direction)
            {
                case CardinalDirection.North:
                    client.Character.Y -= 1;
                    client.MapLineData((CardinalDirection)move.Direction);
                    break;

                case CardinalDirection.South:
                    client.Character.Y += 1;
                    client.MapLineData((CardinalDirection)move.Direction);
                    break;

                case CardinalDirection.East:
                    client.Character.X += 1;
                    client.MapLineData((CardinalDirection)move.Direction);
                    break;

                case CardinalDirection.West:
                    client.Character.X -= 1;
                    client.MapLineData((CardinalDirection)move.Direction);
                    break;

                case CardinalDirection.NorthEast:
                    client.Character.Y -= 1;
                    client.Character.X += 1;
                    client.MapLineData(CardinalDirection.North);
                    client.MapLineData(CardinalDirection.East);
                    break;

                case CardinalDirection.NorthWest:
                    client.Character.Y -= 1;
                    client.Character.X -= 1;
                    client.MapLineData(CardinalDirection.North);
                    client.MapLineData(CardinalDirection.West);
                    break;

                case CardinalDirection.SouthEast:
                    client.Character.Y += 1;
                    client.Character.X += 1;
                    client.MapLineData(CardinalDirection.South);
                    client.MapLineData(CardinalDirection.East);
                    break;

                case CardinalDirection.SouthWest:
                    client.Character.Y += 1;
                    client.Character.X -= 1;
                    client.MapLineData(CardinalDirection.South);
                    client.MapLineData(CardinalDirection.West);
                    break;
            }

            client.StopDisplay();
            client.MoveResult(true); // TODO :: This. All of this.

            client.PlayerLocation();
        }
    }
}
