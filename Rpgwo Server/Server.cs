using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Rpgwo_Server.Networking;
using Rpgwo_Server.Importer;

namespace Rpgwo_Server
{
    public class Server
    {
        public Server()
        {
            Listener Listener = new Listener(new IPEndPoint(IPAddress.Any, 4503));

            // Initialize classes that we need.
            Accounts.AccountManager.Initialize();

            // Load Skills.
            Skill.Load();

            while(true)
            {
                foreach (var Client in NetClient.Clients)
                {
                    Client.ProcessPackets();
                }
            }
        }
    }
}
