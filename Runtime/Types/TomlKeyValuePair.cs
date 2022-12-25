using System;

namespace UnderLogic.Serialization.Toml.Types
{
    internal sealed class TomlKeyValuePair
    {
        public string Key { get; }
        public TomlValue Value { get; }

        public TomlKeyValuePair(string key, TomlValue value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public override string ToString() => $"{Key} = {Value}";
    }
}
