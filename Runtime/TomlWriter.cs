using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnderLogic.Serialization.Toml.Types;

namespace UnderLogic.Serialization.Toml
{
    internal class TomlWriter : IDisposable
    {
        private const string TomlIndent = "    ";
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss.fffZ";

        private readonly TextWriter _writer;
        private bool _isDisposed;

        public TomlWriter(Stream stream, bool leaveOpen = true)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            _writer = new StreamWriter(stream, Encoding.UTF8, 1024, leaveOpen);
        }

        public TomlWriter(TextWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        }

        ~TomlWriter()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        public void WriteNullValue()
        {
            CheckIfDisposed();
            _writer.Write("null");
        }

        public void WriteCharValue(char value)
        {
            CheckIfDisposed();
            _writer.Write($"\"{value}\"");
        }

        public void WriteStringValue(string value, bool isMultiline = false)
        {
            CheckIfDisposed();

            if (value == null)
            {
                WriteNullValue();
                return;
            }

            // Escape all backslashes and double quotes
            value = value.EscapeChar('\\').EscapeChar('"');

            if (!isMultiline)
            {
                // Escape all whitespace characters for single-line strings
                value = value.EscapeWhitespace();
                _writer.Write($"\"{value}\"");
            }
            else
            {
                _writer.Write($"\"\"\"\n{value}\"\"\"");
            }
        }

        public void WriteLiteralStringValue(string value, bool isMultiline = false)
        {
            CheckIfDisposed();

            if (value == null)
            {
                WriteNullValue();
                return;
            }

            if (!isMultiline)
            {
                // Literal strings with a single quote must be escaped with triple quotes
                var quotes = value.Contains("'") ? "'''" : "'";
                _writer.Write($"{quotes}{value}{quotes}");
            }
            else
            {
                _writer.Write($"'''\n{value}'''");
            }
        }

        public void WriteEnumValue<T>(T value) where T : Enum
        {
            CheckIfDisposed();
            _writer.Write($"\"{value:F}\"");
        }

        public void WriteBooleanValue(bool value)
        {
            CheckIfDisposed();
            _writer.Write($"{value.ToString().ToLowerInvariant()}");
        }

        public void WriteIntegerValue(long value, NumberFormat numberFormat = NumberFormat.Decimal)
        {
            CheckIfDisposed();

            if (numberFormat == NumberFormat.HexLowerCase)
                _writer.Write(value.ToHexLowerCaseString());
            else if (numberFormat == NumberFormat.HexUpperCase)
                _writer.Write(value.ToHexUpperCaseString());
            else if (numberFormat == NumberFormat.Octal)
                _writer.Write(value.ToOctalString());
            else if (numberFormat == NumberFormat.Binary)
                _writer.Write(value.ToBinaryString());
            else
                _writer.Write(value.ToString());
        }

        public void WriteFloatValue(double value)
        {
            CheckIfDisposed();

            if (double.IsPositiveInfinity(value))
                _writer.Write("+inf");
            else if (double.IsNegativeInfinity(value))
                _writer.Write("-inf");
            else if (double.IsNaN(value))
                _writer.Write("nan");
            else
                _writer.Write(value.ToString(CultureInfo.InvariantCulture));
        }

        public void WriteDateTime(DateTime value, string format = DateFormat)
        {
            CheckIfDisposed();
            _writer.Write(value.ToString(format));
        }

        public void WriteKeyValue(TomlKeyValuePair keyValuePair) => WriteKeyValue(keyValuePair.Key, keyValuePair.Value);

        public void WriteKeyValue(string key, TomlValue value)
        {
            ValidateKey(key);
            CheckIfDisposed();

            WriteKey(key);
            WriteValue(value);
        }

        public void WriteKey(string key)
        {
            ValidateKey(key);
            CheckIfDisposed();

            _writer.Write($"{key} = ");
        }

        public void WriteValue(TomlValue value)
        {
            CheckIfDisposed();

            if (value is TomlNull)
                WriteNullValue();
            else if (value is TomlString stringValue)
            {
                if (stringValue.IsLiteral)
                    WriteLiteralStringValue(stringValue.Value, stringValue.IsMultiline);
                else
                    WriteStringValue(stringValue.Value, stringValue.IsMultiline);
            }
            else if (value is TomlBoolean boolValue)
                WriteBooleanValue(boolValue.Value);
            else if (value is TomlInteger intValue)
                WriteIntegerValue(intValue.Value, intValue.NumberFormat);
            else if (value is TomlFloat floatValue)
                WriteFloatValue(floatValue.Value);
            else if (value is TomlDateTime dateTimeValue)
                WriteDateTime(dateTimeValue.Value);
            else if (value is TomlArray arrayValue)
            {
                if (arrayValue.IsMultiline)
                    WriteArrayMultiline(arrayValue);
                else
                    WriteArrayInline(arrayValue);
            }
            else if (value is TomlTable tableValue)
                WriteTableInline(tableValue);
            else
                throw new InvalidOperationException($"Type {value.GetType().Name} is not supported");
        }

