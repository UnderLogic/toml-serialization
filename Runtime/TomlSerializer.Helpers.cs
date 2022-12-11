using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        private static bool IsScalarType(Type t) =>
            t.IsPrimitive || t == typeof(decimal) || t == typeof(string) || t == typeof(DateTime);
        
        private static bool TryCastEnumerable<T>(object value, out IEnumerable<T> collection)
        {
            collection = null;

            // This is necessary because signed and unsigned integer will be casted to each other
            if (value is T[] array && value.GetType().GetElementType() == typeof(T))
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
    }
}
