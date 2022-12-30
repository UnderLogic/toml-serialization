using System;
using System.Collections;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedDictionary<TValue> : IEnumerable<KeyValuePair<string, TValue>>
    {
        private Dictionary<string, TValue> _dictionary = new();

        public bool IsNull => _dictionary == null;
        public bool IsEmpty => _dictionary != null && _dictionary.Count == 0;
        
        public int Count => _dictionary?.Count ?? 0;
        
        public TValue this[string key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }
        
        private WrappedDictionary(Dictionary<string, TValue> dict) => _dictionary = dict;
        
        public WrappedDictionary(IEnumerable<KeyValuePair<string, TValue>> keyValuePairs = null)
        {
            if (keyValuePairs == null)
                return;

            foreach (var pair in keyValuePairs)
                Add(pair.Key, pair.Value);
        }

        public bool ContainsKey(string key) => _dictionary.ContainsKey(key);
        
        public void Add(string key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            _dictionary.Add(key, value);
        }

        public TValue Get(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return _dictionary[key];
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public static WrappedDictionary<TValue> Null() => new(null);
    }
}
