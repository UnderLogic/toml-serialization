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

                if (TryDeserializeScalarField(tomlValue, field, obj))
                    continue;
                
                
            }
        }
        
        private static bool TryDeserializeScalarField(TomlValue tomlValue, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;
            
            // Deserialize a boolean value into the field
            if (fieldType == typeof(bool) && tomlValue is TomlBoolean boolValue)
            {
                field.SetValue(obj, boolValue.Value);
                return true;
            }

            // Deserialize a character into the field
            if (fieldType == typeof(char) && tomlValue is TomlString charValue)
            {
                if (string.IsNullOrEmpty(charValue.Value))
                    throw new InvalidOperationException("Cannot deserialize empty string to char");
                
                field.SetValue(obj, charValue.Value[0]);
                return true;
            }

            // Deserialize a string into the field
            if (fieldType == typeof(string))
            {
                // Handle null values
                if (tomlValue is TomlNull)
                {
                    field.SetValue(obj, null);
                    return true;
                }
                
                // Handle string values
                if (tomlValue is TomlString stringValue)
                {
                    field.SetValue(obj, stringValue.Value);
                    return true;
                }
            }

            // Deserialize an enum value into the field
            if (fieldType.IsEnum && tomlValue is TomlString enumValue)
            {
                // Try to parse the enum value, namely bitflags
                if (Enum.TryParse(fieldType, enumValue.Value, out var enumResult))
                {
                    field.SetValue(obj, enumResult);
                    return true;
                }
            }
            
            // Deserialize a signed 8-bit integer into the field
            if (fieldType == typeof(sbyte) && tomlValue is TomlInteger int8Value)
            {
                field.SetValue(obj, (sbyte)int8Value.Value);
                return true;
            }
            // Deserialize a signed 16-bit integer into the field
            if (fieldType == typeof(short) && tomlValue is TomlInteger int16Value)
            {
                field.SetValue(obj, (short)int16Value.Value);
                return true;
            }
            // Deserialize a signed 32-bit integer into the field
            if (fieldType == typeof(int) && tomlValue is TomlInteger int32Value)
            {
                field.SetValue(obj, (int)int32Value.Value);
                return true;
            }
            // Deserialize a signed 64-bit integer into the field
            if (fieldType == typeof(long) && tomlValue is TomlInteger int64Value)
            {
                field.SetValue(obj, int64Value.Value);
                return true;
            }
            
            // Deserialize an unsigned 8-bit integer into the field
            if (fieldType == typeof(byte) && tomlValue is TomlInteger uint8Value)
            {
                field.SetValue(obj, (byte)uint8Value.Value);
                return true;
            }
            // Deserialize an unsigned 16-bit integer into the field
            if (fieldType == typeof(ushort) && tomlValue is TomlInteger uint16Value)
            {
                field.SetValue(obj, (ushort)uint16Value.Value);
                return true;
            }
            // Deserialize an unsigned 32-bit integer into the field
            if (fieldType == typeof(uint) && tomlValue is TomlInteger uint32Value)
            {
                field.SetValue(obj, (uint)uint32Value.Value);
                return true;
            }
            
            // Deserialized a floating-point value into the field
            if (fieldType == typeof(float) && tomlValue is TomlFloat floatValue)
            {
                field.SetValue(obj, (float)floatValue.Value);
                return true;
            }
            // Deserialized a double-precision floating-point value into the field
            if (fieldType == typeof(double) && tomlValue is TomlInteger doubleValue)
            {
                field.SetValue(obj, doubleValue.Value);
                return true;
            }
            
            // Deserialize a date-time value into the field
            if (fieldType == typeof(DateTime) && tomlValue is TomlDateTime dateTimeValue)
            {
                field.SetValue(obj, dateTimeValue.Value);
                return true;
            }

            return false;
        }
    }
}
