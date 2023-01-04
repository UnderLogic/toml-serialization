using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        private static bool IsScalarType(object value) => value == null || IsScalarType(value.GetType());

        private static bool IsScalarType(Type type) =>
            type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(decimal) ||
            type == typeof(DateTime);

        private static bool IsComplexType(object value) => value != null && IsComplexType(value.GetType());

        private static bool IsComplexType(Type t) =>
            t != typeof(object) && !t.IsArray && Type.GetTypeCode(t) == TypeCode.Object;

        private static bool IsGenericList(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>);

        private static bool IsScalarDictionary(Type t) => t.IsGenericType &&
                                                          t.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                                                          t.GetGenericArguments()[0] == typeof(string) &&
                                                          IsScalarType(t.GetGenericArguments()[1]);

        private static bool IsMixedDictionary(Type t) => t.IsGenericType &&
                                                         t.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                                                         t.GetGenericArguments()[0] == typeof(string) &&
                                                         t.GetGenericArguments()[1] == typeof(object);
        
        private static bool IsObjectDictionary(Type t) => t.IsGenericType &&
                                                          t.GetGenericTypeDefinition() == typeof(Dictionary<,>) &&
                                                          t.GetGenericArguments()[0] == typeof(string) &&
                                                          IsComplexType(t.GetGenericArguments()[1]);
        
        private static bool HasDefaultConstructor(Type t) => t.IsValueType || t.GetConstructor(Type.EmptyTypes) != null;

        private static StringCasing GetStringCasing(MemberInfo memberInfo, StringCasing defaultCasing = StringCasing.Default)
        {
            if (TryGetAttribute<TomlCamelCaseAttribute>(memberInfo, out _))
                return StringCasing.CamelCase;
            if (TryGetAttribute<TomlPascalCaseAttribute>(memberInfo, out _))
                return StringCasing.PascalCase;
            if (TryGetAttribute<TomlSnakeCaseAttribute>(memberInfo, out var snakeCaseAttribute))
                return snakeCaseAttribute.IsUpperCase ? StringCasing.UpperSnakeCase : StringCasing.SnakeCase;
            if (TryGetAttribute<TomlKebabCaseAttribute>(memberInfo, out _))
                return StringCasing.KebabCase;

            return defaultCasing;
        }

        private static NumberFormat GetNumberFormat(MemberInfo memberInfo, NumberFormat defaultFormat = NumberFormat.Decimal)
        {
            if (TryGetAttribute<TomlHexNumberAttribute>(memberInfo, out var hexNumberAttribute))
                return hexNumberAttribute.IsUpperCase ? NumberFormat.HexUpperCase : NumberFormat.HexLowerCase;
            if (TryGetAttribute<TomlOctalNumberAttribute>(memberInfo, out _))
                return NumberFormat.Octal;
            if (TryGetAttribute<TomlBinaryNumberAttribute>(memberInfo, out _))
                return NumberFormat.Binary;

            return defaultFormat;
        }

        private static bool TryGetAttribute<T>(MemberInfo memberInfo, out T attribute) where T : Attribute
        {
            attribute = null;

            if (Attribute.GetCustomAttribute(memberInfo, typeof(T), false) is T attr)
            {
                attribute = attr;
                return true;
            }

            return false;
        }
    }
}
