using System;
using System.Collections;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal class WrappedEnumerable<T> : IEnumerable<T>
    {
        public IEnumerable<T> collection;

        public WrappedEnumerable() => collection = new List<T>();

        public WrappedEnumerable(IEnumerable<T> values) => collection = values;
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator() => collection.GetEnumerator();
    }
}
