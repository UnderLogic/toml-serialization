using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class WrappedDictionary<TValue>
    {
        private readonly IDictionary<string, TValue> _dictionary = new Dictionary<string, TValue>();

        public void Add(string key, TValue value) => _dictionary.TryAdd(key, value);

        public string ToTomlStringInline(string key, Func<TValue, string> toString = null)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty or whitespace", nameof(key));

            var pairStrings = _dictionary.Select(pair =>
                $"{pair.Key} = {toString?.Invoke(pair.Value) ?? pair.Value.ToString()}");
            var inlineString = string.Join(", ", pairStrings);
            
            return $"{key} = {{{inlineString}}}";
        }
    }
}
