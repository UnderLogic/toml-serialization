using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedRenamedValue<T>
    {
        [TomlKey("renamedValue")]
        private T _value;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public WrappedRenamedValue() { }

        public WrappedRenamedValue(T value)
        {
            _value = value;
        }
    }
}
