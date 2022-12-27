using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnderLogic.Serialization.Toml.Types;

namespace UnderLogic.Serialization.Toml
{
    internal class TomlReader : IDisposable
    {
        private static readonly Regex KeyValueRegex = new(@"^\s*([\w.-]+)\s*=\s*(.*)", RegexOptions.Compiled);
        private static readonly Regex ArrayRegex = new(@"^\s*\[\s*(.*)\s*\]", RegexOptions.Compiled);
        private static readonly Regex TableInlineRegex = new(@"^\s*\{\s*(.*)\s*\}", RegexOptions.Compiled);
        private static readonly Regex TableRegex = new(@"^\s*\[(.+?)\]", RegexOptions.Compiled);
        private static readonly Regex TableArrayRegex = new(@"^\s*\[\[(.+?)\]\]", RegexOptions.Compiled);

        private readonly TextReader _reader;
        private bool _isDisposed;

        public TomlReader(Stream stream, bool leaveOpen = true)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            _reader = new StreamReader(stream, Encoding.UTF8, false, 1024, leaveOpen);
        }

        public TomlReader(TextReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        ~TomlReader()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        public TomlTable ReadDocument()
        {
            CheckIfDisposed();
            
            TomlTableArray currentTableArray = null;

            var currentTableKey = string.Empty;
            var rootTable = new TomlTable();

            string line;
            while ((line = _reader.ReadLine()) != null)
            {
                // Skip comments
                if (line.StartsWith("#"))
                    continue;

                // Start of a new table array
                var tableArrayMatch = TableArrayRegex.Match(line);
                if (tableArrayMatch.Success)
                {
                    var arrayKey = tableArrayMatch.Groups[1].Value.Trim();
                    if (!rootTable.TryGetValue(arrayKey, out var existingValue))
                    {
                        var newTableArray = new TomlTableArray();
                        rootTable.Add(arrayKey, newTableArray);

                        currentTableArray = newTableArray;
                    }
                    else
                    {
                        if (existingValue is TomlTableArray existingTableArray)
                            currentTableArray = existingTableArray;
                        else
                            throw new InvalidOperationException($"Key {arrayKey} is not a table array");
                    }

                    var newTable = new TomlTable();
                    currentTableArray.Add(newTable);
                    continue;
                }

                // Start of a new table
                var tableMatch = TableRegex.Match(line);
                if (tableMatch.Success)
                {
                    currentTableArray = null;
                    currentTableKey = tableMatch.Groups[1].Value.Trim();

                    var childTable = new TomlTable();
                    rootTable.Add(currentTableKey, childTable);
                    continue;
                }

                // Parse key-value pairs into current table/table array
                if (TryParseKeyValuePair(line, out var keyValuePair))
                {
                    // If we're in a table array, add the key-value pair to the last table
                    if (currentTableArray != null)
                    {
                        var arrayTable = currentTableArray[currentTableArray.Count - 1];
                        arrayTable.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                    // If we're in a table, add the key-value pair to that table
                    else if(!string.IsNullOrWhiteSpace(currentTableKey))
                    {
                        if (!rootTable.TryGetValue(currentTableKey, out var existingValue))
                            throw new InvalidOperationException($"Table {currentTableKey} does not exist");
                        
                        if (!(existingValue is TomlTable existingTable))
                            throw new InvalidOperationException($"Key {currentTableKey} is not a table");

                        existingTable.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                    // Otherwise, add the key-value pair to the root table
                    else
                    {
                        rootTable.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }

            return rootTable;
        }

        private static bool TryParseScalarValue(string text, out TomlValue tomlValue)
        {
            tomlValue = null;

            if (text == null || text == "null")
            {
                tomlValue = TomlNull.Value;
                return true;
            }

            var trimmedText = text.Trim();

            if (TryParseBooleanValue(trimmedText, out var boolValue))
                tomlValue = boolValue;
            else if (TryParseStringValue(trimmedText, out var stringValue))
                tomlValue = stringValue;
            else if (TryParseFloatValue(trimmedText, out var floatValue))
                tomlValue = floatValue;
            else if (TryParseIntegerValue(trimmedText, out var integerValue))
                tomlValue = integerValue;
            else if (TryParseDateTimeValue(trimmedText, out var dateTimeValue))
                tomlValue = dateTimeValue;

            return tomlValue != null;
        }

        private static bool TryParseKeyValuePair(string text, out TomlKeyValuePair keyValuePair)
        {
            keyValuePair = null;

            var keyValueMatch = KeyValueRegex.Match(text);
            if (!keyValueMatch.Success)
                return false;

            var key = keyValueMatch.Groups[1].Value.Trim();
            var valueString = keyValueMatch.Groups[2].Value.Trim();

            TomlValue value;

            if (valueString == "null")
                value = TomlNull.Value;
            else if (TryParseTableInline(valueString, out var tableValue))
                value = tableValue;
            else if (TryParseArray(valueString, out var arrayValue))
                value = arrayValue;
            else if (TryParseScalarValue(valueString, out var scalarValue))
                value = scalarValue;
            else
                return false;

            keyValuePair = new TomlKeyValuePair(key, value);
            return true;
        }

        private static bool TryParseArray(string text, out TomlArray tomlArray)
        {
            tomlArray = null;

            var arrayMatch = ArrayRegex.Match(text.Trim());
            if (!arrayMatch.Success)
                return false;

            var contentString = arrayMatch.Groups[1].Value.Trim();
            if (string.IsNullOrWhiteSpace(contentString))
            {
                tomlArray = TomlArray.Empty;
                return true;
            }

            var arrayValues = SplitString(contentString, ',').Select(value => value?.Trim());
            var parsedValues = new List<TomlValue>();

            foreach (var eachValue in arrayValues)
            {
                if (eachValue == "null")
                    parsedValues.Add(TomlNull.Value);
                else if (TryParseTableInline(eachValue, out var tableValue))
                    parsedValues.Add(tableValue);
                else if (TryParseArray(eachValue, out var childArrayValue))
                    parsedValues.Add(childArrayValue);
                else if (TryParseScalarValue(eachValue, out var scalarValue))
                    parsedValues.Add(scalarValue);
                else
                    return false;
            }

            tomlArray = new TomlArray(parsedValues);
            return true;
        }

        private static bool TryParseTableInline(string text, out TomlTable tomlTable)
        {
            tomlTable = null;

            var tableInlineMatch = TableInlineRegex.Match(text.Trim());
            if (!tableInlineMatch.Success)
                return false;

            var contentString = tableInlineMatch.Groups[1].Value.Trim();
            if (string.IsNullOrWhiteSpace(contentString))
            {
                tomlTable = TomlTable.EmptyInline;
                return true;
            }

            var tableValues = SplitString(contentString, ',');
            var parsedKeyValues = new List<TomlKeyValuePair>();

            foreach (var keyPairString in tableValues)
            {
                if (TryParseKeyValuePair(keyPairString, out var keyValuePair))
                    parsedKeyValues.Add(keyValuePair);
                else
                    return false;
            }

            tomlTable = new TomlTable(parsedKeyValues) { IsInline = true };
            return true;
        }

        private static bool TryParseBooleanValue(string text, out TomlBoolean tomlValue)
        {
            tomlValue = null;

            if (text != "true" && text != "false")
                return false;

            tomlValue = new TomlBoolean(text == "true");
            return true;
        }

        private static bool TryParseStringValue(string text, out TomlString tomlValue)
        {
            tomlValue = null;

            var wrappedInDoubleQuotes = text.StartsWith('"') && text.EndsWith('"');
            var wrappedInSingleQuotes = text.StartsWith("'") && text.EndsWith("'");

            if (!wrappedInDoubleQuotes && !wrappedInSingleQuotes)
                return false;

            var unescapedText = text.Substring(1, text.Length - 2).Replace("\\\\", "\\").Replace("\\\"", "\"");
            tomlValue = new TomlString(unescapedText);
            return true;
        }

        private static bool TryParseFloatValue(string text, out TomlFloat tomlValue)
        {
            tomlValue = null;

            var sanitizedText = text.Replace("_", "").ToLowerInvariant();
            if (!sanitizedText.Contains('.') && !sanitizedText.Contains('e'))
                return false;

            if (!double.TryParse(sanitizedText, out var doubleValue))
                return false;

            tomlValue = new TomlFloat(doubleValue);
            return true;
        }

        private static bool TryParseIntegerValue(string text, out TomlInteger tomlValue)
        {
            tomlValue = null;

            var sanitizedText = text.Replace("_", "");

            if (!long.TryParse(sanitizedText, out var int64Value))
                return false;

            tomlValue = new TomlInteger(int64Value);
            return true;
        }

        private static bool TryParseDateTimeValue(string text, out TomlDateTime tomlValue)
        {
            tomlValue = null;

            if (!DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind,
                    out var dateTimeValue))
                return false;

            tomlValue = new TomlDateTime(dateTimeValue);
            return true;
        }

        private static IEnumerable<string> SplitString(string text, char separator)
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

        public void Close()
        {
            CheckIfDisposed();
            _reader.Close();
        }

        public void Dispose() => Dispose(true);

        private void CheckIfDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(TomlReader), "Reader has been disposed");
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                _reader.Dispose();
            }

            _isDisposed = true;
        }
    }
}
