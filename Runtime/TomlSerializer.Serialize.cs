using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using UnderLogic.Serialization.Toml.Types;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        public static string Serialize(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb, CultureInfo.InvariantCulture))
                Serialize(writer, obj);

            return sb.ToString();
        }

        public static void Serialize(Stream stream, object obj, Encoding encoding = null)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var tomlString = Serialize(obj);
            var bytes = (encoding ?? Encoding.UTF8).GetBytes(tomlString);
            stream.Write(bytes);
        }

        public static void Serialize(TextWriter writer, object obj)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            
            var rootTable = new TomlTable();
            SerializeObject(rootTable, obj);
            
            WriteTable(writer, rootTable);
        }

        private static void SerializeObject(TomlTable table, object obj)
        {
            var type = obj.GetType();

            if (!type.IsSerializable)
                throw new InvalidOperationException("$Type {type.Name} is not serializable");

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.IsNotSerialized)
                    continue;

                var key = field.Name.Trim('_');
                var value = field.GetValue(obj);

                if (value is null)
                    table.AddTomlValue(key, TomlNull.Value);
                else if (TrySerializeValue(value, out var tomlScalar))
                    table.AddTomlValue(key, tomlScalar);
                else if (TrySerializeArray(value, out var tomlArray))
                    table.AddTomlValue(key, tomlArray);
                else if (TrySerializeTableArray(value, key, out var tomlTableArray))
                    table.AddTomlValue(key, tomlTableArray);
                else if (TrySerializeTableInline(value, key, out var tomlTableInline))
                    table.AddTomlValue(key, tomlTableInline);
                else if (TrySerializeTableStandard(value, key, out var tomlTable))
                    table.AddTomlValue(key, tomlTable);
                else
                    throw new InvalidOperationException($"Type {type.Name} is not serializable");
            }
        }

        private static bool TrySerializeValue(object value, out TomlValue tomlValue)
        {
            tomlValue = null;
            
            // Null Value
            if (value == null)
                tomlValue = TomlNull.Value;
            // Boolean Values
            else if (value is bool boolValue)
                tomlValue = new TomlBoolean(boolValue);
            // Character & String Values
            else if (value is char charValue)
                tomlValue = new TomlString(charValue.ToString());
            else if (value is string stringValue)
                tomlValue = new TomlString(stringValue);
            // Signed Integer Values
            else if (value is sbyte int8Value)
                tomlValue = new TomlInteger(int8Value);
            else if (value is short int16Value)
                tomlValue = new TomlInteger(int16Value);
            else if (value is int int32Value)
                tomlValue = new TomlInteger(int32Value);
            else if (value is long int64Value)
                tomlValue = new TomlInteger(int64Value);
            // Unsigned Integer Values
            else if (value is byte uint8Value)
                tomlValue = new TomlInteger(uint8Value);
            else if (value is ushort uint16Value)
                tomlValue = new TomlInteger(uint16Value);
            else if (value is uint uint32Value)
                tomlValue = new TomlInteger(uint32Value);
            // Float Values
            else if (value is float floatValue)
                tomlValue = new TomlFloat(floatValue);
            else if (value is double doubleValue)
                tomlValue = new TomlFloat(doubleValue);
            // DateTime Values
            else if (value is DateTime dateTimeValue)
                tomlValue = new TomlDateTime(dateTimeValue);
            else
                return false;

            return tomlValue != null;
        }

        private static bool TrySerializeArray(object value, out TomlValue tomlArray)
        {
            tomlArray = null;

            // Null
            if (value is null)
                tomlArray = TomlNull.Value;
            // Bool Arrays
            else if (value is IEnumerable<bool> boolCollection)
                tomlArray = TomlArray.FromEnumerable(boolCollection);
            else if (value is IEnumerable<char> charCollection)
                tomlArray = TomlArray.FromEnumerable(charCollection);
            else if (value is IEnumerable<string> stringCollection)
                tomlArray = TomlArray.FromEnumerable(stringCollection);
            else if (TryCastEnumerable<sbyte>(value, out var int8Collection))
                tomlArray = TomlArray.FromEnumerable(int8Collection);
            else if (TryCastEnumerable<short>(value, out var int16Collection))
                tomlArray = TomlArray.FromEnumerable(int16Collection);
            else if (TryCastEnumerable<int>(value, out var int32Collection))
                tomlArray = TomlArray.FromEnumerable(int32Collection);
            else if (TryCastEnumerable<long>(value, out var int64Collection))
                tomlArray = TomlArray.FromEnumerable(int64Collection);
            // Unsigned Integer Arrays
            else if (TryCastEnumerable<byte>(value, out var uint8Collection))
                tomlArray = TomlArray.FromEnumerable(uint8Collection);
            else if (TryCastEnumerable<ushort>(value, out var uint16Collection))
                tomlArray = TomlArray.FromEnumerable(uint16Collection);
            else if (TryCastEnumerable<uint>(value, out var uint32Collection))
                tomlArray = TomlArray.FromEnumerable(uint32Collection);
            // Float Arrays
            else if (TryCastEnumerable<float>(value, out var floatCollection))
                tomlArray = TomlArray.FromEnumerable(floatCollection);
            else if (TryCastEnumerable<double>(value, out var doubleCollection))
                tomlArray = TomlArray.FromEnumerable(doubleCollection);
            // Date Time Arrays
            else if (value is IEnumerable<DateTime> dateTimeCollection)
                tomlArray = TomlArray.FromEnumerable(dateTimeCollection);
            else
                return false;

            return tomlArray != null;
        }

        private static bool TrySerializeTableArray(object value, string key, out TomlTableArray tomlTableArray)
        {
            tomlTableArray = null;

            if (value is not IEnumerable<object> objectCollection)
                return false;
            
            var tableArray = new TomlTableArray(key);

            foreach (var childObj in objectCollection)
            {
                var childTable = new TomlTable(key);
                SerializeObject(childTable, childObj);

                tableArray.AddTable(childTable);
            }

            tomlTableArray = tableArray;
            return true;
        }
        
        private static bool TrySerializeTableInline(object value, string key, out TomlValue tomlTable)
        {
            tomlTable = null;
            
            // Null
            if (value is null)
                tomlTable = TomlNull.Value;
            // Bool Dictionaries
            else if (TryCastDictionary<bool>(value, out var boolDictionary))
                tomlTable = TomlTableInline.FromDictionary(boolDictionary);
            // Character & String Dictionaries
            else if (TryCastDictionary<char>(value, out var charDictionary))
                tomlTable = TomlTableInline.FromDictionary(charDictionary);
            else if (TryCastDictionary<string>(value, out var stringDictionary))
                tomlTable = TomlTableInline.FromDictionary(stringDictionary);
            // Signed Integer Dictionaries
            else if (TryCastDictionary<sbyte>(value, out var int8Dictionary))
                tomlTable = TomlTableInline.FromDictionary(int8Dictionary);
            else if (TryCastDictionary<short>(value, out var int16Dictionary))
                tomlTable = TomlTableInline.FromDictionary(int16Dictionary);
            else if (TryCastDictionary<int>(value, out var int32Dictionary))
                tomlTable = TomlTableInline.FromDictionary(int32Dictionary);
            else if (TryCastDictionary<long>(value, out var int64Dictionary))
                tomlTable = TomlTableInline.FromDictionary(int64Dictionary);
            // Unsigned Integer Dictionaries
            else if (TryCastDictionary<byte>(value, out var uint8Dictionary))
                tomlTable = TomlTableInline.FromDictionary(uint8Dictionary);
            else if (TryCastDictionary<ushort>(value, out var uint16Dictionary))
                tomlTable = TomlTableInline.FromDictionary(uint16Dictionary);
            else if (TryCastDictionary<uint>(value, out var uint32Dictionary))
                tomlTable = TomlTableInline.FromDictionary(uint32Dictionary);
            // Float Dictionaries
            else if (TryCastDictionary<float>(value, out var floatDictionary))
                tomlTable = TomlTableInline.FromDictionary(floatDictionary);
            else if (TryCastDictionary<double>(value, out var doubleDictionary))
                tomlTable = TomlTableInline.FromDictionary(doubleDictionary);
            // DateTime Dictionaries
            else if (TryCastDictionary<DateTime>(value, out var dateTimeDictionary))
                tomlTable = TomlTableInline.FromDictionary(dateTimeDictionary);
            // Object
            else
            {
                var table = new TomlTableInline();
            }
                
            return tomlTable != null;
        }
        
        private static bool TrySerializeTableStandard(object value, string key, out TomlTable tomlTable)
        {
            tomlTable = null;
            
            if (value is not IDictionary<string, object> objectDictionary)
                return false;
            
            var table = new TomlTable(key);
            foreach (var keyPairValue in objectDictionary)
            {
                var childTable = new TomlTable(keyPairValue.Key);
            }

            tomlTable = table;
            return true;
        }

        private static void WriteTable(TextWriter writer, TomlTable table)
        {
            foreach (var keyValuePair in table)
            {
                if (!table.IsRoot)
                    writer.WriteLine($"[{table.Name}]");

                var key = keyValuePair.Key;
                var value = keyValuePair.Value;

                if (value is TomlNull)
                    writer.WriteLine($"{key} = null");
                else if (value is TomlTableArray tableArray)
                    writer.Write(tableArray.ToTomlString());
                else
                    writer.WriteLine(keyValuePair.ToTomlString());
            }
        }
    }
}
