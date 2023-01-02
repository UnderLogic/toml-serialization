using System;
namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TomlNumberFormatAttribute : Attribute
    {
        public NumberFormat NumberFormat { get; }
        
        public TomlNumberFormatAttribute(NumberFormat numberFormat)
        {
            NumberFormat = numberFormat;
        }
    }
}
