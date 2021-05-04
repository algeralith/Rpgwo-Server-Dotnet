using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server.Networking
{
    public enum PacketSecurity
    {
        None,
        Checksum,
        ChecksumRnd
    }
}
