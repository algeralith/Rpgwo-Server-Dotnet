using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rpgwo_Server.Networking.Packets
{
    public class Login : Packet
    {
        public String Username { get; set; } // First 20 Bytes
        public String Password { get; set; } // Second 20 Bytes
        public bool NewUser { get; set; } // Single Byte
        public String Email { get; set; } // 100 Bytes

        public Login() : base((byte)PacketTypes.Login, 141)
        {
            Username = "";
            Password = "";
            Email = "";
        }

        public override byte[] GetBytes()
        {
            AddString(Username, 20, ' ');
            AddString(Password, 20, ' ');
            AddBool(NewUser);
            AddString(Email, 100, ' ');

            return base.GetBytes();
        }

        public override bool Receive()
        {
            Username = ReadString(20);
            Password = ReadString(20);
            NewUser = ReadBool();
            Email = ReadString(100);

            return true;
        }
    }
}
