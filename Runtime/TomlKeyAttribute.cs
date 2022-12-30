using System;
using System.Text.RegularExpressions;

namespace UnderLogic.Serialization.Toml
{
    [AttributeUsage(AttributeTargets.Field)]
    public class TomlKeyAttribute : Attribute
    {
        private static readonly Regex KeyRegex = new(@"^[a-z0-9-_]$", RegexOptions.IgnoreCase| RegexOptions.Compiled);
        
        private string Key { get; }

        public TomlKeyAttribute(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            if (!KeyRegex.IsMatch(key))
                throw new ArgumentException("Key must be a valid TOML key", nameof(key));

            Key = key;
        }
    }
}
