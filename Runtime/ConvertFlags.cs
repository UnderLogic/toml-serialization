using System;

namespace UnderLogic.Serialization.Toml
{
    [Flags]
    internal enum ConvertFlags
    {
        None,
        Literal = 0x1,
        Multiline = 0x2,
        ForceArray = 0x4,
        ForceList = 0x8,
        ForceInline = 0x10,
        ForceExpand = 0x20,
        HexNumberLowerCase = 0x40,
        HexNumberUpperCase = 0x80,
        OctalNumber = 0x100,
        BinaryNumber = 0x200,
    }
}
