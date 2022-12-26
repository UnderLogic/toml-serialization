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
        #region Deserialize Public Methods
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
        #endregion

        #region DeserializeInto Public Methods
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
        #endregion

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

                if (tomlValue is TomlTable tomlTable)
                {
                    if (IsStringDictionary(fieldType))
                        DeserializeDictionaryField(tomlTable, field, obj);
                    else if (IsObjectType(fieldType))
                        DeserializeObjectField(tomlTable, field, obj);
                    else
                        throw new InvalidOperationException($"Type {fieldType.Name} is not supported");
                }
                else if (tomlValue is TomlTableArray tomlTableArray)
                {
                    if (fieldType.IsArray)
                        DeserializeObjectArrayField(tomlTableArray, field, obj);
                    else if (IsGenericList(fieldType))
                        DeserializeObjectListField(tomlTableArray, field, obj);
                }
                else if (tomlValue is TomlArray tomlArray)
                {
                    if (fieldType.IsArray)
                        DeserializeArrayField(tomlArray, field, obj);
                    else if (IsGenericList(fieldType))
                        DeserializeListField(tomlArray, field, obj);
                    else
                        throw new InvalidOperationException($"Type {fieldType.Name} is not supported");
                }
                else DeserializeScalarField(tomlValue, field, obj);
            }
        }

        private static void DeserializeObjectField(TomlTable tomlTable, FieldInfo field, object obj)
        {
            var fieldObj = Activator.CreateInstance(field.FieldType);
            DeserializeObject(tomlTable, fieldObj);
            field.SetValue(obj, fieldObj);
        }
        
        private static void DeserializeObjectArrayField(TomlTableArray tomlTableArray, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;
            var elementType = fieldType.GetElementType();
            
            var array = Array.CreateInstance(elementType, tomlTableArray.Count);
            for (var index = 0; index < tomlTableArray.Count; index++)
            {
                var table = tomlTableArray[index];
                var element = Activator.CreateInstance(elementType);
                
                DeserializeObject(table, element);
                array.SetValue(element, index);
            }
            field.SetValue(obj, array);
        }
        
        private static void DeserializeObjectListField(TomlTableArray tomlTableArray, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;
            var elementType = fieldType.GetGenericArguments()[0];
            
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(elementType);
            var list = (IList)Activator.CreateInstance(constructedListType);
            
            foreach (var table in tomlTableArray)
            {
                var element = Activator.CreateInstance(elementType);
                
                DeserializeObject(table, element);
                list.Add(element);
            }
            field.SetValue(obj, list);
        }

        private static void DeserializeScalarField(TomlValue tomlValue, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;
            
            if (TomlConvert.TryIntoScalar(tomlValue, fieldType, out var scalarValue))
                field.SetValue(obj, scalarValue);
            else
                throw new InvalidOperationException($"Unable to deserialize value into {field.Name}");
        }

        private static void DeserializeArrayField(TomlArray tomlArray, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;
            var elementType = fieldType.GetElementType();

            if (TomlConvert.TryIntoArray(tomlArray, elementType, out var arrayResult))
                field.SetValue(obj, arrayResult);
            else
                throw new InvalidOperationException($"Unable to deserialize array into {field.Name}");
        }

        private static void DeserializeListField(TomlArray tomlArray, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;
            var elementType = fieldType.GetGenericArguments()[0];

            if (TomlConvert.TryIntoList(tomlArray, elementType, out var listResult))
                field.SetValue(obj, listResult);
            else
                throw new InvalidOperationException($"Unable to deserialize list into {field.Name}");
        }

        private static void DeserializeDictionaryField(TomlTable tomlTable, FieldInfo field, object obj)
        {
            var fieldType = field.FieldType;
            var valueType = fieldType.GetGenericArguments()[1];
            
            if (TomlConvert.TryIntoDictionary(tomlTable, valueType, out var dictResult))
                field.SetValue(obj, dictResult);
            else
                throw new InvalidOperationException($"Unable to deserialize dictionary into {field.Name}");
        }
    }
}
