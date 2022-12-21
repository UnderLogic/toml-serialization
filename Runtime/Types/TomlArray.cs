using System;
using System.Collections;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlArray : TomlValue, IReadOnlyList<TomlValue>
    {
        private readonly List<TomlValue> _values = new();

        public static TomlArray Empty => new();
        
        private TomlArray() { }

        public int Count => _values.Count;
        
        public TomlValue this[int index] => _values[index];
        
        public TomlArray(IEnumerable<TomlValue> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            foreach (var value in values)
            {
                // Arrays can only contain inline tables
                if (value is TomlTable table)
                {
                    table.IsInline = true;
                    _values.Add(table);
                }
                else _values.Add(value);
            }
        }

        public IEnumerator<TomlValue> GetEnumerator() => _values.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
