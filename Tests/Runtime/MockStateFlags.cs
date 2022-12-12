using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Flags]
    internal enum MockStateFlags
    {
        None = 0x0,
        Pending = 0x1,
        InProgress = 0x2,
        Completed = 0x4,
        Cancelled = 0x8,
        All = Pending | InProgress | Completed | Cancelled
    }
}
