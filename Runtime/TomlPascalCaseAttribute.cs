using System;

namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field)]
    public sealed class TomlPascalCaseAttribute : Attribute { }
}
