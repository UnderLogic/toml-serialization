namespace UnderLogic.Serialization.Toml
{
    public sealed class TomlString : TomlValue
    {
        public string Value { get; }
    
        public TomlString(string value) => Value = value;
    }
}
