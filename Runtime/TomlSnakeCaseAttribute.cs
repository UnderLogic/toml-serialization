using System;

namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public sealed class TomlSnakeCaseAttribute : Attribute
    {
        public bool IsUpperCase { get; }

        public TomlSnakeCaseAttribute(bool isUpperCase = false) => IsUpperCase = isUpperCase;
    }
}
