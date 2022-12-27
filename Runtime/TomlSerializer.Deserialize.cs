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

                if (!TryConvertFromTomlValue(tomlValue, fieldType, out var parsedValue))
                    throw new InvalidOperationException(
                        $"Type {fieldType.Name} could not be deserialized for field {field.Name}");

                field.SetValue(obj, parsedValue);
            }
        }

        private static bool TryConvertFromTomlValue(TomlValue tomlValue, Type type, out object result)
        {
            result = null;

            if (tomlValue is TomlNull)
                return true;

            if (tomlValue is TomlTable tomlTable)
            {
                if (IsScalarDictionary(type) || IsMixedDictionary(type))
                {
                    var valueType = type.GetGenericArguments()[1];
                    if (!TryConvertToDictionary(tomlTable, valueType, out var dictResult))
                        return false;

                    result = dictResult;
                    return true;
                }

                if (IsComplexType(type))
                {
                    if (!HasDefaultConstructor(type))
                        return false;
                    
                    var instance = Activator.CreateInstance(type);
                    DeserializeObject(tomlTable, instance);

                    result = instance;
                    return true;
                }
            }
            else if (tomlValue is TomlTableArray tomlTableArray)
            {
                if (type.IsArray)
                {
                    if (!TryConvertToObjectArray(tomlTableArray, type.GetElementType(), out var arrayResult))
                        return false;

                    result = arrayResult;
                    return true;
                }

                if (IsGenericList(type))
                {
                    var elementType = type.GetGenericArguments()[0];
                    if (!TryConvertToObjectList(tomlTableArray, elementType, out var listResult))
                        return false;

                    result = listResult;
                    return true;
                }
            }
            else if (tomlValue is TomlArray tomlArray)
            {
                if (type.IsArray)
                {
                    if (!TryConvertToArray(tomlArray, type.GetElementType(), out var arrayResult))
                        return false;

                    result = arrayResult;
                    return true;
                }

                if (IsGenericList(type))
                {
                    var elementType = type.GetGenericArguments()[0];
                    if (!TryConvertToList(tomlArray, elementType, out var listResult))
                        return false;

                    result = listResult;
                    return true;
                }
            }
            else return TryConvertToScalar(tomlValue, type, out result);

            return false;
        }

        private static bool TryConvertToDictionary(TomlTable tomlTable, Type valueType, out IDictionary dictResult)
        {
            dictResult = null;

            var dictType = typeof(Dictionary<,>);
            var constructedDictType = dictType.MakeGenericType(typeof(string), valueType);
            var dict = (IDictionary)Activator.CreateInstance(constructedDictType);

            foreach (var keyValuePair in tomlTable)
            {
                if (TryConvertFromTomlValue(keyValuePair.Value, valueType, out var convertedValue))
                    dict.Add(keyValuePair.Key, convertedValue);
                else
                    return false;
            }

            dictResult = dict;
            return true;
        }

        private static bool TryConvertToObjectList(TomlTableArray tomlTableArray, Type elementType,
            out IList listResult)
        {
            listResult = null;

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(elementType);
            var list = (IList)Activator.CreateInstance(constructedListType);

            foreach (var tomlTable in tomlTableArray)
            {
                if (!HasDefaultConstructor(elementType))
                    return false;

                var instance = Activator.CreateInstance(elementType);
                DeserializeObject(tomlTable, instance);

                list.Add(instance);
            }

            listResult = list;
            return true;
        }

        private static bool TryConvertToList(TomlArray tomlArray, Type elementType, out IList listResult)
        {
            listResult = null;

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(elementType);
            var list = (IList)Activator.CreateInstance(constructedListType);
            
            foreach (var tomlValue in tomlArray)
            {
                if (TryConvertFromTomlValue(tomlValue, elementType, out var convertedValue))
                    list.Add(convertedValue);
                else
                    return false;
            }

            listResult = list;
            return true;
        }

        private static bool TryConvertToObjectArray(TomlTableArray tomlTableArray, Type elementType,
            out Array arrayResult)
        {
            arrayResult = null;
            
            var array = Array.CreateInstance(elementType, tomlTableArray.Count);
            for (var index = 0; index < tomlTableArray.Count; index++)
            {
                var tomlTable = tomlTableArray[index];
                if (!TryConvertFromTomlValue(tomlTable, elementType, out var convertedValue))
                    return false;

                array.SetValue(convertedValue, index);
            }

            arrayResult = array;
            return true;
        }
        
        private static bool TryConvertToArray(TomlArray tomlArray, Type elementType, out Array arrayResult)
        {
            arrayResult = null;

            var array = Array.CreateInstance(elementType, tomlArray.Count);
            for (var index = 0; index < tomlArray.Count; index++)
            {
                var tomlValue = tomlArray[index];
                if (TryConvertFromTomlValue(tomlValue, elementType, out var convertedValue))
                    array.SetValue(convertedValue, index);
                else
                    return false;
            }

            arrayResult = array;
            return true;
        }

        private static bool TryConvertToScalar(TomlValue tomlValue, Type type, out object scalarResult)
        {
            scalarResult = null;

            if (tomlValue is TomlNull)
                return true;

            if (tomlValue is TomlBoolean tomlBool)
                scalarResult = tomlBool.Value;
            else if (tomlValue is TomlString tomlString)
            {
                if (type == typeof(char))
                {
                    if (string.IsNullOrEmpty(tomlString.Value))
                        return false;
                    
                    scalarResult = tomlString.Value[0];
                }
                else if (type.IsEnum)
                {
                    if (!Enum.TryParse(type, tomlString.Value, false, out var enumValue))
                        return false;

                    scalarResult = enumValue;
                }
                else scalarResult = tomlString.Value;
            }
            else if (tomlValue is TomlInteger tomlInteger)
            {
                if (type == typeof(sbyte))
                    scalarResult = (sbyte)tomlInteger.Value;
                else if (type == typeof(short))
                    scalarResult = (short)tomlInteger.Value;
                else if (type == typeof(int))
                    scalarResult = (int)tomlInteger.Value;
                else if (type == typeof(byte))
                    scalarResult = (byte)tomlInteger.Value;
                else if (type == typeof(ushort))
                    scalarResult = (ushort)tomlInteger.Value;
                else if (type == typeof(uint))
                    scalarResult = (uint)tomlInteger.Value;
                else if (type == typeof(ulong))
                    return false;   // Not supported
                else if (type == typeof(float))
                    scalarResult = (float)tomlInteger.Value;
                else if (type == typeof(double))
                    scalarResult = (double)tomlInteger.Value;
                else
                    scalarResult = tomlInteger.Value;
            }
            else if (tomlValue is TomlFloat tomlFloat)
            {
                if (type == typeof(float))
                    scalarResult = (float)tomlFloat.Value;
                else if (type == typeof(decimal))
                    return false;   // Not supported
                else
                    scalarResult = tomlFloat.Value;
            }
            else if (tomlValue is TomlDateTime tomlDateTime)
                scalarResult = tomlDateTime.Value;

            return scalarResult != null;
        }
    }
}
