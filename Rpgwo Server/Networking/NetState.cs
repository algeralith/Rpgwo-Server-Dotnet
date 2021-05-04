using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking
{
    public enum NetState
    {
        None,
        LoginScreen,
        LoginSent,
        MainMenu,
        PlayerCreation,
        PlayerDelete,
        EnterStart,
        EnterFinal,
        InGame
    }
}
