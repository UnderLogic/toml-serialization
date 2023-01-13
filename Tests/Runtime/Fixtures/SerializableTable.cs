using System;
using System.Collections;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableTable<TValue> : IReadOnlyDictionary<string, TValue>
    {
        [TomlExpand]
        private Dictionary<string, TValue> _table;

        public int Count => _table.Count;
        public TValue this[string key] => _table[key];

        public IEnumerable<string> Keys => _table.Keys;
        public IEnumerable<TValue> Values => _table.Values;

        public bool IsNull => _table == null;

        public SerializableTable()
        {
            _table = new Dictionary<string, TValue>();
        }

        public SerializableTable(IEnumerable<KeyValuePair<string, TValue>> pairs)
            : this() { }

        public bool ContainsKey(string key) => _table.ContainsKey(key);

        public bool TryGetValue(string key, out TValue value) => _table.TryGetValue(key, out value);

        public void Add(string key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            _table.Add(key, value);
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator() => _table.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => $"Count = {Count}";

        public static SerializableTable<TValue> Null() =>
            new SerializableTable<TValue>() { _table = null };

        public static SerializableTable<TValue> Empty() =>
            new SerializableTable<TValue>();
    }
}
