using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures.Builders
{
    internal class TomlStringBuilder
    {
        private const string IsoDateFormat = "yyyy-MM-dd HH:mm:ss.fffZ";

        private readonly StringBuilder _builder = new StringBuilder();

        #region Append Special Methods

        public TomlStringBuilder AppendNullValue(string key)
            => AppendKey(key).Append("null");

        public TomlStringBuilder AppendNaNValue(string key)
            => AppendKey(key).Append("nan");

        public TomlStringBuilder AppendPositiveInfinityValue(string key)
            => AppendKey(key).Append("+inf");

        public TomlStringBuilder AppendNegativeInfinityValue(string key)
            => AppendKey(key).Append("-inf");

        #endregion

        #region Append KeyValue Methods

        public TomlStringBuilder AppendKeyValue(string key, bool value) => AppendKey(key).AppendBoolean(value);
        public TomlStringBuilder AppendKeyValue(string key, char value) => AppendKey(key).AppendChar(value);
        public TomlStringBuilder AppendKeyValue(string key, string value) => AppendKey(key).AppendString(value);

        public TomlStringBuilder AppendKeyValue<T>(string key, T value) where T : Enum =>
            AppendKey(key).AppendEnum(value);

        public TomlStringBuilder AppendKeyValue(string key, long value) => AppendKey(key).AppendInteger(value);
        public TomlStringBuilder AppendKeyValue(string key, double value) => AppendKey(key).AppendFloat(value);

        public TomlStringBuilder AppendKeyValue(string key, DateTime value,
            string dateFormat = IsoDateFormat)
            => AppendKey(key).AppendDateTime(value, dateFormat);

        #endregion

        #region Append Value Methods

        public TomlStringBuilder AppendBoolean(bool value) => Append(value.ToString().ToLowerInvariant());
        public TomlStringBuilder AppendChar(char value) => Append($"\"{value.ToString()}\"");
        public TomlStringBuilder AppendString(string value) => Append($"\"{value}\"");
        public TomlStringBuilder AppendEnum<T>(T value) where T : Enum => Append($"\"{value:F}\"");
        public TomlStringBuilder AppendInteger(long value) => Append(value.ToString());

        public TomlStringBuilder AppendFloat(double value)
        {
            if (double.IsNaN(value))
                return Append("nan");
            if (double.IsPositiveInfinity(value))
                return Append("+inf");
            if (double.IsNegativeInfinity(value))
                return Append("-inf");

            return Append(value.ToString(CultureInfo.InvariantCulture));
        }

        public TomlStringBuilder AppendDateTime(DateTime value, string dateFormat = "yyyy-MM-dd HH:mm:ss.fffZ") =>
            Append(value.ToString(dateFormat));

        #endregion

        #region Append Array Methods

        public TomlStringBuilder AppendEmptyArray(string key) => AppendKey(key).Append("[]");

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<bool> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendBoolean(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<char> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendChar(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<string> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendString(value));

        public TomlStringBuilder AppendArray<T>(string key, IReadOnlyList<T> collection) where T : Enum
            => AppendKey(key).AppendArrayInternal(collection, value => AppendEnum(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<sbyte> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<short> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<int> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<long> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<byte> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<ushort> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<uint> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<float> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendFloat(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<double> collection)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendFloat(value));

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<DateTime> collection,
            string dateFormat = IsoDateFormat)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendDateTime(value, dateFormat));

        #endregion

        #region Append Inline Table Methods

        public TomlStringBuilder AppendEmptyInlineTable(string key) => AppendKey(key).Append("{}");

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, bool> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendBoolean(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, char> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendChar(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, string> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendString(value));

        public TomlStringBuilder AppendInlineTable<T>(string key, IReadOnlyDictionary<string, T> collection)
            where T : Enum
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendEnum(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, sbyte> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, short> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, int> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, long> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, byte> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, ushort> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, uint> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendInteger(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, float> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendFloat(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, double> collection)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendFloat(value));

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, DateTime> collection,
            string dateFormat = IsoDateFormat)
            => AppendKey(key).AppendInlineTableInternal(collection, value => AppendDateTime(value, dateFormat));

        #endregion

        public TomlStringBuilder AppendKey(string key) => Append($"{key} = ");

        public TomlStringBuilder AppendTableHeader(string key) => AppendLine($"[{key}]");

        public TomlStringBuilder AppendTableArrayHeader(string key) => AppendLine($"[[{key}]]");

        public TomlStringBuilder Append(string value)
        {
            _builder.Append(value);
            return this;
        }

        public TomlStringBuilder AppendLine(string line = "")
        {
            _builder.AppendLine(line);
            return this;
        }

        public override string ToString() => _builder.ToString();

        private TomlStringBuilder AppendArrayInternal<T>(IReadOnlyCollection<T> collection, Action<T> appendItem)
        {
            if (collection == null)
                return Append("null");

            if (collection.Count < 1)
                return Append("[]");

            Append("[ ");

            var counter = 0;
            foreach (var item in collection)
            {
                appendItem?.Invoke(item);

                if (counter++ < collection.Count - 1)
                    Append(", ");
            }

            Append(" ]");
            return this;
        }

        private TomlStringBuilder AppendInlineTableInternal<T>(IReadOnlyDictionary<string, T> dictionary,
            Action<T> appendItem)
        {
            if (dictionary == null)
                return Append("null");

            if (dictionary.Count < 1)
                return Append("{}");

            Append("{ ");

            var counter = 0;
            foreach (var kv in dictionary)
            {
                AppendKey(kv.Key);
                appendItem?.Invoke(kv.Value);

                if (counter++ < dictionary.Count - 1)
                    Append(", ");
            }

            Append(" }");
            return this;
        }
    }
}
