using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableArray<T> : IReadOnlyList<T>
    {
        private readonly T[] _array;

        public int Count => _array.Length;
        public T this[int index] => _array[index];

        public SerializableArray() : this(Array.Empty<T>()) { }

        public SerializableArray(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            _array = collection.ToArray();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var value in _array)
                yield return value;
        }
        
        public static SerializableArray<T> WithValues(params T[] values) => new SerializableArray<T>(values);
    }
}