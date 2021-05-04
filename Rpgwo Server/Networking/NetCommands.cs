using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking.Packets;

namespace Rpgwo_Server.Networking
{
    public partial class NetClient
    {
        public void Ack()
        {
            Send(new Ack(), PacketSecurity.None); // No security is added to Ack / Nack packets.
        }

        public void Nack()
        {
            Send(new Nack(), PacketSecurity.None); // No security is added to Ack / Nack packets.
        }

        public void ClientList()
        {
            ClientList clientList = new ClientList()
            {
                Text = "No Players."
            };

            Send(clientList);
        }

        public void CreateDef()
        {
            Send(new CreateDef()
            {
                Attributes = WorldConfig.AttributePoints,
                SkillPoints = WorldConfig.SkillPoints
            });
        }

        public void MapLineData(CardinalDirection direction)
        {
            var mapLineData = new MapLineData()
            {
                Xpos = Character.X,
                Ypos = Character.Y,
                Zpos = Character.Z,
                Direction = (byte)direction,
                Tiles = World.World.GetMapLineData(Character.X, Character.Y, direction)
            };

            //Send(new StartLineDisplay()
            // {
            //     Direction = (byte)direction
            // });

            Send(mapLineData);

            // Send(new StopDisplay());
        }

        public void MapData()
        {
            // Client viewport is 19 x 17 in default mode.
            Send(new MapData()
            {
                Xpos = 100,
                Ypos = 100,
                Zpos = 0,
                Tiles = World.World.GetViewport(Character.X, Character.Y, 0)
            });

        }

        public void MoveResult(bool result)
        {
            Send(new Event()
            {
                Result = result ? 0 : 2 // Mickey sends 0 on true, 2 on false.
            });
        }

        public void RandomByte(short seed)
        {
            Send(new RandomByte()
            {
                Seed = seed
            });
        }

        public void StartDisplay()
        {
            Send(new StartDisplay());
        }
        public void StartLineDisplay(CardinalDirection direction)
        {
            Send(new StartLineDisplay()
            {
                Direction = (byte)direction
            });
        }

        public void StopDisplay()
        {
            Send(new StopDisplay());
        }

        public void Text(string text)
        {
            // Maximum text size  if 255.
            if (text.Length > 255)
            {
                text = text.Substring(0, 255);
            }

            Text textPacket = new Text()
            {
                TextLength = (byte)text.Length,
                Channel = 10,
                TextContent = new TextContent((byte)text.Length)
                {
                    Text = text
                }
            };

            textPacket.AddSubPacket(textPacket.TextContent);

            Send(textPacket);
        }

        public void PlayerList()
        {

            var characters = Account.Characters.GetRange(0, Math.Min(5, Account.Characters.Count));

            PlayerList playerList = new PlayerList();

            for (int i = 0; i < characters.Count; i++)
            {
                playerList.PlayerNames[i] = characters[i].Name;
            }

            Send(playerList);
        }

        public void PlayerLocation()
        {
            /*
                AddByte(Xpos);
                AddByte(Ypos);
                AddByte(ImageType);
                AddByte(Stealth);
                AddString(Name, 50, ' ');
                AddByte(LifePercentage);
                AddByte(Tame);
                AddByte(pType);
                AddIntAsString(Index, 4);
                AddIntAsString(Level, 4);
                AddByte(Light);
                AddIntAsString(Image, 4);
                AddByte(Head);
                AddByte(Arms);
                AddByte(Chest);
                AddByte(Legs);
                AddByte(Weapon);
                AddByte(Shield);
                AddByte(Wearing);
                AddByte(StaminaPercentage);
                AddByte(ManaPercentage);
            */

            Send(new PlayerLocation()
            {
                Xpos = 9,
                Ypos = 8,
                ImageType = 0,
                Stealth = 1,
                Name = Character.Name,
                LifePercentage = 100,
                Tame = 0,
                pType = 0,
                Index = 1,
                Level = 0,
                Light = 10,
                Image = 0,
                Head = 1,
                Arms = 1,
                Chest = 1,
                Legs = 1,
                Weapon = 0,
                Shield = 0,
                Wearing = 0,
                StaminaPercentage = 100,
                ManaPercentage = 100
            });
        }

        public void SkillDef(Skill skill, bool clearList = false)
        {
            // Console.WriteLine("Skill definitions. " + skill.ID);

            SkillDef skillDef = new SkillDef()
            {
                Name = skill.Name,
                SkillID = (byte)skill.ID,
                ClearList = clearList,
                // Value -- Still not sure what this is
                SkillPoints = skill.Free ? (byte)(skill.SkillPoints + 100) : (byte)skill.SkillPoints, // Mickey adds + 100 to all skill points if its free.
                Strength = skill.Strength ? 1 : 0,
                Dexterity = skill.Dexterity ? 1 : 0,
                Quickness = skill.Quickness ? 1 : 0,
                Intelligence = skill.Intelligence ? 1 : 0,
                Wisdom = skill.Wisdom ? 1 : 0,
                Divisor = (byte)skill.Divisor,
                // Status, not entiretyy sure. TODO :: //
                Description = skill.Description
            };

            Send(skillDef);
        }

        public void WorldState()
        {
            Send(new WorldState()
            {
                MapSize = 1200, // TODO :: 
                Dark = 10,

            });
        }
    }
}
