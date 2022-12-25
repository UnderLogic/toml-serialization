using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        private static bool IsObjectType(object value) => value != null && IsObjectType(value.GetType());
        private static bool IsObjectType(Type t) => Type.GetTypeCode(t) == TypeCode.Object;

        private static bool IsGenericList(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>);

        private static bool IsStringDictionary(Type t) => t.IsGenericType &&
                                                          t.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                                                          t.GetGenericArguments()[0] == typeof(string);
    }
}
