namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlFloat : TomlValue
    {
        private readonly double _value;

        public TomlFloat(double value) => _value = value;

        public override string ToTomlString() => _value.ToString();
    }
}
