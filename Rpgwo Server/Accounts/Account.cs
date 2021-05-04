using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Mob;
using Rpgwo_Server.Events;

namespace Rpgwo_Server.Accounts
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public List<PlayerMob> Characters { get; private set; } = new List<PlayerMob>();

        static Account()
        {
            ServerEvents.OnCreate += ServerEvents_OnCreate;
            ServerEvents.OnEnter += ServerEvents_OnEnter;
        }



        public Account(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void AddCharacter(PlayerMob character)
        {
            Characters.Add(character);
        }

        public void RemoveCharacter(String name)
        {

        }

        private static void ServerEvents_OnEnter(EnterEventArgs e)
        {
            var enter = e.Enter;
            var account = e.Client.Account;

            // Make sure the character actually exists.
            // TODO :: Make sure character is not deleted, and is allowed to be used.
            foreach (var character in account.Characters)
            {
                if (character.Name.Trim() == enter.Name.Trim())
                {
                    // Character exists.
                    e.Result = true;
                    e.Character = character;

                    return;
                }
            }

            e.Result = false;
            e.Reason = "Character does not exist.";
        }

        private static void ServerEvents_OnCreate(CreateEventArgs e)
        {   
            var TotalAP = WorldConfig.AttributePoints;
            var TotalSP = WorldConfig.SkillPoints;
            var create = e.Create;

            // First, Verify that the attributes are correct.

            // Make sure the the packet has at least the minimum values for each attribute.
            if (create.Life < 10 || create.Strength < 10 || create.Dexterity < 10 
                || create.Quickness < 10 || create.Intelligence < 10 || create.Wisdom < 10)
            {
                e.Reason = "Some attribute values are below minimum allowed.";
                e.Result = false;
                return;
            }

            // Now to make sure the values are not above the maximum.
            if (create.Life > 100 || create.Strength > 100 || create.Dexterity > 100
                || create.Quickness > 100 || create.Intelligence > 100 || create.Wisdom > 100)
            {
                e.Reason = "Some attribute values are above maximum allowed.";
                e.Result = false;
                return;
            }

            // And finally, make sure the total values do no exceed the total Attribute points allowed.
            if (create.Life + create.Strength + create.Dexterity + create.Quickness + create.Intelligence + create.Wisdom > TotalAP)
            {
                e.Reason = "Total attribute values exceed maximum allowed.";
                e.Result = false;
                return;
            }

            // At this point, attributes are all correct. Let's make sure 
            var spentSP = 0;
            foreach (var skill in create.Skills)
            {
                if (skill == null)
                    continue;

                if (!Skill.Skills.ContainsKey(skill.SkillID))
                {
                    // Skill does not exist. For now we'll ignore. // TODO :: Handle this case with a log.
                }

                // Skill is free, do not count it towards the cost.
                if (Skill.Skills[skill.SkillID].Free)
                    continue;

                if (skill.Spec)
                {
                    spentSP += (Skill.Skills[skill.SkillID].SkillPoints * 2);
                }
                else
                {
                    spentSP += Skill.Skills[skill.SkillID].SkillPoints;
                }
            }

            // Make sure they are under the SP threshold.
            if (spentSP > TotalSP)
            {
                e.Reason = "Total Skill Points used exceeds maximum allowed.";
                e.Result = false;
                return;
            }

            // All tests have passed, go ahead and try to create the character.
            var player = PlayerMob.CreateFromPacket(create);

            if (player == null)
            {
                e.Reason = "Failed to create character. Let someone know.";
                e.Result = false;
                return;
            }

            // Go ahead and add the character to the account. TODO :: If you are enforcing a character limit, this might be a good spot. Or at the start.
            e.Client.Account.AddCharacter(player);

            e.Result = true;
        }
    }
}
