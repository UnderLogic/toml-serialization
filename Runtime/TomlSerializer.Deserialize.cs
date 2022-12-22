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
                else if (TryDeserializeArrayValue(tomlArray, elementType, out var arrayValue))
                    array.SetValue(arrayValue, index);
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

        private static bool TryDeserializeArrayValue(TomlArray tomlArray, Type elementType, out object arrayValue)
        {
            arrayValue = null;

            var array = Array.CreateInstance(elementType, tomlArray.Count);
            
            for (var index = 0; index < tomlArray.Count; index++)
            {
                var tomlValue = tomlArray[index];
                if (TryDeserializeScalarValue(tomlValue, elementType, out var scalarValue))
                    array.SetValue(scalarValue, index);
                else
                    return false;
            }

            arrayValue = array;
            return true;
        }
        
        private static bool TryDeserializeScalarValue(TomlValue tomlValue, Type type, out object scalarValue)
        {
            scalarValue = null;

            if (tomlValue is TomlNull)
                return true;

            if (tomlValue is TomlBoolean boolValue)
            {
                scalarValue = boolValue.Value;
                return true;
            }

            if (tomlValue is TomlString stringValue)
            {
                if (type == typeof(char))
                {
                    if (string.IsNullOrEmpty(stringValue.Value))
                        throw new InvalidOperationException("Cannot deserialize empty string to char");

                    scalarValue = stringValue.Value[0];
                    return true;
                }

                if (type == typeof(string) || type == typeof(object))
                {
                    scalarValue = stringValue.Value;
                    return true;
                }

                if (type.IsEnum)
                {
                    if (Enum.TryParse(type, stringValue.Value, false, out var enumValue))
                    {
                        scalarValue = enumValue;
                        return true;
                    }
                }
            }

            if (tomlValue is TomlInteger integerValue)
            {
                if (type == typeof(sbyte))
                {
                    scalarValue = (sbyte)integerValue.Value;
                    return true;
                }
                if (type == typeof(short))
                {
                    scalarValue = (short)integerValue.Value;
                    return true;
                }
                if (type == typeof(int))
                {
                    scalarValue = (int)integerValue.Value;
                    return true;
                }
                if (type == typeof(long) || type == typeof(object))
                {
                    scalarValue = integerValue.Value;
                    return true;
                }

                if (type == typeof(byte))
                {
                    scalarValue = (byte)integerValue.Value;
                    return true;
                }
                if (type == typeof(ushort))
                {
                    scalarValue = (ushort)integerValue.Value;
                    return true;
                }

                if (type == typeof(uint))
                {
                    scalarValue = (uint)integerValue.Value;
                    return true;
                }
            }

            if (tomlValue is TomlFloat floatValue)
            {
                if (type == typeof(float))
                {
                    scalarValue = (float)floatValue.Value;
                    return true;
                }

                if (type == typeof(double) || type == typeof(object))
                {
                    scalarValue = floatValue.Value;
                    return true;
                }
            }
            
            if (tomlValue is TomlDateTime dateTimeValue)
            {
                scalarValue = dateTimeValue.Value;
                return true;
            }

            return false;
        }
    }
}
