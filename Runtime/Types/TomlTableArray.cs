using System;
using System.Collections;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlTableArray : TomlValue, IReadOnlyList<TomlTable>
    {
        private readonly List<TomlTable> _tables = new List<TomlTable>();

        public int Count => _tables.Count;
        
        public TomlTable this[int index] => _tables[index];

        public TomlTableArray() { }

        public TomlTableArray(IEnumerable<TomlTable> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            foreach (var table in values)
                _tables.Add(table);
        }

        public void Add(TomlTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            if (!_tables.Contains(table))
                _tables.Add(table);
        }

        public override string ToString() => $"Count = {Count}";

        public IEnumerator<TomlTable> GetEnumerator() => _tables.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
