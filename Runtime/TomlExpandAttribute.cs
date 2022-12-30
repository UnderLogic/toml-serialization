using System;

namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TomlExpandAttribute : Attribute { }
}
