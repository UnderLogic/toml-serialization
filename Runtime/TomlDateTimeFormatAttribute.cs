using System;

namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TomlDateTimeFormatAttribute : Attribute
    {
        public string DateTimeFormat { get; set; }

        public TomlDateTimeFormatAttribute(string dateTimeFormat)
        {
            if (string.IsNullOrWhiteSpace(dateTimeFormat))
                throw new ArgumentException("Date time format cannot be null or whitespace.", nameof(dateTimeFormat));

            DateTimeFormat = dateTimeFormat;
        }
    }
}
