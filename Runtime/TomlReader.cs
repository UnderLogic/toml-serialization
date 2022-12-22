using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnderLogic.Serialization.Toml.Types;

namespace UnderLogic.Serialization.Toml
{
    internal class TomlReader : IDisposable
    {
        private static readonly Regex KeyValueRegex = new (@"^\s*([\w.-]+)\s*=\s*(.*)", RegexOptions.Compiled);

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

            var rootTable = new TomlTable();

            var currentTable = rootTable;

            var tableStack = new Stack<TomlTable>();
            
            string line = null;
            while ((line = _reader.ReadLine()) != null)
            {
                // Skip comments
                if (line.StartsWith("#"))
                    continue;

                // Parse key-value pairs
                var keyValueMatch = KeyValueRegex.Match(line);
                if (keyValueMatch.Success)
                {
                    var key = keyValueMatch.Groups[1].Value.Trim();
                    var valueString = keyValueMatch.Groups[2].Value.Trim();

                    if (TryParseScalarValue(valueString, out var scalarValue))
                    {
                        currentTable.Add(key, scalarValue);
                        continue;
                    }
                }
            }

            return rootTable;
        }

        private static bool TryParseScalarValue(string text, out TomlValue tomlValue)
        {
            tomlValue = null;

            if (text == "null")
            {
                tomlValue = TomlNull.Value;
                return true;
            }

            if (TryParseBooleanValue(text, out var boolValue))
                tomlValue = boolValue;
            else if (TryParseStringValue(text, out var stringValue))
                tomlValue = stringValue;
            else if (TryParseFloatValue(text, out var floatValue))
                tomlValue = floatValue;
            else if (TryParseIntegerValue(text, out var integerValue))
                tomlValue = integerValue;
            else if (TryParseDateTimeValue(text, out var dateTimeValue))
                tomlValue = dateTimeValue;

            return tomlValue != null;
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
            
            tomlValue = new TomlString(text.Substring(1, text.Length - 2));
            return true;
        }
        
        private static bool TryParseFloatValue(string text, out TomlFloat tomlValue)
        {
            tomlValue = null;

            var sanitizedText = text.Replace("_", "");
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

            if (!DateTime.TryParse(text, out var dateTimeValue))
                return false;
            
            tomlValue = new TomlDateTime(dateTimeValue);
            return true;
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
