using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpgwo_Server.Networking.Packets;

namespace Rpgwo_Server.Networking.Handlers
{
    public class ReqSkillListHandler : PacketHandler
    {
        public ReqSkillListHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {
        }

        public override void Handle(NetClient client, Packet packet)
        {
            // TODO :: State Check.
            // When we receive this packet, the client is trying to create a new character.

            // Send createDef.
            client.CreateDef();

            bool cleared = false;
            foreach (Skill skill in Skill.Skills.Values)
            {
                if (cleared)
                    client.SkillDef(skill);
                else
                {
                    cleared = true;
                    client.SkillDef(skill, true);
                }
            }
        }
    }
}
