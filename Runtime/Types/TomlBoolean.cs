namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlBoolean : TomlValue
    {
        public bool Value { get; }

        public TomlBoolean(bool value) => Value = value;

        public override string ToString() => Value ? "true" : "false";
    }
}
