namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlString : TomlValue
    {
        private readonly string _value;

        public TomlString(string value) => _value = value;

        public override string ToTomlString() => $"\"{_value}\"";
    }
}
