using System.Globalization;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlInteger : TomlValue
    {
        public long Value { get; }

        public TomlInteger(long value) => Value = value;

        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}
