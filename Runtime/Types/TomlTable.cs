using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlTable : TomlValue,  IEnumerable<TomlKeyValuePair>
    {
        private readonly Dictionary<string, TomlValue> _table = new();

        public bool IsInline { get; set; }

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

        public override string ToTomlString()
        {
            if (_table.Count < 1)
                return "{}";
            
            var keyPairStrings = this.Select(kvp => kvp.ToTomlString());
            var inlineString = string.Join(", ", keyPairStrings);

            return $"{{ {inlineString} }}";
        }

        public override string ToString() => $"[{GetType().Name}] Values = {_table.Count}";

        public IEnumerator<TomlKeyValuePair> GetEnumerator() =>
            _table.Select(pair => new TomlKeyValuePair(pair.Key, pair.Value)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
