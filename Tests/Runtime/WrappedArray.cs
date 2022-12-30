using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    [TomlCasing(StringCasing.Default)]
    internal sealed class WrappedArray<T> : IReadOnlyList<T>
    {
        private T[] _array;

        public bool IsNull => _array == null;
        public bool IsEmpty => _array != null && _array.Length == 0;

        public int Count => _array?.Length ?? -1;
        
        public T this[int index] => _array[index];

        private WrappedArray() => _array = Array.Empty<T>();
        
        private WrappedArray(T[] array) => _array = array;
        
        public WrappedArray(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            _array = collection.ToArray();
        }

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_array).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static WrappedArray<T> Empty() => new();

        public static WrappedArray<T> Null() => new(null);

        public static WrappedArray<T> FromValues(params T[] values) => new(values);
    }
}
