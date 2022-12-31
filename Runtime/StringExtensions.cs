using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnderLogic.Serialization.Toml
{
    internal static class StringExtensions
    {
        private static readonly Regex UnicodeCharRegex =
            new(@"\\u([0-9a-f]{2,4})", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex WordSplitRegex =
            new(@"(?<=[a-z])(?=[A-Z])|(?<=[A-Z])(?=[A-Z][a-z])|(?<=\W)(?=\w)|(?<=\w)(?=\W)|(?<=_)(?=\w)|(?<=\w)(?=_)",
                RegexOptions.Compiled);
        private static readonly Regex WordRegex = new(@"^[a-z0-9]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static string EscapeChar(this string text, char escapeChar)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            return text.Replace(escapeChar.ToString(), $"\\{escapeChar}");
        }

        public static string EscapeWhitespace(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            return text
                .Replace("\t", "\\t")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n");
        }

        public static string UnescapeTomlString(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            return text
                .Replace("\\\\", "\\")
                .Replace("\\\"", "\"")
                .Replace("\\t", "\t")
                .Replace("\\r", "\r")
                .Replace("\\n", "\n");
        }

        public static string ToCase(this string text, StringCasing casing)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            switch (casing)
            {
                case StringCasing.LowerCase:
                    return text.ToLowerInvariant();
                case StringCasing.UpperCase:
                    return text.ToUpperInvariant();
                case StringCasing.CamelCase:
                    return text.ToCamelCase();
                case StringCasing.PascalCase:
                    return text.ToPascalCase();
                case StringCasing.SnakeCase:
                    return text.ToSnakeCase();
                case StringCasing.KebabCase:
                    return text.ToKebabCase();
                default:
                    return text;
            }
        }

        public static string ToCamelCase(this string text)
            => string.Join(string.Empty,
                EnumerateWords(text).Select((word, index) => index == 0 ? LowerFirst(word) : UpperFirst(word)));

        public static string ToPascalCase(this string text)
            => string.Join(string.Empty, EnumerateWords(text).Select(UpperFirst));

        public static string ToSnakeCase(this string text)
            => string.Join('_', EnumerateWords(text).Select(word => word.ToLowerInvariant()));

        public static string ToKebabCase(this string text)
            => string.Join('-', EnumerateWords(text).Select(word => word.ToLowerInvariant()));

        public static string LowerFirst(this string text)
            => !string.IsNullOrWhiteSpace(text) ? text.Substring(0, 1).ToLowerInvariant() + text.Substring(1) : text;

        public static string UpperFirst(this string text)
            => !string.IsNullOrWhiteSpace(text) ? text.Substring(0, 1).ToUpperInvariant() + text.Substring(1) : text;

        public static IEnumerable<string> EnumerateWords(this string text)
            => WordSplitRegex.Split(text)
                .Where(word => !string.IsNullOrEmpty(word))
                .Where(word => WordRegex.IsMatch(word));

        public static string DecodeUnicodeChars(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            return UnicodeCharRegex.Replace(text, match =>
            {
                var hexValue = match.Groups[1].Value;
                return int.TryParse(hexValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var intValue)
                    ? ((char)intValue).ToString()
                    : match.Value;
            });
        }

        public static IEnumerable<string> SplitTomlString(this string text, char separator)
        {
            var currentString = new StringBuilder();
            var inQuotes = false;
            var inArray = false;
            var inTable = false;
            var escaped = false;

            foreach (var eachChar in text)
            {
                if (eachChar == separator && !inQuotes && !inArray && !inTable)
                {
                    yield return currentString.ToString();
                    currentString.Clear();
                    continue;
                }

                if (eachChar == '"' && !escaped)
                    inQuotes = !inQuotes;

                if (eachChar == '[' && !escaped)
                    inArray = true;

                if (eachChar == ']' && !escaped)
                    inArray = false;

                if (eachChar == '{' && !escaped)
                    inTable = true;

                if (eachChar == '}' && !escaped)
                    inTable = false;

                if (eachChar == '\\' && !escaped)
                {
                    escaped = true;
                    continue;
                }

                currentString.Append(eachChar);
                escaped = false;
            }

            yield return currentString.ToString();
        }

        public static string StripTomlComment(this string line)
        {
            if (line.StartsWith("#"))
                return string.Empty;

            var inDoubleQuotes = false;
            var inSingleQuotes = false;

            for (var charIndex = 0; charIndex < line.Length; charIndex++)
            {
                var eachChar = line[charIndex];

                if (eachChar == '"')
                    inDoubleQuotes = !inDoubleQuotes;

                if (eachChar == '\'')
                    inSingleQuotes = !inSingleQuotes;

                if (!inDoubleQuotes && !inSingleQuotes && eachChar == '#')
                    return line.Substring(0, charIndex);
            }

            return line;
        }
    }
}
