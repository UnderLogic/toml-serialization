using System;
using System.Collections;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml
{
    public sealed class TomlTableArray : TomlValue, IReadOnlyList<TomlTable>
    {
        private readonly List<TomlTable> _tables = new();

        public string Name { get; private set; }
        
        public int Count => _tables.Count;
        
        public TomlTable this[int index] => _tables[index];

        public TomlTableArray(string name) => Name = name.Trim();

        public TomlTableArray(string name, IEnumerable<TomlTable> values)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Table array name cannot be empty", nameof(name));

            if (values == null)
                throw new ArgumentNullException(nameof(values));

            Name = name.Trim();

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

        public IEnumerator<TomlTable> GetEnumerator() => _tables.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
