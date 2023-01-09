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

        public void Add(string key, TomlValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!_table.TryAdd(key, value))
                throw new InvalidOperationException($"Key {key} already exists in table");
        }

        public void AddPath(string keyPath, TomlValue value)
        {
            if (keyPath == null)
                throw new ArgumentNullException(nameof(keyPath));
            
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var components = keyPath.Split('.');
            if (components.Length < 2)
            {
                Add(components[0], value);
                return;
            }

            var tablePath = components.Take(components.Length - 1);
            var key = components.Last();
            
            var currentTable = this;
            foreach (var tableKey in tablePath)
            {
                if (!currentTable._table.TryGetValue(tableKey, out var tableValue))
                {
                    tableValue = new TomlTable();
                    currentTable.Add(tableKey, tableValue);
                }

                if (!(tableValue is TomlTable table))
                    throw new InvalidOperationException($"Key {tableKey} is not a table");

                currentTable = table;
            }
            
            currentTable.Add(key, value);
        }

        public bool TryGetValue(string key, out TomlValue value) => _table.TryGetValue(key, out value);

        public bool TryGetValuePath(string keyPath, out TomlValue value)
        {
            value = null;
            
            var components = keyPath.Split('.');
            if (components.Length < 2)
                return TryGetValue(components[0], out value);

            var tablePath = components.Take(components.Length - 1);
            var key = components.Last();

            var currentTable = this;
            foreach(var tableKey in tablePath)
            {
                if (!TryGetValue(tableKey, out var valueAtPath))
                    return false;
                
                if (valueAtPath is TomlTable childTable)
                    currentTable = childTable;
                else
                    return false;
            }
            
            return currentTable.TryGetValue(key, out value);
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
