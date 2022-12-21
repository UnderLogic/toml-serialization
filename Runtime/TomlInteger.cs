namespace UnderLogic.Serialization.Toml
{
    public sealed class TomlInteger : TomlValue
    {
        public long Value { get; }

        public TomlInteger(long value) => Value = value;
    }
}
