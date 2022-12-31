using System;
using System.Text.RegularExpressions;

namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class TomlKeyAttribute : Attribute
    {
        private static readonly Regex KeyRegex = new(@"^[a-z0-9-_]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private string _key;

        public string Key
        {
            get => _key;
            private set => _key = KeyRegex.IsMatch(value) ? value : throw new ArgumentException("Invalid TOML key");
        }

        public TomlKeyAttribute(string key)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
        }
    }
}
