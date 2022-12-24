namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlString : TomlValue
    {
        public string Value { get; }

        public TomlString(string value) => Value = value;

        public override string ToString() => Value != null ? "<null>" : $"\"{Value}\"";
    }
}
