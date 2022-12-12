using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlArray : TomlValue, IEnumerable<TomlValue>
    {
        private readonly List<TomlValue> _values = new();

        public TomlArray() { }

        public TomlArray(IEnumerable<TomlValue> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            _values.AddRange(values);
        }

        public override string ToTomlString()
        {
            var valueStrings = this.Select(value => value.ToTomlString());
            return $"[ {string.Join(", ", valueStrings)} ]";
        }
        
        public IEnumerator<TomlValue> GetEnumerator() => _values.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
