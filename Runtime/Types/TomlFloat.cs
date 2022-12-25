using System.Globalization;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlFloat : TomlValue
    {
        public double Value { get; }

        public TomlFloat(double value) => Value = value;
        
        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}
