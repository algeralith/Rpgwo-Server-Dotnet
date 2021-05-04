using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Events
{
    // Event Delegates.
    public delegate void VersionHandler(VersionEventArgs e);
    public delegate void Info2Handler(Info2EventArgs e);
    public delegate void AccountLoginHandler(LoginEventArgs e);
    public delegate void ClientListHandler(ClientListEventArgs e);
    public delegate void CreateHandler(CreateEventArgs e);
    public delegate void EnterHandler(EnterEventArgs e);

    public static class ServerEvents 
    {
        // Events.
        public static event VersionHandler OnVersion;
        public static event Info2Handler OnInfo2;
        public static event AccountLoginHandler OnAccountLogin;
        public static event ClientListHandler OnClientList;
        public static event CreateHandler OnCreate;
        public static event EnterHandler OnEnter;

        // Event invokations.
        public static void InvokeVersion(VersionEventArgs e)
        {
            OnVersion?.Invoke(e);
        }

        public static void InvokeInfo2(Info2EventArgs e)
        {   
            OnInfo2?.Invoke(e);
        }

        public static void InvokeAccountLogin(LoginEventArgs e)
        {
            OnAccountLogin?.Invoke(e);
        }

        public static void InvokeClientList(ClientListEventArgs e)
        {
            OnClientList?.Invoke(e);
        }

        public static void InvokeCreate(CreateEventArgs e)
        {
            OnCreate?.Invoke(e);
        }

        public static void InvokeEnter(EnterEventArgs e)
        {
            OnEnter?.Invoke(e);
        }
    }
}
