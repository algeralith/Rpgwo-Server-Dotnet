using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server
{
    public enum SkillPurpose
    {
        Melee,
        Missile,
        Magic
    }

    public class Skill
    {
        private static Dictionary<int, Skill> _skills = new Dictionary<int, Skill>();
        public static Dictionary<int, Skill> Skills => _skills;

        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Free { get; private set; }
        public bool Usable { get; private set; }
        public int SkillPoints { get; private set; }
        public bool Strength { get; private set; }
        public bool Dexterity { get; private set; }
        public bool Quickness { get; private set; }
        public bool Intelligence { get; private set; }
        public bool Wisdom { get; private set; }
        public int Divisor { get; private set; }
        public bool Mastery { get; private set; }
        public int LevelRequirement { get; private set; }
        public bool SpecialFeature { get; private set; }
        public bool Exclude { get; private set; }
        public bool BurdenFactor { get; private set; }
        public SkillPurpose Purpose { get; private set; }

        public static void Load()
        {
            _skills.Clear();

            using (IniParser iniParser = new IniParser("skill.ini", "skill="))
            {
                List<IniEntry> entries = null;

                while((entries = iniParser.NextEntry()) != null)
                {
                    Skill skill = null;

                    foreach (IniEntry entry in entries)
                    {
                        switch (entry.Key)
                        {
                            case "skill":
                                skill = new Skill
                                {
                                    ID = entry.ValueAsInt()
                                };
                                break;

                            case "name":
                                skill.Name = entry.Value;
                                break;

                            case "description":
                                skill.Description = entry.Value;
                                break;

                            case "freeskill":
                                skill.Free = true;
                                break;

                            case "usable":
                                skill.Usable = true;
                                break;

                            case "skillpoints":
                                skill.SkillPoints = entry.ValueAsInt();
                                break;

                            case "str":
                                skill.Strength = true;
                                break;

                            case "dex":
                                skill.Dexterity = true;
                                break;

                            case "quick":
                                skill.Quickness = true;
                                break;

                            case "intel":
                                skill.Intelligence = true;
                                break;

                            case "wisdom":
                                skill.Wisdom = true;
                                break;

                            case "divisor":
                                skill.Divisor = entry.ValueAsInt();
                                break;

                            case "mastery":
                                skill.Mastery = true;
                                break;

                            case "levelreq":
                                skill.LevelRequirement = entry.ValueAsInt();
                                break;

                            case "excludeskill":
                                skill.Exclude = true;
                                break;

                            case "specialfeature":
                                skill.SpecialFeature = true;
                                break;

                            case "burdenfactor":
                                skill.BurdenFactor = true;
                                break;

                            case "purpose":
                                switch(entry.Value.ToLower())
                                {
                                    case "melee":
                                        skill.Purpose = SkillPurpose.Melee;
                                        break;
                                    case "missle": // TODO :: Fix this typo?
                                        skill.Purpose = SkillPurpose.Missile;
                                        break;
                                    case "magic":
                                        skill.Purpose = SkillPurpose.Magic;
                                        break;
                                    default:
                                        Console.WriteLine("Unknown purpose. Ignoring. " + entry.Value);
                                        break;
                                }
                                break;

                            default:
                                Console.WriteLine("Unknown tag in skill.ini. Ignoring. " + entry.Key); // TODO :: 
                                break;
                        }
                    }

                    if (skill != null)
                    {
                        if (_skills.ContainsKey(skill.ID))
                        {
                            Console.WriteLine("Skill id is already in use. Ignoring. ID=" + skill.ID);

                        }
                        else
                        {
                            _skills[skill.ID] = skill;
                        }
                    }
                }
            }
        }
    }

}
