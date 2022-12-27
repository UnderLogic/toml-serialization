using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnderLogic.Serialization.Toml.Types;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        #region Serialize Public Methods
        public static string Serialize(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb, CultureInfo.InvariantCulture))
                Serialize(writer, obj);

            return sb.ToString();
        }

        public static void Serialize(Stream stream, object obj, bool leaveOpen = true)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen))
                Serialize(writer, obj);
        }

        public static void Serialize(TextWriter writer, object obj)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var rootTable = new TomlTable();
            SerializeObject(rootTable, obj);
            
            using (var tomlWriter = new TomlWriter(writer))
                tomlWriter.WriteDocument(rootTable);
        }
        #endregion

        private static void SerializeObject(TomlTable table, object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var type = obj.GetType();
            if (!type.IsSerializable)
                throw new InvalidOperationException($"Type {type.Name} is not serializable");

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.IsNotSerialized)
                    continue;

                var fieldKey = field.Name.Trim('_');
                var fieldType = field.FieldType;
                var fieldValue = field.GetValue(obj);

                var tomlValue = ConvertToTomlValue(fieldValue, fieldType);

                if (tomlValue == null)
                    throw new InvalidOperationException($"Type {type.Name} is not serializable");

                table.Add(fieldKey, tomlValue);
            }
        }

        private static TomlValue ConvertToTomlValue(object obj, Type type)
        {
            if (obj == null)
                return TomlNull.Value;

            if (obj is IDictionary dictionary)
            {
                var tomlTable = ConvertToTomlTableInline(dictionary);
                if (tomlTable != null)
                    return tomlTable;
            }
            else if (obj is IList list)
            {
                var tomlArray = ConvertToTomlArray(list);
                if (tomlArray != null)
                    return tomlArray;
            }
            else if (IsObjectType(type))
            {
                var tomlTable = ConvertToTomlTableExpanded(obj);
                
                if (tomlTable != null)
                    return tomlTable;
            }
            
            if (type == typeof(bool) && obj is bool boolValue)
                return new TomlBoolean(boolValue);
            if (type == typeof(char) && obj is char charValue)
                return new TomlString(charValue.ToString());
            if (type == typeof(string) && obj is string stringValue)
                return new TomlString(stringValue);
            if (type.IsEnum && obj is Enum enumValue)
                return new TomlString(enumValue.ToString("F"));
            if (type == typeof(sbyte) && obj is sbyte int8Value)
                return new TomlInteger(int8Value);
            if (type == typeof(short) && obj is short int16Value)
                return new TomlInteger(int16Value);
            if (type == typeof(int) && obj is int int32Value)
                return new TomlInteger(int32Value);
            if (type == typeof(long) && obj is long int64Value)
                return new TomlInteger(int64Value);
            if (type == typeof(byte) && obj is byte uint8Value)
                return new TomlInteger(uint8Value);
            if (type == typeof(ushort) && obj is ushort uint16Value)
                return new TomlInteger(uint16Value);
            if (type == typeof(uint) && obj is uint uint32Value)
                return new TomlInteger(uint32Value);
            if (type == typeof(float) && obj is float floatValue)
                return new TomlFloat(floatValue);
            if (type == typeof(double) && obj is double doubleValue)
                return new TomlFloat(doubleValue);
            if (type == typeof(DateTime) && obj is DateTime dateTimeValue)
                return new TomlDateTime(dateTimeValue);

            return null;
        }

        private static TomlValue ConvertToTomlArray(IEnumerable values)
        {
            IList<object> collection;

            if (values is IEnumerable<string> stringList)
                collection = stringList.Cast<object>().ToList();
            else
                collection = values.OfType<object>().ToList();

            if (collection.Count < 1)
                return TomlArray.Empty;

            if (collection.All(IsObjectType))
            {
                var tomlTableArray = new TomlTableArray();
                foreach (var value in collection)
                {
                    var tomlTable = new TomlTable();
                    SerializeObject(tomlTable, value);
                    tomlTableArray.Add(tomlTable);
                }

                return tomlTableArray;
            }

            var tomlValues = collection.Select(value =>
                ConvertToTomlValue(value, value?.GetType()));

            return new TomlArray(tomlValues);
        }

        private static TomlTable ConvertToTomlTableInline(IDictionary dictionary)
        {
            var tomlTable = new TomlTable { IsInline = true };
            foreach (var innerKey in dictionary.Keys)
            {
                var innerKeyString = innerKey.ToString();
                var value = dictionary[innerKey];
                var valueType = value?.GetType();

                var tomlValue = ConvertToTomlValue(value, value?.GetType());
                
                if (tomlValue == null)
                    throw new InvalidOperationException($"Type {valueType?.Name} is not serializable");

                tomlTable.Add(innerKeyString, tomlValue);
            }

            return tomlTable;
        }

        private static TomlTable ConvertToTomlTableExpanded(object obj)
        {
            var tomlTable = new TomlTable();
            SerializeObject(tomlTable, obj);

            return tomlTable;
        }
    }
}
