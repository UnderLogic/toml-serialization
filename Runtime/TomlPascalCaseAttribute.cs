using System;

namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public sealed class TomlPascalCaseAttribute : Attribute { }
}
