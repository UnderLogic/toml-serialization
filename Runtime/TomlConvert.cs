using System;
using System.Collections;
using System.Collections.Generic;
using UnderLogic.Serialization.Toml.Types;

namespace UnderLogic.Serialization.Toml
{
    internal static class TomlConvert
    {
        public static bool TryIntoArray(TomlArray tomlArray, Type elementType, out Array arrayResult)
        {
            arrayResult = null;

            var array = Array.CreateInstance(elementType, tomlArray.Count);

            for (var index = 0; index < tomlArray.Count; index++)
            {
                var tomlValue = tomlArray[index];
                if (TryIntoScalar(tomlValue, elementType, out var scalarValue))
                    array.SetValue(scalarValue, index);
                else
                    return false;
            }

            arrayResult = array;
            return true;
        }

        public static bool TryIntoList(TomlArray tomlArray, Type elementType, out IList listResult)
        {
            listResult = null;

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(elementType);
            var list = (IList)Activator.CreateInstance(constructedListType);

            foreach (var tomlValue in tomlArray)
            {
                if (TryIntoScalar(tomlValue, elementType, out var scalarValue))
                    list.Add(scalarValue);
                else
                    return false;
            }

            listResult = list;
            return true;
        }

        public static bool TryIntoScalar(TomlValue tomlValue, Type type, out object result)
        {
            result = null;
            
            if (tomlValue is TomlNull)
                return true;

            if (tomlValue is TomlBoolean boolValue)
            {
                result = boolValue.Value;
                return true;
            }

            if (tomlValue is TomlString stringValue)
            {
                if (type == typeof(char))
                {
                    if (string.IsNullOrEmpty(stringValue.Value))
                        throw new InvalidOperationException("Cannot deserialize empty string to char");

                    result = stringValue.Value[0];
                    return true;
                }

                if (type == typeof(string) || type == typeof(object))
                {
                    result = stringValue.Value;
                    return true;
                }

                if (type.IsEnum)
                {
                    if (Enum.TryParse(type, stringValue.Value, false, out var enumValue))
                    {
                        result = enumValue;
                        return true;
                    }
                }
            }

            if (tomlValue is TomlInteger integerValue)
            {
                if (type == typeof(sbyte))
                {
                    result = (sbyte)integerValue.Value;
                    return true;
                }
                if (type == typeof(short))
                {
                    result = (short)integerValue.Value;
                    return true;
                }
                if (type == typeof(int))
                {
                    result = (int)integerValue.Value;
                    return true;
                }
                if (type == typeof(long) || type == typeof(object))
                {
                    result = integerValue.Value;
                    return true;
                }

                if (type == typeof(byte))
                {
                    result = (byte)integerValue.Value;
                    return true;
                }
                if (type == typeof(ushort))
                {
                    result = (ushort)integerValue.Value;
                    return true;
                }

                if (type == typeof(uint))
                {
                    result = (uint)integerValue.Value;
                    return true;
                }
            }

            if (tomlValue is TomlFloat floatValue)
            {
                if (type == typeof(float))
                {
                    result = (float)floatValue.Value;
                    return true;
                }

                if (type == typeof(double) || type == typeof(object))
                {
                    result = floatValue.Value;
                    return true;
                }
            }
            
            if (tomlValue is TomlDateTime dateTimeValue)
            {
                result = dateTimeValue.Value;
                return true;
            }

            return false;
        }
    }
}
