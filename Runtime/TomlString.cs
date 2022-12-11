using System;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal class TomlString : TomlValue
    {
        public string Value { get; private set; }

        public TomlString(string value) => Value = value;

        public override string ToTomlString() => Value != null ? $"\"{Value}\\\"" : "null";
    }
}
