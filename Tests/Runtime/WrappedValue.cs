using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedValue<T>
    {
        private readonly T _value;

        public WrappedValue(T value) => _value = value;
    }
}
