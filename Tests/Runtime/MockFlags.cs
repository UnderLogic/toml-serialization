using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Flags]
    internal enum MockFlags
    {
        None = 0,
        Available = 0x1,
        InProgress = 0x2,
        Completed = 0x4,
        Cancelled = 0x8,
        All = Available | InProgress | Completed | Cancelled
    }
}
