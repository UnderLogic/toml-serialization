using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class WrappedValue : WrappedValue<object>
    {
        public WrappedValue(object value) : base(value) {}
    }
    
    [Serializable]
    public class WrappedValue<T>
    {
        public T value;

        public WrappedValue() { }

        public WrappedValue(T  initialValue) => value = initialValue;
    }
}
