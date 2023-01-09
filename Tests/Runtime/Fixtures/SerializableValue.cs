using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableValue<T>
    {
        private T _value;

        public T Value => _value;

        public SerializableValue() : this(default) { }

        public SerializableValue(T value) => _value = value;

        public override string ToString() => $"Value = {Value}";

        public static implicit operator T(SerializableValue<T> wrappedValue) => wrappedValue.Value;

        public static implicit operator SerializableValue<T>(T value) => new SerializableValue<T>(value);
    }
}
