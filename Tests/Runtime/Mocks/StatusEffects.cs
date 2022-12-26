using System;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Flags]
    internal enum StatusEffects
    {
        None,
        Poison,
        Blind,
        Frozen,
        Stun,
        Sleep,
        Silence,
        Burn,
        All = Poison | Blind | Frozen | Stun | Sleep | Silence | Burn
    }
}
