namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlBoolean : TomlValue
    {
        private readonly bool _value;

        public TomlBoolean(bool value) => _value = value;

        public override string ToTomlString() => _value ? "true" : "false";
    }
}
