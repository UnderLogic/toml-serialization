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

        private static void SerializeObject(TomlTable table, object obj) =>
            SerializeObject(table, obj, ConvertOptions.Default);
        
        private static void SerializeObject(TomlTable table, object obj, ConvertOptions options)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var type = obj.GetType();
            if (!type.IsSerializable)
                throw new InvalidOperationException($"Type {type.Name} is not serializable");

            // Allow the top-level object to specify a default field casing
            var objectFieldCasing = StringCasing.Default;
            if (TryGetAttribute<TomlCasingAttribute>(type, out var objectCasingAttribute))
                objectFieldCasing = objectCasingAttribute.Casing;

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.IsNotSerialized)
                    continue;

                var fieldCasing = objectFieldCasing;
                var fieldKey = field.Name.Trim('_');
                var fieldType = field.FieldType;
                var fieldValue = field.GetValue(obj);
                var fieldConvertOptions = options;

                // Allow the key to be renamed via the TomlKeyAttribute
                if (TryGetAttribute<TomlKeyAttribute>(field, out var keyAttribute))
                    fieldKey = keyAttribute.Key;
                else
                {
                    // Allow the field to specify a custom field casing
                    if (TryGetAttribute<TomlCasingAttribute>(field, out var fieldCasingAttribute))
                        fieldCasing = fieldCasingAttribute.Casing;

                    // Apply the field casing to the key
                    if (fieldCasing != StringCasing.Default)
                        fieldKey = fieldKey.ToCase(fieldCasing);
                }

                // Allow literal string format
                if (TryGetAttribute<TomlLiteralAttribute>(field, out _))
                    fieldConvertOptions.IsLiteral = true;

                // Allow multiline string format
                if (TryGetAttribute<TomlMultilineAttribute>(field, out _))
                    fieldConvertOptions.IsMultiline = true;

                // Allow forced inlining of tables
                if (TryGetAttribute<TomlInlineAttribute>(field, out _))
                    fieldConvertOptions.ForceInline = true;

                // Allow forced expansion of tables
                if (TryGetAttribute<TomlExpandAttribute>(field, out _))
                    fieldConvertOptions.ForceExpand = true;
                
                // Allow number formats
                if (TryGetAttribute<TomlHexNumberAttribute>(field, out var hexNumberAttribute))
                {
                    fieldConvertOptions.NumberFormat = hexNumberAttribute.IsUpperCase
                        ? NumberFormat.HexUpperCase
                        : NumberFormat.HexLowerCase;
                }
                else if (TryGetAttribute<TomlOctalNumberAttribute>(field, out _))
                    fieldConvertOptions.NumberFormat = NumberFormat.Octal;
                else if (TryGetAttribute<TomlBinaryNumberAttribute>(field, out _))
                    fieldConvertOptions.NumberFormat = NumberFormat.Binary;
                    
                var tomlValue = ConvertToTomlValue(fieldValue, fieldType, fieldConvertOptions);

                if (tomlValue == null)
                    throw new InvalidOperationException($"Type {type.Name} is not serializable");

                table.Add(fieldKey, tomlValue);
            }
        }

        private static TomlValue ConvertToTomlValue(object obj, Type type, ConvertOptions options)
        {
            if (obj == null)
                return TomlNull.Value;

            var numberFormat = options.NumberFormat;

            if (obj is IDictionary dictionary)
            {
                var dictionaryConvertOptions = options;
                
                if (!IsObjectDictionary(type) && !options.ForceExpand)
                    dictionaryConvertOptions.ForceInline = true;
                
                var tomlTable = ConvertToTomlTable(dictionary, dictionaryConvertOptions);

                if (tomlTable != null)
                    return tomlTable;
            }
            else if (obj is IList list)
            {
                var tomlArray = ConvertToTomlArray(list, options);
                if (tomlArray != null)
                    return tomlArray;
            }
            else if (IsComplexType(type))
            {
                var tomlTable = ConvertObjectToTomlTable(obj, options);

                if (tomlTable != null)
                    return tomlTable;
            }

            if (type == typeof(bool) && obj is bool boolValue)
                return new TomlBoolean(boolValue);
            if (type == typeof(char) && obj is char charValue)
                return new TomlString(charValue.ToString());
            if (type == typeof(string) && obj is string stringValue)
            {
                return new TomlString(stringValue)
                {
                    IsLiteral = options.IsLiteral,
                    IsMultiline = options.IsMultiline
                };
            }
            if (type.IsEnum && obj is Enum enumValue)
                return new TomlString(enumValue.ToString("F"));
            if (type == typeof(sbyte) && obj is sbyte int8Value)
                return new TomlInteger(int8Value, numberFormat);
            if (type == typeof(short) && obj is short int16Value)
                return new TomlInteger(int16Value, numberFormat);
            if (type == typeof(int) && obj is int int32Value)
                return new TomlInteger(int32Value, numberFormat);
            if (type == typeof(long) && obj is long int64Value)
                return new TomlInteger(int64Value, numberFormat);
            if (type == typeof(byte) && obj is byte uint8Value)
                return new TomlInteger(uint8Value, numberFormat);
            if (type == typeof(ushort) && obj is ushort uint16Value)
                return new TomlInteger(uint16Value, numberFormat);
            if (type == typeof(uint) && obj is uint uint32Value)
                return new TomlInteger(uint32Value, numberFormat);
            if (type == typeof(float) && obj is float floatValue)
                return new TomlFloat(floatValue);
            if (type == typeof(double) && obj is double doubleValue)
                return new TomlFloat(doubleValue);
            if (type == typeof(DateTime) && obj is DateTime dateTimeValue)
                return new TomlDateTime(dateTimeValue);

            return null;
        }

        private static TomlValue ConvertToTomlArray(IEnumerable values, ConvertOptions options)
        {
            IList<object> collection;

            if (values is IEnumerable<string> stringList)
                collection = stringList.Cast<object>().ToList();
            else
                collection = values.OfType<object>().ToList();

            if (collection.Count < 1)
                return TomlArray.Empty;

            if (collection.All(IsComplexType))
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

            // Do not propagate the multiline flag to the array elements
            var elementConvertOptions = options;
            elementConvertOptions.IsMultiline = false;
            
            var tomlValues = collection.Select(value =>
                ConvertToTomlValue(value, value?.GetType(), elementConvertOptions));

            return new TomlArray(tomlValues) { IsMultiline = options.IsMultiline };
        }

        private static TomlTable ConvertToTomlTable(IDictionary dictionary, ConvertOptions options)
        {
            var tomlTable = new TomlTable { IsInline = options.ForceInline };

            foreach (var innerKey in dictionary.Keys)
            {
                var innerKeyString = innerKey.ToString();
                var value = dictionary[innerKey];
                var valueType = value?.GetType();

                var tomlValue = ConvertToTomlValue(value, value?.GetType(), options);

                if (tomlValue == null)
                    throw new InvalidOperationException($"Type {valueType?.Name} is not serializable");

                tomlTable.Add(innerKeyString, tomlValue);
            }

            return tomlTable;
        }

        private static TomlTable ConvertObjectToTomlTable(object obj, ConvertOptions options)
        {
            var tomlTable = new TomlTable();
            SerializeObject(tomlTable, obj, options);

            return tomlTable;
        }
    }
}
