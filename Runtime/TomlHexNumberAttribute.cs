using System;

namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TomlHexNumberAttribute : Attribute
    {
        public bool IsUpperCase { get; }

        public TomlHexNumberAttribute(bool isUpperCase = false) => IsUpperCase = isUpperCase;
    }
}
