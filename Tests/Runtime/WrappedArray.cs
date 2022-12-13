using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class WrappedArray<T> : IEnumerable<T>
    {
        private readonly T[] _array;

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
