using System;
using System.Collections;
using System.Collections.Generic;
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
                    if (fieldType.IsArray)
                        DeserializeArrayField(tomlArray, field, obj);
                    else if (fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>))
                        DeserializeListField(tomlArray, field, obj);
                }
                else
                {
                    if (TryDeserializeScalarValue(tomlValue, fieldType, out var scalarValue))
                        field.SetValue(obj, scalarValue);
                }
            }
        }

        private static void DeserializeArrayField(TomlArray tomlArray, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;

            var elementType = fieldType.GetElementType();
            var array = Array.CreateInstance(elementType, tomlArray.Count);

            for (var index = 0; index < tomlArray.Count; index++)
            {
                var tomlValue = tomlArray[index];
                if (TryDeserializeScalarValue(tomlValue, elementType, out var scalarValue))
                    array.SetValue(scalarValue, index);
            }

            field.SetValue(obj, array);
        }

        private static void DeserializeListField(TomlArray tomlArray, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;

            var elementType = fieldType.GetGenericArguments()[0];
            var list = (IList)Activator.CreateInstance(fieldType);

            foreach (var tomlValue in tomlArray)
            {
                if (TryDeserializeScalarValue(tomlValue, elementType, out var scalarValue))
                    list.Add(scalarValue);
            }

            field.SetValue(obj, list);
        }
        
        private static bool TryDeserializeScalarValue(TomlValue tomlValue, Type scalarType, out object scalarValue)
        {
            scalarValue = null;
            
            if (scalarType == typeof(bool) && tomlValue is TomlBoolean boolValue)
            {
                scalarValue = boolValue.Value;
                return true;
            }
            if (scalarType == typeof(char) && tomlValue is TomlString charValue)
            {
                if (string.IsNullOrEmpty(charValue.Value))
                    throw new InvalidOperationException("Cannot deserialize empty string to char");

                scalarValue = charValue.Value[0];
                return true;
            }
            if (scalarType == typeof(string))
            {
                if (tomlValue is TomlNull)
                    scalarValue = null;
                else if (tomlValue is TomlString stringValue)
                    scalarValue = stringValue.Value;

                return true;
            }
            if (scalarType.IsEnum && tomlValue is TomlString enumValue)
            {
                if (Enum.TryParse(scalarType, enumValue.Value, out var enumResult))
                {
                    scalarValue = enumResult;
                    return true;
                }
            }
            if (scalarType == typeof(sbyte) && tomlValue is TomlInteger int8Value)
            {
                scalarValue = (sbyte)int8Value.Value;
                return true;
            }
            if (scalarType == typeof(short) && tomlValue is TomlInteger int16Value)
            {
                scalarValue = (short)int16Value.Value;
                return true;
            }
            if (scalarType == typeof(int) && tomlValue is TomlInteger int32Value)
            {
                scalarValue = (int)int32Value.Value;
                return true;
            }
            if (scalarType == typeof(long) && tomlValue is TomlInteger int64Value)
            {
                scalarValue = int64Value.Value;
                return true;
            }
            if (scalarType == typeof(byte) && tomlValue is TomlInteger uint8Value)
            {
                scalarValue = (byte)uint8Value.Value;
                return true;
            }
            if (scalarType == typeof(ushort) && tomlValue is TomlInteger uint16Value)
            {
                scalarValue = (ushort)uint16Value.Value;
                return true;
            }
            if (scalarType == typeof(uint) && tomlValue is TomlInteger uint32Value)
            {
                scalarValue = (uint)uint32Value.Value;
                return true;
            } 
            if (scalarType == typeof(float) && tomlValue is TomlFloat floatValue)
            {
                scalarValue = (float)floatValue.Value;
                return true;
            }
            if (scalarType == typeof(double) && tomlValue is TomlInteger doubleValue)
            {
                scalarValue = doubleValue.Value;
                return true;
            }
            if (scalarType == typeof(DateTime) && tomlValue is TomlDateTime dateTimeValue)
            {
                scalarValue = dateTimeValue.Value;
                return true;
            }

            return false;
        }
    }
}
