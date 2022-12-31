using System;

namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field)]
    public sealed class TomlCasingAttribute : Attribute
    {
        public StringCasing Casing { get; }

        public TomlCasingAttribute(StringCasing casing)
        {
            Casing = casing;
        }
    }
}
