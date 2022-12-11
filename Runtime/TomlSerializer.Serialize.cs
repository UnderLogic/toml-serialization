using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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

            var type = obj.GetType();

            if (!type.IsSerializable)
                throw new InvalidOperationException($"Type {type.Name} is not serializable");

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.IsNotSerialized)
                    continue;

                var fieldKey = field.Name;
                var value = field.GetValue(obj);

                SerializeField(writer, fieldKey, value);
            }
        }

        private static void SerializeField(TextWriter writer, string key, object value)
        {
            var fieldType = value.GetType();

            if (IsScalarType(fieldType))
                SerializeKeyValue(writer, key, value);
            else if (TryCastEnumerable<bool>(value, out var boolArray))
                SerializeScalarArray(writer, key, boolArray);
            else if (TryCastEnumerable<char>(value, out var charArray))
                SerializeScalarArray(writer, key, charArray);
            else if (TryCastEnumerable<sbyte>(value, out var int8Array))
                SerializeScalarArray(writer, key, int8Array);
            else if (TryCastEnumerable<short>(value, out var int16Array))
                SerializeScalarArray(writer, key, int16Array);
            else if (TryCastEnumerable<int>(value, out var int32Array))
                SerializeScalarArray(writer, key, int32Array);
            else if (TryCastEnumerable<long>(value, out var int64Array))
                SerializeScalarArray(writer, key, int64Array);
            else if (TryCastEnumerable<byte>(value, out var uint8Array))
                SerializeScalarArray(writer, key, uint8Array);
            else if (TryCastEnumerable<ushort>(value, out var uint16Array))
                SerializeScalarArray(writer, key, uint16Array);
            else if (TryCastEnumerable<uint>(value, out var uint32Array))
                SerializeScalarArray(writer, key, uint32Array);
            else if (TryCastEnumerable<ulong>(value, out var uint64Array))
                SerializeScalarArray(writer, key, uint64Array);
            else if (TryCastEnumerable<float>(value, out var floatArray))
                SerializeScalarArray(writer, key, floatArray);
            else if (TryCastEnumerable<double>(value, out var doubleArray))
                SerializeScalarArray(writer, key, doubleArray);
            else if (TryCastEnumerable<decimal>(value, out var decimalArray))
                SerializeScalarArray(writer, key, decimalArray);
            else if (TryCastEnumerable<string>(value, out var stringArray))
                SerializeScalarArray(writer, key, stringArray);
            else if (TryCastEnumerable<DateTime>(value, out var dateTimeArray))
                SerializeScalarArray(writer, key, dateTimeArray);
            else if (value is IEnumerable<object> objectArray)
            {
                SerializeObjectArray(writer, key, objectArray);
            }
            else
            {
                throw new InvalidOperationException($"Type {fieldType.Name} is not serializable");
            }
        }

        private static void SerializeKeyValue(TextWriter writer, string key, object value)
        {
            writer.WriteLine($"{key} = {Stringify(value)}");
        }

        private static void SerializeScalarArray<T>(TextWriter writer, string key, IEnumerable<T> collection)
        {
            var type = typeof(T);
            if (!IsScalarType(type))
                throw new ArgumentException($"Type {type.Name} is not a scalar type", nameof(collection));

            var values = collection.Select(Stringify);
            var arrayString = string.Join(", ", values);

            writer.WriteLine($"{key} = [{arrayString}]");
        }

        private static void SerializeObjectArray(TextWriter writer, string key, IEnumerable collection)
        {
            var isFirstItem = true;

            foreach (var obj in collection)
            {
                var type = obj.GetType();
                
                if (!type.IsSerializable)
                    throw new InvalidOperationException($"Type {type.Name} is not serializable");

                if (!isFirstItem)
                    writer.WriteLine();

                writer.WriteLine($"[[{key}]]");

                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
                foreach (var field in fields)
                {
                    if (field.IsNotSerialized)
                        continue;

                    var fieldKey = field.Name;
                    var value = field.GetValue(obj);

                    SerializeField(writer, fieldKey, value);
                }

                isFirstItem = false;
            }
        }

        private static string Stringify<T>(T value)
        {
            var type = value.GetType();

            switch (value)
            {
                case char charValue:
                    return $"'{charValue}'";
                case bool boolValue:
                    return $"{boolValue}".ToLowerInvariant();
                case decimal decimalValue:
                    return $"{decimalValue}";
                case string stringValue:
                    return $"\"{stringValue}\"";
                case DateTime dt:
                    return $"{dt:yyyy-MM-ddTHH:mm:ss.fffZ}";
            }

            if (type.IsPrimitive)
                return $"{value}";

            throw new InvalidOperationException($"Type {type.Name} cannot be converted to a string");
        }
    }
}
