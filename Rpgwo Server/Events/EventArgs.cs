using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking;
using Rpgwo_Server.Networking.Packets;
using Rpgwo_Server.Mob;

namespace Rpgwo_Server.Events
{
    public class VersionEventArgs : EventArgs
    {
        private readonly NetClient _client;
        public NetClient Client => _client;

        private readonly string _name;
        public string Name => _name;

        private readonly string _number;
        public string Number => _number;

        public VersionEventArgs(NetClient client, string name, string number)
        {
            _client = client;
            _name = name;
            _number = number;
        }
    }

    public class Info2EventArgs : EventArgs
    {
        private readonly NetClient _client;
        public NetClient Client => _client;
    }

    public class LoginEventArgs : EventArgs
    {
        private readonly NetClient _client;
        public NetClient Client => _client;

        private readonly string _username;
        public string Username => _username;

        private readonly string _password;
        public string Password => _password;

        private readonly string _email;
        public string Email => _email;

        private readonly bool _newUser;
        public bool NewUser => _newUser;

        public bool Result { get; set; } = false;
        public string Reason { get; set; }

        
        public LoginEventArgs(NetClient client, string account, string password, string email, bool newAccount)
        {
            _client = client;
            _username = account;
            _email = email;
            _password = password;
            _newUser = newAccount;
        }

    }

    public class ClientListEventArgs : EventArgs
    {
        private readonly NetClient _client;
        public NetClient Client => _client;

        public ClientListEventArgs(NetClient client)
        {
            _client = client;
        }
    }

    public class CreateEventArgs : EventArgs
    {
        private readonly NetClient _client;
        public NetClient Client => _client;

        private readonly Create _create;
        public Create Create => _create;

        public bool Result { get; set; } = false;
        public string Reason { get; set; } // This will probably not be used.

        public CreateEventArgs(NetClient client, Create create)
        {
            _client = client;
            _create = create;
        }
    }

    public class EnterEventArgs : EventArgs
    {
        private readonly NetClient _client;
        public NetClient Client => _client;

        private readonly Enter _enter;
        public Enter Enter => _enter;

        public bool Result { get; set; } = false;
        public string Reason { get; set; }
        public PlayerMob Character { get; set; }

        public EnterEventArgs(NetClient client, Enter enter)
        {
            _client = client;
            _enter = enter;
        }
    }
}
