using System;
using System.Globalization;
using System.Text;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures.Builders
{
    internal class TomlStringBuilder
    {
        private readonly StringBuilder _builder = new StringBuilder();

        public TomlStringBuilder AppendNullValue(string key)
            => Append($"{key} = null");
        
        public TomlStringBuilder AppendNaNValue(string key)
            => Append($"{key} = nan");
        
        public TomlStringBuilder AppendPositiveInfinityValue(string key)
            => Append($"{key} = +inf");
        
        public TomlStringBuilder AppendNegativeInfinityValue(string key)
            => Append($"{key} = -inf");
        
        public TomlStringBuilder AppendKeyValue(string key, bool value)
            => Append($"{key} = {value.ToString().ToLowerInvariant()}");

        public TomlStringBuilder AppendKeyValue(string key, char value)
            => Append($"{key} = \"{value.ToString()}\"");

        public TomlStringBuilder AppendKeyValue(string key, string value)
            => value != null ? Append($"{key} = \"{value}\"") : AppendNullValue(key);

        public TomlStringBuilder AppendKeyValue<T>(string key, T value) where T : Enum
            => Append($"{key} = {value:F}");
        
        public TomlStringBuilder AppendKeyValue(string key, long value)
            => Append($"{key} = {value}");
        
        public TomlStringBuilder AppendKeyValue(string key, double value)
            => Append($"{key} = {value.ToString(CultureInfo.InvariantCulture)}");
        
        public TomlStringBuilder AppendKeyValue(string key, DateTime value, string dateFormat = "yyyy-MM-dd HH:mm:ss.fffZ")
            => Append($"{key} = {value.ToString(dateFormat)}");

        public TomlStringBuilder Append(string value)
        {
            _builder.Append(value);
            return this;
        }

        public TomlStringBuilder AppendLine(string line = "")
        {
            _builder.AppendLine(line);
            return this;
        }

        public override string ToString() => _builder.ToString();
    }
}
