namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlInteger : TomlValue
    {
        private readonly long _value;

        public TomlInteger(long value) => _value = value;

        public override string ToTomlString() => _value.ToString();
    }
}
