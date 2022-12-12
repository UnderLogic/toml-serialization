using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal class WrappedValue<T>
    {
        private T _value;

        public T Value => _value;

        public WrappedValue() { }

        public WrappedValue(T  initialValue) => _value = initialValue;

        public string ToTomlStringKeyValuePair(string key, Func<T, string> toString = null)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty or whitespace", nameof(key));

            return $"{key} = {toString?.Invoke(Value) ?? Value.ToString()}\n";
        }
    }
}
