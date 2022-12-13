using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlTableArray : TomlValue, IEnumerable<TomlTable>
    {
        private readonly List<TomlTable> _tables = new();

        public string Name { get; private set; }

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

        public override string ToTomlString()
        {
            var sb = new StringBuilder();

            var isFirstItem = true;

            foreach (var table in _tables)
            {
                if (!isFirstItem)
                    sb.AppendLine();

                sb.AppendLine($"[[{Name}]]");
                foreach (var keyValuePair in table)
                    sb.AppendLine(keyValuePair.ToTomlString());

                isFirstItem = false;
            }

            return sb.ToString();
        }

        public IEnumerator<TomlTable> GetEnumerator() => _tables.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
