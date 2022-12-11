using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal class TomlTableInline : TomlValue
    {
        private readonly IDictionary<string, TomlValue> _table = new Dictionary<string, TomlValue>();

        public TomlTableInline() { }

        public TomlTableInline(IEnumerable<TomlKeyValuePair> keyValuePairs)
        {
            if (keyValuePairs == null)
                throw new ArgumentNullException(nameof(keyValuePairs));

            foreach (var pair in keyValuePairs)
                _table.Add(pair.Key, pair.Value);
        }
        
        public void AddTomlValue(string key, TomlValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!_table.TryAdd(key, value))
                throw new InvalidOperationException($"Key {key} already exists in table");
        }

        public override string ToTomlString()
        {
            var keyPairStrings = _table.Select(pair => $"{pair.Key} = {pair.Value.ToTomlString()}");
            return $"{string.Join(", ", keyPairStrings)}";
        }
    }
}
