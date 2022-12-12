using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlTable : TomlValue, ITomlTable
    {
        private readonly Dictionary<string, TomlValue> _table = new();

        public string Name { get; private set; }
        public TomlTable Parent { get; set; }

        public bool IsRoot => Parent == null;

        public TomlTable() => Name = string.Empty;

        public TomlTable(string name, TomlTable parent = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Table must have a name");

            Name = name.Trim();
            Parent = parent;
        }

        public void AddTomlValue(string key, TomlValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!_table.TryAdd(key, value))
                throw new InvalidOperationException($"Key {key} already exists in table");

            if (value is TomlTable childTable)
                childTable.Parent = this;
        }

        public override string ToTomlString()
        {
            var sb = new StringBuilder();

            if (!IsRoot)
                sb.AppendLine($"[{Name}]");

            foreach (var keyValuePair in this)
                sb.AppendLine(keyValuePair.ToTomlString());

            return sb.ToString();
        }

        public override string ToString() => $"[{GetType().Name}] Values = {_table.Count}";

        public IEnumerator<TomlKeyValuePair> GetEnumerator() =>
            _table.Select(pair => new TomlKeyValuePair(pair.Key, pair.Value)).GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
