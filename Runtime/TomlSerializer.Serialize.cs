using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

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

                var key = field.Name;
                var value = field.GetValue(obj);

                // Boolean Values
                if (value is bool boolValue)
                    table.AddTomlValue(key, new TomlBoolean(boolValue));
                // Character & String Values
                else if (value is char charValue)
                    table.AddTomlValue(key, new TomlString(charValue.ToString()));
                else if (value is string stringValue)
                    table.AddTomlValue(key, new TomlString(stringValue));
                // Signed Integer Values
                else if (value is sbyte int8Value)
                    table.AddTomlValue(key, new TomlInteger(int8Value));
                else if (value is short int16Value)
                    table.AddTomlValue(key, new TomlInteger(int16Value));
                else if (value is int int32Value)
                    table.AddTomlValue(key, new TomlInteger(int32Value));
                else if (value is long int64Value)
                    table.AddTomlValue(key, new TomlInteger(int64Value));
                // Unsigned Integer Values
                else if (value is byte uint8Value)
                    table.AddTomlValue(key, new TomlInteger(uint8Value));
                else if (value is ushort uint16Value)
                    table.AddTomlValue(key, new TomlInteger(uint16Value));
                else if (value is uint uint32Value)
                    table.AddTomlValue(key, new TomlInteger(uint32Value));
                // Float Values
                else if (value is float floatValue)
                    table.AddTomlValue(key, new TomlFloat(floatValue));
                else if (value is double doubleValue)
                    table.AddTomlValue(key, new TomlFloat(doubleValue));
                // Date Time Values
                else if (value is DateTime dateTimeValue)
                    table.AddTomlValue(key, new TomlDateTime(dateTimeValue));
                // Bool Arrays
                else if (value is IEnumerable<bool> boolCollection)
                    table.AddTomlValue(key, TomlArray.FromEnumerable(boolCollection));
                // Character & String Arrays
                else if (value is IEnumerable<char> charCollection)
                    table.AddTomlValue(key, TomlArray.FromEnumerable(charCollection));
                else if (value is IEnumerable<string> stringCollection)
                    table.AddTomlValue(key, TomlArray.FromEnumerable(stringCollection));
                // Signed Integer Arrays
                else if (TryCastEnumerable<sbyte>(value, out var int8Collection))
                    table.AddTomlValue(key, TomlArray.FromEnumerable(int8Collection));
                else if (TryCastEnumerable<short>(value, out var int16Collection))
                    table.AddTomlValue(key, TomlArray.FromEnumerable(int16Collection));
                else if (TryCastEnumerable<int>(value, out var int32Collection))
                    table.AddTomlValue(key, TomlArray.FromEnumerable(int32Collection));
                else if (TryCastEnumerable<long>(value, out var int64Collection))
                    table.AddTomlValue(key, TomlArray.FromEnumerable(int64Collection));
                // Unsigned Integer Arrays
                else if (TryCastEnumerable<byte>(value, out var uint8Collection))
                    table.AddTomlValue(key, TomlArray.FromEnumerable(uint8Collection));
                else if (TryCastEnumerable<ushort>(value, out var uint16Collection))
                    table.AddTomlValue(key, TomlArray.FromEnumerable(uint16Collection));
                else if (TryCastEnumerable<uint>(value, out var uint32Collection))
                    table.AddTomlValue(key, TomlArray.FromEnumerable(uint32Collection));
                // Float Arrays
                else if (TryCastEnumerable<float>(value, out var floatCollection))
                    table.AddTomlValue(key, TomlArray.FromEnumerable(floatCollection));
                else if (TryCastEnumerable<double>(value, out var doubleCollection))
                    table.AddTomlValue(key, TomlArray.FromEnumerable(doubleCollection));
                // Date Time Arrays
                else if (value is IEnumerable<DateTime> dateTimeCollection)
                    table.AddTomlValue(key, TomlArray.FromEnumerable(dateTimeCollection));
                // Object Arrays
                else if (value is IEnumerable<object> objectCollection)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    throw new InvalidOperationException($"Type {type.Name} is not serializable");
                }
            }
        }
        
        private static void WriteTable(TextWriter writer, TomlTable table)
        {
            foreach (var keyPair in table.Values)
            {
                if (!table.IsRoot)
                    writer.WriteLine($"[{table.Name}]");
                
                writer.WriteLine(keyPair.ToTomlString());
            }
        }
    }
}
