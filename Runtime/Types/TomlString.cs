using System;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlString : TomlValue
    {
        public static TomlString Empty = new TomlString(string.Empty);
        
        public string Value { get; private set; }

        public TomlString(string value) => Value = value;

        public override string ToTomlString() => Value != null ? $"\"{Value}\"" : "null";
    }
}
