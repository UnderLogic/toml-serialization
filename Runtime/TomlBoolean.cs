namespace UnderLogic.Serialization.Toml
{
    public sealed class TomlBoolean : TomlValue
    {
        public bool Value { get; }

        public TomlBoolean(bool value) => Value = value;
    }
}
