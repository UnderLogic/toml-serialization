using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        private static bool IsComplexType(object value) => value != null && IsComplexType(value.GetType());
        private static bool IsComplexType(Type t) => t != typeof(object) && Type.GetTypeCode(t) == TypeCode.Object;

        private static bool IsGenericList(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>);

        private static bool IsStringDictionary(Type t) => t.IsGenericType &&
                                                          t.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                                                          t.GetGenericArguments()[0] == typeof(string);

        private static bool IsObjectDictionary(Type t) => t.IsGenericType &&
                                                          t.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                                                          IsComplexType(t.GetGenericArguments()[1]);
    }
}
