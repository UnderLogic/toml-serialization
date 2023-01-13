using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableInlineValue<T> where T : new()
    {
        [TomlInline]
        private T _value;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public SerializableInlineValue() : this(default) { }

        public SerializableInlineValue(T value)
        {
            _value = value;
        }
    }
}
