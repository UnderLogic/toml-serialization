using System;

namespace UnderLogic.Serialization.Toml
{
    public sealed class TomlDateTime : TomlValue
    {
        public DateTime Value { get; }
        
        public TomlDateTime(DateTime value) => Value = value;
    }
}
