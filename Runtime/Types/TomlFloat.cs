namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlFloat : TomlValue
    {
        public double Value { get; }

        public TomlFloat(double value) => Value = value;
    }
}
