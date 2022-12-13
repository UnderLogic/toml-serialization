using System;

namespace UnderLogic.Serialization.Toml
{
    [Flags]
    internal enum TomlSerializeFlags
    {
        None = 0x0,
        ForceInline = 0x1
    }
}
