using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlArray : TomlValue, IEnumerable<TomlValue>
    {
        public IReadOnlyCollection<TomlValue> Values { get; }

        public TomlArray() => Values = new List<TomlValue>();

        public TomlArray(IEnumerable<TomlValue> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            Values = values.ToList();
        }

        public override string ToTomlString()
        {
            var valueStrings = Values.Select(value => value.ToTomlString());
            return $"[{string.Join(", ", valueStrings)}]";
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<TomlValue> GetEnumerator() => Values.GetEnumerator();

        public static TomlArray FromEnumerable(IEnumerable<bool> collection) =>
            new (collection.Select(value => new TomlBoolean(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<char> collection) =>
            new (collection.Select(value => new TomlString(value.ToString())));
        
        public static TomlArray FromEnumerable(IEnumerable<string> collection) => 
            new (collection.Select(value => new TomlString(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<sbyte> collection) =>
            new (collection.Select(value => new TomlInteger(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<short> collection) =>
            new (collection.Select(value => new TomlInteger(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<int> collection) =>
            new (collection.Select(value => new TomlInteger(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<long> collection) =>
            new (collection.Select(value => new TomlInteger(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<byte> collection) =>
            new (collection.Select(value => new TomlInteger(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<ushort> collection) =>
            new (collection.Select(value => new TomlInteger(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<uint> collection) =>
            new (collection.Select(value => new TomlInteger(value)));

        public static TomlArray FromEnumerable(IEnumerable<float> collection) =>
            new (collection.Select(value => new TomlFloat(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<double> collection) =>
            new (collection.Select(value => new TomlFloat(value)));
        
        public static TomlArray FromEnumerable(IEnumerable<DateTime> collection) =>
            new (collection.Select(value => new TomlDateTime(value)));
    }
}
