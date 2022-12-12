using System;

namespace UnderLogic.Serialization.Toml.Types
{
    [Serializable]
    internal sealed class TomlKeyValuePair
    {
        public string Key { get; private set; }
        public TomlValue Value { get; private set; }
        
        public TomlKeyValuePair(string key, TomlValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            if(string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty or whitespace", nameof(key));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Key = key;
            Value = value;
        }
        
        public string ToTomlString() => $"{Key} = {Value.ToTomlString()}";
        
        public override string ToString() => $"[{GetType().Name}] {ToTomlString()}";
    }
}
