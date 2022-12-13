using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlArray : TomlValue, IEnumerable<TomlValue>
    {
        private readonly List<TomlValue> _values = new();

        public static TomlArray Empty => new();
        
        private TomlArray() { }
        
        public TomlArray(IEnumerable<TomlValue> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            foreach (var value in values)
            {
                // Arrays only support inline tables
                if (value is TomlTable table)
                    _values.Add(table.ToInlineTable());
                else
                    _values.Add(value);
            }
        }

        public override string ToTomlString()
        {
            if (_values.Count < 1)
                return "[]";
            
            var valueStrings = this.Select(value => value.ToTomlString());
            return $"[ {string.Join(", ", valueStrings)} ]";
        }
        
        public IEnumerator<TomlValue> GetEnumerator() => _values.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
