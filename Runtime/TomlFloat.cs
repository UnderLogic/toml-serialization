using System;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal class TomlFloat : TomlValue
    {
        public double Value { get; private set; }

        public TomlFloat(double value) => Value = value;

        public override string ToTomlString() => Value.ToString();
    }
}
