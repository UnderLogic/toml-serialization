using System;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal class TomlBoolean : TomlValue
    {
        public bool Value { get; private set; }

        public TomlBoolean(bool value) => Value = value;

        public override string ToTomlString() => Value ? "true" : "false";
    }
}
