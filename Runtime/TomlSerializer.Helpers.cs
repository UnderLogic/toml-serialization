using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        private static bool IsScalarType(Type t) =>
            t.IsPrimitive || t == typeof(string) || t == typeof(DateTime) || t.IsEnum;

        private static bool TryCastEnumerable<T>(object value, out IEnumerable<T> collection)
        {
            collection = null;

            if (value == null)
                return false;

            var type = value.GetType();
            
            // This is necessary because signed and unsigned integer will be casted to each other
            if (value is T[] array && type.GetElementType() == typeof(T))
            {
                collection = array;
                return true;
            }

            if (value is not IEnumerable<T> enumerable)
                return false;
            
            var enumerableType = enumerable.GetType();
            var typeArgs = enumerableType.GetGenericArguments();

            // This is necessary because signed and unsigned integer will be casted to each other
            if (typeArgs.Length < 1 || typeArgs[0] != typeof(T))
                return false;

            collection = enumerable;
            return true;
        }

        private static bool TryStringifyEnumValues(object value, out IEnumerable<string> collection)
        {
            collection = null;

            if (value == null)
                return false;

            var type = value.GetType();

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                if (elementType.IsEnum)
                {
                    var arrayObjects = (value as Array).OfType<object>();
                    collection = arrayObjects.Select(enumValue => $"{enumValue:F}");
                    return true;
                }
            }

            if (value is not IEnumerable enumerable)
                return false;

            var enumerableType = enumerable.GetType();
            var typeArgs = enumerableType.GetGenericArguments();

            if (typeArgs.Length < 1 || !typeArgs[0].IsEnum)
                return false;


            var enumObjects = enumerable.OfType<object>();
            collection = enumObjects.Select(enumValue => $"{enumValue:F}");
            return true;
        }

        private static bool TryCastDictionary<T>(object value, out IDictionary<string, T> dictionary)
        {
            dictionary = null;

            if (value is not IDictionary<string, T> dict)
                return false;

            var dictType = dict.GetType();
            var typeArgs = dictType.GetGenericArguments();

            // This is necessary because signed and unsigned integer will be casted to each other
            if (typeArgs.Length < 2 || typeArgs[0] != typeof(string) || typeArgs[1] != typeof(T))
                return false;
            
            dictionary = dict;
            return true;
        }
        
        private static bool TryStringifyEnumDictionary(object value, out IDictionary<string, string> dictionary)
        {
            dictionary = null;

            if (value is not IDictionary dict)
                return false;

            var dictType = dict.GetType();
            var typeArgs = dictType.GetGenericArguments();

            if (typeArgs.Length < 2 || typeArgs[0] != typeof(string) || !typeArgs[1].IsEnum)
                return false;

            var enumValueDict = new Dictionary<string, string>();
            foreach (var key in dict.Keys)
            {
                var enumValue = dict[key];
                enumValueDict.Add(key.ToString(), $"{enumValue:F}");
            }
            
            dictionary = enumValueDict;
            return true;
        }
    }
}
