using System;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlDateTime : TomlValue
    {
        public const string DefaultFormat = "yyyy-MM-dd HH:mm:ss.fffZ";
        
        public DateTime Value { get; }

        public string DateTimeFormat { get; set; } = DefaultFormat;

        public TomlDateTime(DateTime value) => Value = value;

        public override string ToString() => Value.ToString(DateTimeFormat);
    }
}
