using System;
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
            if (!Attribute.IsDefined(type, typeof(SerializableAttribute)))
                throw new ArgumentException($"Type {type.Name} is not serializable", nameof(obj));

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

            var isFirstField = true;
            
            foreach (var field in fields)
            {
                if (Attribute.IsDefined(field, typeof(NonSerializedAttribute)))
                    continue;

                if (!isFirstField)
                    writer.WriteLine();

                var fieldName = field.Name;
                var fieldType = field.FieldType;
                var value = field.GetValue(obj);

                if (IsScalarType(fieldType))
                    SerializeScalar(writer, fieldName, value);
                else if (TryCastEnumerable<bool>(value, out var boolArray))
                    SerializeScalarArray(writer, fieldName, boolArray);
                else if (TryCastEnumerable<char>(value, out var charArray))
                    SerializeScalarArray(writer, fieldName, charArray);
                else if (TryCastEnumerable<sbyte>(value, out var int8Array))
                    SerializeScalarArray(writer, fieldName, int8Array);
                else if (TryCastEnumerable<short>(value, out var int16Array))
                    SerializeScalarArray(writer, fieldName, int16Array);
                else if (TryCastEnumerable<int>(value, out var int32Array))
                    SerializeScalarArray(writer, fieldName, int32Array);
                else if (TryCastEnumerable<long>(value, out var int64Array))
                    SerializeScalarArray(writer, fieldName, int64Array);
                else if (TryCastEnumerable<byte>(value, out var uint8Array))
                    SerializeScalarArray(writer, fieldName, uint8Array);
                else if (TryCastEnumerable<ushort>(value, out var uint16Array))
                    SerializeScalarArray(writer, fieldName, uint16Array);
                else if (TryCastEnumerable<uint>(value, out var uint32Array))
                    SerializeScalarArray(writer, fieldName, uint32Array);
                else if (TryCastEnumerable<ulong>(value, out var uint64Array))
                    SerializeScalarArray(writer, fieldName, uint64Array);
                else if (TryCastEnumerable<float>(value, out var floatArray))
                    SerializeScalarArray(writer, fieldName, floatArray);
                else if (TryCastEnumerable<double>(value, out var doubleArray))
                    SerializeScalarArray(writer, fieldName, doubleArray);
                else if (TryCastEnumerable<string>(value, out var stringArray))
                    SerializeScalarArray(writer, fieldName, stringArray);
                else if (TryCastEnumerable<DateTime>(value, out var dateTimeArray))
                    SerializeScalarArray(writer, fieldName, dateTimeArray);
                else
                {
                    throw new ArgumentException(
                        $"Type {fieldType.Name} is not serializable for field '{fieldName}'");
                }

                isFirstField = false;
            }
        }

        private static void SerializeScalar(TextWriter writer, string propertyName, object value)
        {
            writer.WriteLine($"{propertyName} = {Stringify(value)}");
        }

        private static void SerializeScalarArray<T>(TextWriter writer, string propertyName, IEnumerable<T> collection)
        {
            var type = typeof(T);
            if (!IsScalarType(type))
                throw new ArgumentException($"Type {type.Name} is not a scalar type", nameof(collection));

            var values = collection.Select(Stringify);
            var arrayString = string.Join(", ", values);
            
            writer.WriteLine($"{propertyName} = [{arrayString}]");
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
                case string stringValue:
                    return $"\"{stringValue}\"";
                case DateTime dt:
                    return $"{dt:yyyy-MM-ddTHH:mm:ss.fffZ}";
            }
            
            if (type.IsPrimitive)
                return $"{value}";

            throw new ArgumentException($"Type {type.Name} is not stringifiable", nameof(value));
        }
    }
}
