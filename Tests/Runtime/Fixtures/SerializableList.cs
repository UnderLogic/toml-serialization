using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableList<T> : IReadOnlyList<T>
    {
        private List<T> _list;

        public int Count => _list.Count;
        public T this[int index] => _list[index];

        public SerializableList() : this(Enumerable.Empty<T>()) { }

        public SerializableList(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            _list = collection.ToList();
        }

        public void Add(T item) => _list.Add(item);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

        public static SerializableList<T> Null() => new SerializableList<T> { _list = null };

        public static SerializableList<T> Empty() => new SerializableList<T>();

        public static SerializableList<T> WithValues(params T[] values) => new SerializableList<T>(values);
    }
}
