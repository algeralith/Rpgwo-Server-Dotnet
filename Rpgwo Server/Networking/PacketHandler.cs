using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking;
using Rpgwo_Server.Networking.Packets;

namespace Rpgwo_Server.Networking
{
    public abstract class PacketHandler
    {
        private readonly int _packetID;
        public int PacketID => _packetID;

        private readonly Type _packetType;
        public Type PacketType => _packetType;

        public PacketHandler(PacketTypes packetID, Type packetType)
        {
            _packetID = (int)packetID;
            _packetType = packetType;
        }

        public abstract void Handle(NetClient client, Packet packet);
    }
}
