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

        public TomlStringBuilder AppendNullKeyValue(string key)
            => AppendNullValue(key).AppendLine();

        public TomlStringBuilder AppendKeyValue(string key, bool value) =>
            AppendKey(key).AppendBoolean(value).AppendLine();

        public TomlStringBuilder AppendKeyValue(string key, char value) =>
            AppendKey(key).AppendChar(value).AppendLine();

        public TomlStringBuilder AppendKeyValue(string key, string value) =>
            AppendKey(key).AppendString(value).AppendLine();

        public TomlStringBuilder AppendKeyValue<T>(string key, T value) where T : Enum =>
            AppendKey(key).AppendEnum(value).AppendLine();

        public TomlStringBuilder AppendKeyValue(string key, long value) =>
            AppendKey(key).AppendInteger(value).AppendLine();

        public TomlStringBuilder AppendKeyValue(string key, double value) =>
            AppendKey(key).AppendFloat(value).AppendLine();

        public TomlStringBuilder AppendKeyValue(string key, DateTime value,
            string dateFormat = IsoDateFormat)
            => AppendKey(key).AppendDateTime(value, dateFormat).AppendLine();

        #endregion

        #region Append Value Methods

        public TomlStringBuilder AppendBoolean(bool value) => Append(value.ToString().ToLowerInvariant());
        public TomlStringBuilder AppendChar(char value) => Append($"\"{value.ToString()}\"");
        public TomlStringBuilder AppendString(string value) => value != null ? Append($"\"{value}\"") : Append("null");
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

        public TomlStringBuilder AppendDateTime(DateTime value, string dateFormat = IsoDateFormat) =>
            Append(value.ToString(dateFormat));

        public TomlStringBuilder AppendObject(object obj, string dateFormat = IsoDateFormat)
        {
            if (obj == null)
                return Append("null");

            var t = obj.GetType();

            if (obj is bool boolValue)
                return AppendBoolean(boolValue);
            if (obj is char charValue)
                return AppendChar(charValue);
            if (obj is string stringValue)
                return AppendString(stringValue);
            if (t.IsEnum)
                return AppendEnum((Enum)obj);
            if (t == typeof(DateTime))
                return AppendDateTime((DateTime)obj, dateFormat);
            if (t == typeof(sbyte))
                return AppendInteger((sbyte)obj);
            if (t == typeof(short))
                return AppendInteger((short)obj);
            if (t == typeof(int))
                return AppendInteger((int)obj);
            if (t == typeof(long))
                return AppendInteger((long)obj);
            if (t == typeof(byte))
                return AppendInteger((byte)obj);
            if (t == typeof(ushort))
                return AppendInteger((ushort)obj);
            if (t == typeof(uint))
                return AppendInteger((uint)obj);
            if (t == typeof(float))
                return AppendFloat((float)obj);
            if (t == typeof(double))
                return AppendFloat((double)obj);

            throw new InvalidOperationException($"Type {t.Name} is not supported");
        }

        #endregion

        #region Append Array Methods

        public TomlStringBuilder AppendEmptyArray(string key) => AppendKey(key).AppendLine("[]");

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<bool> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendBoolean(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<char> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendChar(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<string> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendString(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray<T>(string key, IReadOnlyList<T> collection, bool multiline = false)
            where T : Enum
            => AppendKey(key).AppendArrayInternal(collection, value => AppendEnum(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<sbyte> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<short> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<int> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<long> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<byte> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<ushort> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<uint> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendInteger(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<float> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendFloat(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<double> collection, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendFloat(value), multiline).AppendLine();

        public TomlStringBuilder AppendArray(string key, IReadOnlyList<DateTime> collection,
            string dateFormat = IsoDateFormat, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendDateTime(value, dateFormat), multiline)
                .AppendLine();
        
        public TomlStringBuilder AppendArray(string key, IReadOnlyList<object> collection,
            string dateFormat = IsoDateFormat, bool multiline = false)
            => AppendKey(key).AppendArrayInternal(collection, value => AppendObject(value, dateFormat), multiline)
                .AppendLine();

        #endregion

        #region Append Inline Table Methods

        public TomlStringBuilder AppendEmptyInlineTable(string key) => AppendKey(key).AppendLine("{}");

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, bool> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendBoolean(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, char> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendChar(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, string> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendString(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable<T>(string key, IReadOnlyDictionary<string, T> dictionary)
            where T : Enum
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendEnum(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, sbyte> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendInteger(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, short> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendInteger(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, int> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendInteger(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, long> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendInteger(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, byte> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendInteger(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, ushort> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendInteger(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, uint> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendInteger(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, float> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendFloat(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, double> dictionary)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendFloat(value)).AppendLine();

        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, DateTime> dictionary,
            string dateFormat = IsoDateFormat)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendDateTime(value, dateFormat))
                .AppendLine();
        
        public TomlStringBuilder AppendInlineTable(string key, IReadOnlyDictionary<string, object> dictionary,
            string dateFormat = IsoDateFormat)
            => AppendKey(key).AppendInlineTableInternal(dictionary, value => AppendObject(value, dateFormat))
                .AppendLine();

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

        private TomlStringBuilder AppendArrayInternal<T>(IReadOnlyCollection<T> collection, Action<T> appendItem,
            bool multiline = false)
        {
            if (collection == null)
                return Append("null");

            if (collection.Count < 1)
                return Append("[]");

            Append(multiline ? "[" : "[ ");

            if (multiline)
                AppendLine();

            var counter = 0;
            foreach (var item in collection)
            {
                appendItem?.Invoke(item);

                if (multiline || counter++ < collection.Count - 1)
                    Append(", ");

                if (multiline)
                    AppendLine();
            }

            Append(multiline ? "]" : " ]");
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
