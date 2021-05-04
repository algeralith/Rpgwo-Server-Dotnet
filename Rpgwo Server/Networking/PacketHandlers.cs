using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking.Packets;
using Rpgwo_Server.Networking.Handlers;

namespace Rpgwo_Server.Networking
{
    public class PacketHandlers
    {
        // RPGWO uses a byte, thus 256 max packets. But for now, let's use int for future changes.
        private static readonly Dictionary<int, PacketHandler> _packets = new Dictionary<int, PacketHandler>(256);
        public static Dictionary<int, PacketHandler> Packets => _packets;

        public PacketHandlers()
        {
        }

        static PacketHandlers()
        {
            RegisterPacket(new VersionHandler(PacketTypes.Version, typeof(Packets.Version)));
            RegisterPacket(new LoginHandler(PacketTypes.Login, typeof(Login)));
            RegisterPacket(new Info2Handler(PacketTypes.Info2, typeof(Info2)));
            RegisterPacket(new ReqSkillDefHandler(PacketTypes.ReqSkillDef, typeof(ReqSkillDef)));
            RegisterPacket(new ReqPlayerListHandler(PacketTypes.ReqPlayerList, typeof(ReqPlayerList)));
            RegisterPacket(new ReqClientListHandler(PacketTypes.ReqClientList, typeof(ReqClientList)));
            RegisterPacket(new ReqSkillListHandler(PacketTypes.ReqSkillList, typeof(ReqSkillList)));
            RegisterPacket(new CreateHandler(PacketTypes.Create, typeof(Create)));
            RegisterPacket(new EnterHandler(PacketTypes.Enter, typeof(Enter)));
            RegisterPacket(new EnterFinalHandler(PacketTypes.EnterFinal, typeof(EnterFinal)));
            RegisterPacket(new TextHandler(PacketTypes.Text, typeof(Text)));
            RegisterPacket(new RequestHandler(PacketTypes.Request, typeof(Request)));
            RegisterPacket(new MovePlayerHandler(PacketTypes.MovePlayer, typeof(MovePlayer)));

            // RegisterPacket((byte)PacketTypes.Nack, typeof(Nack));
            // RegisterPacket((byte)PacketTypes.Version, typeof(Packets.Version));
            // RegisterPacket((byte)PacketTypes.Login, typeof(Login));
            // RegisterPacket((byte)PacketTypes.Create, typeof(Create));
            // RegisterPacket((byte)PacketTypes.ReqPlayerList, typeof(ReqPlayerList));
            // RegisterPacket((byte)PacketTypes.PlayerList, typeof(PlayerList));
            // RegisterPacket((byte)PacketTypes.Logout, typeof(Logout));
            // RegisterPacket((byte)PacketTypes.Delete, typeof(Delete));
            // RegisterPacket((byte)PacketTypes.MapData, typeof(MapData));
            // RegisterPacket((byte)PacketTypes.Enter, typeof(Enter));
            // RegisterPacket((byte)PacketTypes.EnterFinal, typeof(EnterFinal));
            // RegisterPacket((byte)PacketTypes.ExitWorld, typeof(ExitWorld));
            // RegisterPacket((byte)PacketTypes.MovePlayer, typeof(MovePlayer));
            // RegisterPacket((byte)PacketTypes.StartDisplay, typeof(StartDisplay));
            // RegisterPacket((byte)PacketTypes.StopDisplay, typeof(StopDisplay));
            // RegisterPacket((byte)PacketTypes.PlayerLocation, typeof(PlayerLocation));
            // RegisterPacket((byte)PacketTypes.ItemLocation, typeof(ItemLocation));
            // RegisterPacket((byte)PacketTypes.WorldState, typeof(WorldState));
            // RegisterPacket((byte)PacketTypes.MapLineData, typeof(MapLineData));
            // RegisterPacket((byte)PacketTypes.StartLineDisplay, typeof(StartLineDisplay));
            // RegisterPacket((byte)PacketTypes.PlayerStats, typeof(PlayerStats));
            // RegisterPacket((byte)PacketTypes.ReqClientList, typeof(ReqClientList));
            // RegisterPacket((byte)PacketTypes.ClientList, typeof(ClientList));
            // RegisterPacket((byte)PacketTypes.Attack, typeof(Attack));
            // RegisterPacket((byte)PacketTypes.RaiseAttribute, typeof(RaiseAttribute));
            // RegisterPacket((byte)PacketTypes.RaiseSkill, typeof(RaiseSkill));
            // RegisterPacket((byte)PacketTypes.Event, typeof(Event));
            // RegisterPacket((byte)PacketTypes.Sound, typeof(Sound));
            // RegisterPacket((byte)PacketTypes.Skill, typeof(Skill));
            // RegisterPacket((byte)PacketTypes.Attributes, typeof(Attributes));
            // RegisterPacket((byte)PacketTypes.ReqSkillList, typeof(ClientList));
            // RegisterPacket((byte)PacketTypes.TrainSkill, typeof(TrainSkill));
            // RegisterPacket((byte)PacketTypes.MoveItem, typeof(ClientList));
            // RegisterPacket((byte)PacketTypes.Text, typeof(Text));
            // RegisterPacket((byte)PacketTypes.UseItem, typeof(UseItem));
            // RegisterPacket((byte)PacketTypes.PlayerStats2, typeof(PlayerStats2));
            // RegisterPacket((byte)PacketTypes.StatCheck, typeof(StatCheck));
            // RegisterPacket((byte)PacketTypes.SelfDestruct, typeof(SelfDestruct));
            // RegisterPacket((byte)PacketTypes.PlayerInfo2, typeof(PlayerInfo2));
            // RegisterPacket((byte)PacketTypes.UntrainSkill, typeof(UntrainSkill));
            // RegisterPacket((byte)PacketTypes.RequestAppeal, typeof(ReqAppeal));
            // RegisterPacket((byte)PacketTypes.Appeal, typeof(Appeal));
            // RegisterPacket((byte)PacketTypes.AppealDel, typeof(AppealDel));
            // RegisterPacket((byte)PacketTypes.Cast, typeof(Cast));
            // RegisterPacket((byte)PacketTypes.SpellTest, typeof(SpellTest));
            // RegisterPacket((byte)PacketTypes.SpellDef, typeof(SpellDef));
            // RegisterPacket((byte)PacketTypes.RuneBag, typeof(RuneBag));
            // RegisterPacket((byte)PacketTypes.DropRune, typeof(DropRune));
            // RegisterPacket((byte)PacketTypes.CreateDef, typeof(CreateDef));
            // RegisterPacket((byte)PacketTypes.SkillDef, typeof(SkillDef));
            // RegisterPacket((byte)PacketTypes.ReqSkillDef, typeof(ReqSkillDef));
            // RegisterPacket((byte)PacketTypes.SpellDelete, typeof(SpellDelete));
            // RegisterPacket((byte)PacketTypes.Animation2, typeof(Animation2));
            // RegisterPacket((byte)PacketTypes.CheckItem, typeof(CheckItem));
            // RegisterPacket((byte)PacketTypes.MonsterLocation, typeof(MonsterLocation));
            // RegisterPacket((byte)PacketTypes.Projectile, typeof(Projectile));
            // RegisterPacket((byte)PacketTypes.RandomByte, typeof(RandomByte));
            // RegisterPacket((byte)PacketTypes.Info2, typeof(Info2));
            // RegisterPacket((byte)PacketTypes.Info3, typeof(Info3));
            // RegisterPacket((byte)PacketTypes.Request, typeof(Request));
            // RegisterPacket((byte)PacketTypes.Mail, typeof(Mail));
            // RegisterPacket((byte)PacketTypes.Result, typeof(Result));
            // RegisterPacket((byte)PacketTypes.Ack, typeof(Ack));
        }

        public static void RegisterPacket(PacketHandler packetHandler)
        {
            if (_packets.ContainsKey(packetHandler.PacketID))
            {
                // Packet already registered. Error.
                Console.WriteLine("Packet ID already in use.");
                return;
            }

            _packets.Add(packetHandler.PacketID, packetHandler);
        }

        public static PacketHandler GetPacketHandler(int packetID)
        {
            PacketHandler packetHandler = null;

            _packets.TryGetValue(packetID, out packetHandler);

            return packetHandler;
        }
    }
}
