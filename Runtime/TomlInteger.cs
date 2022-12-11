using System;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal class TomlInteger : TomlValue
    {
        public long Value { get; private set; }

        public TomlInteger(long value) => Value = value;

        public override string ToTomlString() => Value.ToString();
    }
}
