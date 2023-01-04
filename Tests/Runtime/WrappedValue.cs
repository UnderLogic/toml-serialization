using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedValue<T>
    {
        private T _value;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public WrappedValue() { }

        public WrappedValue(T value) => _value = value;
    }
}
