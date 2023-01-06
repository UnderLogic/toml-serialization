using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlArray : TomlValue, IReadOnlyList<TomlValue>
    {
        private readonly List<TomlValue> _values = new List<TomlValue>();

        public static TomlArray Empty => new TomlArray();
        
        private TomlArray() { }

        public bool IsMultiline { get; set; }

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

        public override string ToString()
        {
            if (Count < 1)
                return "[]";

            var valueStrings = _values.Select(value => value.ToString());
            return $"[ {string.Join(", ", valueStrings)} ]";
        }

        public IEnumerator<TomlValue> GetEnumerator() => _values.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
