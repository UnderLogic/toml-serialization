using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableMultilineArray<T> : IReadOnlyList<T>
    {
        [TomlMultiline]
        private T[] _array;

        public int Count => _array.Length;
        public T this[int index] => _array[index];
        
        public bool IsNull => _array == null;

        public SerializableMultilineArray() : this(Array.Empty<T>()) { }

        public SerializableMultilineArray(IEnumerable<T> collection)
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
        
        public override string ToString() => $"Count = {Count}";

        public static SerializableMultilineArray<T> Null() => new SerializableMultilineArray<T> { _array = null };

        public static SerializableMultilineArray<T> Empty() => new SerializableMultilineArray<T>();

        public static SerializableMultilineArray<T> WithValues(params T[] values) => new SerializableMultilineArray<T>(values);
    }
}
