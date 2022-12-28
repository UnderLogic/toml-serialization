using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal class WrappedJaggedArray<T>
    {
        private T[][] _array;

        public bool IsNull => _array == null;
        public bool IsEmpty => _array != null && _array.Length == 0;
        
        public int Count => _array?.Length ?? -1;

        public T[] this[int index] => _array[index];

        private WrappedJaggedArray(int initialSize = 0)
        {
            _array = new T[initialSize][];
        }

        public WrappedJaggedArray(T[][] array)
        {
            _array = array;
        }
        
        public static WrappedJaggedArray<T> Empty() => new WrappedJaggedArray<T>(0);
        
        public static WrappedJaggedArray<T> Null() => new WrappedJaggedArray<T>(null);
    }
}
