namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlString : TomlValue
    {
        public bool IsLiteral { get; set; }
        public bool IsMultiline { get; set; }
        
        public string Value { get; }

        public TomlString(string value) => Value = value;

        public override string ToString() => Value != null ? $"\"{Value}\"" : "null";
    }
}
