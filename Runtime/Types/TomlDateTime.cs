using System;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlDateTime : TomlValue
    {
        private readonly DateTime _value;
        
        public TomlDateTime(DateTime value) => _value = value;

        public override string ToTomlString() => $"{_value:yyyy-MM-dd HH:mm:ss.fffZ}";
    }
}