        public void WriteArrayInline(TomlArray array)
        {
            CheckIfDisposed();

            if (array == null)
            {
                WriteNullValue();
                return;
            }

            if (array.Count < 1)
            {
                _writer.Write("[]");
                return;
            }

            _writer.Write("[ ");

            for (var i = 0; i < array.Count; i++)
            {
                WriteValue(array[i]);

                if (i < array.Count - 1)
                    _writer.Write(", ");
            }

            _writer.Write(" ]");
        }

        private void WriteArrayMultiline(TomlArray array)
        {
            CheckIfDisposed();

            if (array == null)
            {
                WriteNullValue();
                return;
            }

            if (array.Count < 1)
            {
                _writer.Write("[]");
                return;
            }

            _writer.WriteLine("[");

            foreach (var value in array)
            {
                _writer.Write(TomlIndent);
                WriteValue(value);
                _writer.WriteLine(",");
            }

            _writer.Write("]");
        }

        public void WriteTableInline(TomlTable table)
        {
            CheckIfDisposed();

            if (table == null)
            {
                WriteNullValue();
                return;
            }

            if (table.Count < 1)
            {
                _writer.Write("{}");
                return;
            }

            _writer.Write("{ ");

            var counter = 0;
            foreach (var keyValuePair in table)
            {
                WriteKeyValue(keyValuePair);
                counter++;

                if (counter < table.Count)
                    _writer.Write(", ");
            }

            _writer.Write(" }");
        }

        public void WriteTableExpanded(string tableKey, TomlTable table)
        {
            CheckIfDisposed();

            if (!string.IsNullOrWhiteSpace(tableKey))
                WriteTableKey(tableKey);

            var isFirstItem = string.IsNullOrWhiteSpace(tableKey);

            if (table != null)
            {
                var childTables = new Dictionary<string, TomlTable>();
                var childArrays = new Dictionary<string, TomlTableArray>();

                foreach (var keyValuePair in table)
                {
                    var key = keyValuePair.Key;
                    var value = keyValuePair.Value;

                    if (value is TomlTable childTable)
                    {
                        if (childTable.IsInline)
                        {
                            WriteKey(key);
                            WriteTableInline(childTable);
                            _writer.WriteLine();
                        }
                        else
                        {
                            childTables.Add(key, childTable);
                            continue;
                        }
                    }
                    else if (value is TomlTableArray childTableArray)
                    {
                        childArrays.Add(key, childTableArray);
                        continue;
                    }
                    else
                    {
                        WriteKeyValue(keyValuePair);
                        _writer.WriteLine();
                    }

                    isFirstItem = false;
                }

                foreach (var childTable in childTables)
                {
                    if (!isFirstItem)
                        _writer.WriteLine();

                    var childTablePath = string.IsNullOrWhiteSpace(tableKey)
                        ? childTable.Key
                        : $"{tableKey}.{childTable.Key}";

                    WriteTableExpanded(childTablePath, childTable.Value);
                    isFirstItem = false;
                }

                foreach (var childArray in childArrays)
                {
                    if (!isFirstItem)
                        _writer.WriteLine();

                    var childArrayPath = string.IsNullOrWhiteSpace(tableKey)
                        ? childArray.Key
                        : $"{tableKey}.{childArray.Key}";

                    WriteTableArray(childArrayPath, childArray.Value);
                    isFirstItem = false;
                }
            }
        }

        public void WriteTableKey(string key)
        {
            ValidateKey(key);
            CheckIfDisposed();

            _writer.WriteLine($"[{key}]");
        }

        public void WriteTableArray(string key, TomlTableArray tableArray)
        {
            ValidateKey(key);
            CheckIfDisposed();

            if (tableArray.Count < 1)
                return;

            var counter = 0;
            foreach (var table in tableArray)
            {
                WriteTableArrayKey(key);

                if (table != null)
                    WriteTableExpanded(string.Empty, table);

                counter++;

                if (counter < tableArray.Count)
                    _writer.WriteLine();
            }
        }

        public void WriteTableArrayKey(string key)
        {
            ValidateKey(key);
            CheckIfDisposed();

            _writer.WriteLine($"[[{key}]]");
        }

        public void WriteDocument(TomlTable rootTable)
        {
            CheckIfDisposed();

            if (rootTable == null)
                throw new ArgumentNullException(nameof(rootTable));

            WriteTableExpanded(string.Empty, rootTable);
        }

        public void WriteLine()
        {
            CheckIfDisposed();
            _writer.WriteLine();
        }

        public void Flush()
        {
            CheckIfDisposed();
            _writer.Flush();
        }

        public void Close()
        {
            CheckIfDisposed();
            _writer.Close();
        }

        public void Dispose() => Dispose(true);

        private static void ValidateKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or whitespace", nameof(key));
        }

        private void CheckIfDisposed()
        {
            if (_isDisposed)
                throw new ObjectDisposedException(nameof(TomlWriter), "Writer has been disposed");
        }

        private void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            if (isDisposing)
            {
                _writer.Dispose();
            }

            _isDisposed = true;
        }
    }
}
