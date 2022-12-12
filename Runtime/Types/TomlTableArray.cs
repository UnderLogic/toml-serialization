using System;
using System.Collections.Generic;
using System.Text;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlTableArray : TomlValue
    {
        private readonly List<TomlTable> _tables = new();

        public string Name { get; private set; }
        public IReadOnlyCollection<TomlTable> Tables => _tables;

        public TomlTableArray(string name) => Name = name.Trim();

        public TomlTableArray(string name, IEnumerable<TomlTable> values)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Table array must have a name", nameof(name));
            
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            
            Name = name.Trim();

            foreach (var table in values)
                _tables.Add(table);
        }

        public void AddTable(TomlTable table)
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
                foreach (var keyValuePair in table.Values)
                    sb.AppendLine(keyValuePair.ToTomlString());

                isFirstItem = false;
            }

            return sb.ToString();
        }
    }
}