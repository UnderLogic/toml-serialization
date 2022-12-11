using System;

namespace UnderLogic.Serialization.Toml
{
    [Serializable]
    internal class TomlTable
    {
        public string Name { get; private set; }
        public TomlTable Parent { get; private set; }

        public bool IsRoot => Parent == null;

        public TomlTable(string name = "", TomlTable parent = null)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (parent != null && string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Child table must have a name");

            Name = name.Trim();
            Parent = parent;
        }
    }
}
