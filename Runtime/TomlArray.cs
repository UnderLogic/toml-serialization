using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal class TomlArray : TomlValue, IEnumerable<TomlValue>
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
    }
}
