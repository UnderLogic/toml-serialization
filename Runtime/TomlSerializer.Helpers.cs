using System;

namespace UnderLogic.Serialization.Toml
{
    public static partial class TomlSerializer
    {
        public static bool IsScalarType(Type t) =>
            t.IsPrimitive || t.IsEnum || t == typeof(string) || t == typeof(DateTime);
    }
}
