using System;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlTableInline : TomlValue
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
            return $"{{{string.Join(", ", keyPairStrings)}}}";
        }
        
        // write a FromDictionary method that takes a dictionary of strings to bools, ints, doubles, strings, datetimes
        // also include all integer and float primitive types
        // include char and string types
        // should convert the dictionary to a TomlTableInline
        public static TomlTableInline FromDictionary(IDictionary<string, bool> dict) =>
            new (dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlBoolean(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, char> dict) =>
            new (dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlString(pair.Value.ToString()))));

        public static TomlTableInline FromDictionary(IDictionary<string, string> dict) =>
            new(dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlString(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, sbyte> dict) =>
            new(dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlInteger(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, short> dict) =>
            new(dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlInteger(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, int> dict) =>
            new (dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlInteger(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, long> dict) =>
            new (dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlInteger(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, byte> dict) =>
            new (dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlInteger(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, ushort> dict) =>
            new (dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlInteger(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, uint> dict) =>
            new (dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlInteger(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, float> dict) =>
            new (dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlFloat(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, double> dict) =>
            new (dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlFloat(pair.Value))));
        
        public static TomlTableInline FromDictionary(IDictionary<string, DateTime> dict) =>
            new(dict.Select(pair => new TomlKeyValuePair(pair.Key, new TomlDateTime(pair.Value))));
    }
}
