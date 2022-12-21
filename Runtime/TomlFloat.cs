namespace UnderLogic.Serialization.Toml
{
    public sealed class TomlFloat : TomlValue
    {
        public double Value { get; }

        public TomlFloat(double value) => Value = value;
    }
}
