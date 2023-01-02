using System.Globalization;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlInteger : TomlValue
    {
        public long Value { get; }

        public NumberFormat NumberFormat { get; }

        public TomlInteger(long value, NumberFormat numberFormat = NumberFormat.Decimal)
        {
            Value = value;
            NumberFormat = numberFormat;
        }

        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}
