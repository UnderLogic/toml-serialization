using System;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlDateTime : TomlValue
    {
        public DateTime Value { get; private set; }

        public TomlDateTime(DateTime value) => Value = value;

        public override string ToTomlString() => $"{Value:yyyy-MM-dd HH:mm:ss.fffZ}";
    }
}
