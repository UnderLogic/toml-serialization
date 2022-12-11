using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal class WrappedValue<T>
    {
        public T value;

        public WrappedValue() { }

        public WrappedValue(T  initialValue) => value = initialValue;
    }
}
