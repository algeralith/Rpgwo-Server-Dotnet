using System;
using System.Collections.Generic;
using System.Text;
using Rpgwo_Server.Networking.Packets;
using Rpgwo_Server.Events;

namespace Rpgwo_Server.Networking.Handlers
{
    public class LoginHandler : PacketHandler
    {
        public LoginHandler(PacketTypes packetID, Type packetType) : base(packetID, packetType)
        {
        }

        public override void Handle(NetClient client, Packet packet)
        {
            // TODO  :: State Check. Make sure account is not already logged into.
            var login = (Login)packet;

            LoginEventArgs loginEventArgs = new LoginEventArgs(client, login.Username.Trim(), login.Password.Trim(), login.Email.Trim(), login.NewUser);

            ServerEvents.InvokeAccountLogin(loginEventArgs);

            if (loginEventArgs.Result)
            {
                // Login / Account creation were succesful.
                client.Ack();

                // Send the client list.
                client.ClientList();
            }
            else
            {
                client.Text(loginEventArgs.Reason);
                client.Nack();
            }
        }
    }
}
