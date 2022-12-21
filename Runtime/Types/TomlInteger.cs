namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlInteger : TomlValue
    {
        public long Value { get; }

        public TomlInteger(long value) => Value = value;
    }
}
