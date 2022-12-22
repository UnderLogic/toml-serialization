using System;
using System.IO;
using System.Reflection;
using System.Text;
using UnderLogic.Serialization.Toml.Types;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        public static T Deserialize<T>(string toml) where T : new()
        {
            if (toml == null)
                throw new ArgumentNullException(nameof(toml));

            using (var reader = new StringReader(toml))
                return Deserialize<T>(reader);
        }

        public static T Deserialize<T>(Stream stream, bool leaveOpen = true) where T : new()
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen))
                return Deserialize<T>(reader);
        }

        public static T Deserialize<T>(TextReader reader) where T : new()
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            var instance = new T();
            DeserializeInto(reader, instance);

            return instance;
        }

        public static void DeserializeInto(string toml, object obj)
        {
            if (toml == null)
                throw new ArgumentNullException(nameof(toml));

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            using (var reader = new StringReader(toml))
                DeserializeInto(reader, obj);
        }

        public static void DeserializeInto(Stream stream, object obj, bool leaveOpen = true)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            using (var reader = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen))
                DeserializeInto(reader, obj);
        }

        public static void DeserializeInto(TextReader reader, object obj)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            using (var tomlReader = new TomlReader(reader))
            {
                var rootTable = tomlReader.ReadDocument();
                DeserializeObject(rootTable, obj);
            }
        }

        private static void DeserializeObject(TomlTable table, object obj)
        {
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

                if (!table.TryGetValue(fieldKey, out var tomlValue))
                    continue;

                if (tomlValue is TomlTable)
                    continue;

                if (tomlValue is TomlTableArray)
                    continue;

                if (tomlValue is TomlArray tomlArray)
                {
                    DeserializeArrayField(tomlArray, field, obj);
                }
                else
                {
                    DeserializeScalarField(tomlValue, field, obj);
                }
            }
        }

        private static void DeserializeScalarField(TomlValue tomlValue, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;

            if (fieldType == typeof(bool) && tomlValue is TomlBoolean boolValue)
            {
                field.SetValue(obj, boolValue.Value);
            }
            else if (fieldType == typeof(char) && tomlValue is TomlString charValue)
            {
                if (string.IsNullOrEmpty(charValue.Value))
                    throw new InvalidOperationException("Cannot deserialize empty string to char");

                field.SetValue(obj, charValue.Value[0]);
            }
            else if (fieldType == typeof(string))
            {
                if (tomlValue is TomlNull)
                    field.SetValue(obj, null);
                else if (tomlValue is TomlString stringValue)
                    field.SetValue(obj, stringValue.Value);
            }
            else if (fieldType.IsEnum && tomlValue is TomlString enumValue)
            {
                if (Enum.TryParse(fieldType, enumValue.Value, out var enumResult))
                    field.SetValue(obj, enumResult);
            }
            else if (fieldType == typeof(sbyte) && tomlValue is TomlInteger int8Value)
            {
                field.SetValue(obj, (sbyte)int8Value.Value);
            }
            else if (fieldType == typeof(short) && tomlValue is TomlInteger int16Value)
            {
                field.SetValue(obj, (short)int16Value.Value);
            }
            else if (fieldType == typeof(int) && tomlValue is TomlInteger int32Value)
            {
                field.SetValue(obj, (int)int32Value.Value);
            }
            else if (fieldType == typeof(long) && tomlValue is TomlInteger int64Value)
            {
                field.SetValue(obj, int64Value.Value);
            }
            else if (fieldType == typeof(byte) && tomlValue is TomlInteger uint8Value)
            {
                field.SetValue(obj, (byte)uint8Value.Value);
            }
            else if (fieldType == typeof(ushort) && tomlValue is TomlInteger uint16Value)
            {
                field.SetValue(obj, (ushort)uint16Value.Value);
            }
            else if (fieldType == typeof(uint) && tomlValue is TomlInteger uint32Value)
            {
                field.SetValue(obj, (uint)uint32Value.Value);
            }
            else if (fieldType == typeof(float) && tomlValue is TomlFloat floatValue)
            {
                field.SetValue(obj, (float)floatValue.Value);
            }
            else if (fieldType == typeof(double) && tomlValue is TomlInteger doubleValue)
            {
                field.SetValue(obj, doubleValue.Value);
            }
            else if (fieldType == typeof(DateTime) && tomlValue is TomlDateTime dateTimeValue)
            {
                field.SetValue(obj, dateTimeValue.Value);
            }
        }

        private static void DeserializeArrayField(TomlArray tomlArray, FieldInfo field, object obj) { }
    }
}
