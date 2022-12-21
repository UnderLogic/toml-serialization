using System;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlDateTime : TomlValue
    {
        public DateTime Value { get; }
        
        public TomlDateTime(DateTime value) => Value = value;
    }
}
