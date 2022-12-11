using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal class TomlTable
    {
        private readonly IDictionary<string, TomlValue> _values = new Dictionary<string, TomlValue>();
        
        public string Name { get; private set; }
        public TomlTable Parent { get; private set; }

        public bool IsRoot => Parent == null;

        public IEnumerable<TomlKeyValuePair> Values
        {
            get
            {
                foreach (var value in _values)
                    yield return new TomlKeyValuePair(value.Key, value.Value);
            }
        }

        public TomlTable(string name = "", TomlTable parent = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (parent != null && string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Child table must have a name");

            Name = name.Trim();
            Parent = parent;
        }

        public void AddTomlValue(string key, TomlValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (!_values.TryAdd(key, value))
                throw new InvalidOperationException($"Key {key} already exists in table");
        }
        
        public override string ToString() => $"[{GetType().Name}] Values = {_values.Count}";
    }
}
