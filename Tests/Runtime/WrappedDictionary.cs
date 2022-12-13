using System;
using System.Collections;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedDictionary<TValue> : IEnumerable<KeyValuePair<string, TValue>>
    {
        private readonly Dictionary<string, TValue> _dictionary = new();

        private WrappedDictionary(Dictionary<string, TValue> dict) => _dictionary = dict;
        
        public WrappedDictionary(IEnumerable<KeyValuePair<string, TValue>> keyValuePairs = null)
        {
            if (keyValuePairs == null)
                return;

            foreach (var pair in keyValuePairs)
                Add(pair.Key, pair.Value);
        }

        public void Add(string key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            _dictionary.Add(key, value);
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public static WrappedDictionary<TValue> Null() => new(null);
    }
}
