using System;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal sealed class TomlBoolean : TomlValue
    {
        public static readonly TomlBoolean True = new TomlBoolean(true);
        public static readonly TomlBoolean False = new TomlBoolean(false);
        
        public bool Value { get; private set; }

        public TomlBoolean(bool value) => Value = value;

        public override string ToTomlString() => Value ? "true" : "false";
    }
}
