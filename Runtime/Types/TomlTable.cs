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

        public TomlTable(string name, TomlTable parent = null, IEnumerable<TomlKeyValuePair> keyValuePairs = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Table name cannot be empty");

            Name = name.Trim();
            Parent = parent;

            if (keyValuePairs == null)
                return;

            foreach (var kvp in keyValuePairs)
                Add(kvp.Key, kvp.Value);
        }

        public void Add(string key, TomlValue value)
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

        public TomlTableInline ToInlineTable() => new(this);

        public override string ToString() => $"[{GetType().Name}] Values = {_table.Count}";

        public IEnumerator<TomlKeyValuePair> GetEnumerator() =>
            _table.Select(pair => new TomlKeyValuePair(pair.Key, pair.Value)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
