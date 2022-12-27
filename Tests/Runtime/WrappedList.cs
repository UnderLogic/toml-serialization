using System;
using System.Collections;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedList<T> : IList<T>
    {
        private List<T> _list;

        public bool IsNull => _list == null;
        public bool IsEmpty => _list != null && _list.Count == 0;

        public int Count => _list?.Count ?? 0;
        public bool IsReadOnly => false;

        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        private WrappedList() { }

        public WrappedList(IEnumerable<T> collection = null)
        {
            _list = collection != null ? new List<T>(collection) : new List<T>();
        }

        public int IndexOf(T item) => _list.IndexOf(item);
        public bool Contains(T item) => _list.Contains(item);
        public void Add(T item) => _list.Add(item);
        public void Insert(int index, T item) => _list.Insert(index, item);
        public bool Remove(T item) => _list.Remove(item);
        public void RemoveAt(int index) => _list.RemoveAt(index);
        public void Clear() => _list.Clear();
        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public static WrappedList<T> Empty() => new(null);

        public static WrappedList<T> Null() => new();

        public static WrappedList<T> FromValues(params T[] values) => new(values);

        public bool AreElementsSame(IReadOnlyList<T> other)
        {
            if (other == null)
                return false;

            if (Count != other.Count)
                return false;

            for (var i = 0; i < Count; i++)
            {
                if (!this[i].Equals(other[i]))
                    return false;
            }

            return true;
        }
    }
}
