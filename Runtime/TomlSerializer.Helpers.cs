using System;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        private static bool IsScalarType(Type t) =>
            t.IsPrimitive || t == typeof(string) || t == typeof(DateTime);
    }
}
