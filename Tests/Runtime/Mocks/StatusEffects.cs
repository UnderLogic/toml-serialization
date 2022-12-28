using System;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Flags]
    internal enum StatusEffects
    {
        None,
        Poison = 0x01,
        Blind = 0x02,
        Frozen = 0x04,
        Stun = 0x08,
        Sleep = 0x10,
        Silence = 0x20,
        Burn = 0x40,
        All = Poison | Blind | Frozen | Stun | Sleep | Silence | Burn
    }
}
