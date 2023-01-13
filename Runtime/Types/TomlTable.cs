using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlTable : TomlValue,  IEnumerable<TomlKeyValuePair>
    {
        private readonly Dictionary<string, TomlValue> _table = new Dictionary<string, TomlValue>();

        public static TomlTable Empty => new TomlTable();
        public static TomlTable EmptyInline => new TomlTable() { IsInline = true };
        
        public bool IsInline { get; set; }
        public int Count => _table.Count;

        public TomlTable() { }

        public TomlTable(IEnumerable<TomlKeyValuePair> keyValuePairs = null)
        {
            if (keyValuePairs == null)
                return;

            foreach (var kvp in keyValuePairs)
                Add(kvp.Key, kvp.Value);
        }

        public void Add(string keyPath, TomlValue value)
        {
            if (keyPath == null)
                throw new ArgumentNullException(nameof(keyPath));

            if (string.IsNullOrWhiteSpace(keyPath))
                throw new ArgumentException("Key path cannot be whitespace", nameof(keyPath));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var components = keyPath.Split('.', 2, StringSplitOptions.RemoveEmptyEntries);
            var key = components[0];

            // Not a dotted key, add normally to this table
            if (components.Length < 2)
            {
                if (!_table.TryAdd(key, value))
                    throw new InvalidOperationException($"Key {key} already exists in table");

                return;
            }

            // Dotted key, create a sub-table if it doesn't exist
            TomlTable childTable = null;
            if (_table.TryGetValue(key, out var existingValue))
            {
                if (existingValue is TomlTable table)
                    childTable = table;
                else if (existingValue is TomlTableArray tableArray)
                    childTable = tableArray.Last();
            }
            else
            {
                childTable = new TomlTable();
                _table.Add(key, childTable);
            }

            if (childTable == null)
                throw new InvalidOperationException($"Key {key} already exists in table and is not a table");

            var subKeyPath = keyPath.Substring(key.Length + 1);
            childTable.Add(subKeyPath, value);
        }

        public bool TryGetValue(string keyPath, out TomlValue value)
        {
            value = null;

            if (string.IsNullOrWhiteSpace(keyPath))
                return false;
            
            var components = keyPath.Split('.', 2, StringSplitOptions.RemoveEmptyEntries);
            var key = components[0];

            if (components.Length < 2)
                return _table.TryGetValue(key, out value);

            TomlTable childTable = null;
            if (_table.TryGetValue(key, out var existingValue))
            {
                if (existingValue is TomlTable table)
                    childTable = table;
                else if (existingValue is TomlTableArray tableArray)
                    childTable = tableArray.Last();
            }

            if (childTable == null)
                return false;
            
            var subKeyPath = keyPath.Substring(key.Length + 1);
            return childTable.TryGetValue(subKeyPath, out value);
        }

        public override string ToString()
        {
            if (!IsInline)
                return $"Count = {Count}";

            if (Count < 1)
                return "{}";

            var valueStrings = _table.Select(keyValuePair => $"{keyValuePair.Key} = {keyValuePair.Value}");
            return $"{{ {string.Join(", ", valueStrings)} }}";
        }

        public IEnumerator<TomlKeyValuePair> GetEnumerator() =>
            _table.Select(pair => new TomlKeyValuePair(pair.Key, pair.Value)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
