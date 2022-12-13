using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlTableInline : TomlValue, ITomlTable
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

        public void Add(string key, TomlValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            // Inline tables can only contain other inline tables
            if (value is TomlTable table)
                value = table.ToInlineTable();
            
            if (!_table.TryAdd(key, value))
                throw new InvalidOperationException($"Key {key} already exists in table");
        }

        public override string ToTomlString()
        {
            if (_table.Count < 1)
                return "{}";
            
            var keyPairStrings = this.Select(pair => pair.ToTomlString());
            return $"{{ {string.Join(", ", keyPairStrings)} }}";
        }

        public TomlTable ToNamedTable(string name, TomlTable parent = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Table name cannot be empty", nameof(name));

            return new TomlTable(name, parent, this);
        }

        public IEnumerator<TomlKeyValuePair> GetEnumerator() =>
            _table.Select(pair => new TomlKeyValuePair(pair.Key, pair.Value)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
