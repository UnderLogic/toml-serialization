using System;

namespace UnderLogic.Serialization.Toml
{
    [Flags]
    internal enum ConvertFlags
    {
        None,
        ForceArray = 0x1,
        ForceList = 0x2,
    }
}
