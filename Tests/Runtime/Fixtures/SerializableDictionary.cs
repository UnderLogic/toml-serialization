using System;
using System.Collections;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableDictionary<TValue> : IReadOnlyDictionary<string, TValue>
    {
        private Dictionary<string, TValue> _dictionary;

        public int Count => _dictionary.Count;
        public TValue this[string key] => _dictionary[key];

        public IEnumerable<string> Keys => _dictionary.Keys;
        public IEnumerable<TValue> Values => _dictionary.Values;
        
        public bool IsNull => _dictionary == null;

        public SerializableDictionary()
        {
            _dictionary = new Dictionary<string, TValue>();
        }

        public SerializableDictionary(IEnumerable<KeyValuePair<string, TValue>> pairs)
            : this() { }

        public bool ContainsKey(string key) => _dictionary.ContainsKey(key);

        public bool TryGetValue(string key, out TValue value) => _dictionary.TryGetValue(key, out value);

        public void Add(string key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            _dictionary.Add(key, value);
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public override string ToString() => $"Count = {Count}";

        public static SerializableDictionary<TValue> Null() =>
            new SerializableDictionary<TValue>() { _dictionary = null };

        public static SerializableDictionary<TValue> Empty() =>
            new SerializableDictionary<TValue>();
    }
}
