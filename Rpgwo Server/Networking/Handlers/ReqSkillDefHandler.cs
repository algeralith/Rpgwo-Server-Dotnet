using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking.Packets;

namespace Rpgwo_Server.Networking.Handlers
{
    public class ReqSkillDefHandler : PacketHandler
    {
        public ReqSkillDefHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {
        }

        public override void Handle(NetClient client, Packet packet)
        {
            // TODO :: State Check.

            bool cleared = false;
            foreach(Skill skill in Skill.Skills.Values)
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
