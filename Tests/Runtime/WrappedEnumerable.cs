using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal class WrappedEnumerable<T> : IEnumerable<T>
    {
        private IEnumerable<T> _values;

        public IEnumerable<T> Values => _values;
        
        public WrappedEnumerable() => _values = new List<T>();

        public WrappedEnumerable(IEnumerable<T> values) => _values = values;
        
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<T> GetEnumerator() => _values.GetEnumerator();

        public string ToTomlStringArray(string key, Func<T, string> toString = null)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty or whitespace", nameof(key));

            var valueStrings = Values.Select(value => toString?.Invoke(value) ?? value.ToString());
            var arrayString = string.Join(", ", valueStrings);

            return $"{key} = [{arrayString}]";
        }
        
        public string ToTomlStringTableArray(string key, Func<T, string> toString = null)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be empty or whitespace", nameof(key));

            var sb = new StringBuilder();

            var isFirstItem = true;
            foreach (var value in Values)
            {
                if (!isFirstItem)
                    sb.AppendLine();

                sb.AppendLine($"[[{key}]]");

                var valueString = toString?.Invoke(value) ?? value.ToString();
                sb.Append(valueString);

                if (!valueString.EndsWith("\n"))
                    sb.AppendLine();

                isFirstItem = false;
            }

            return sb.ToString();
        }
    }
}
