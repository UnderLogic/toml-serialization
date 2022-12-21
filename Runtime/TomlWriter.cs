using System;
using System.Globalization;
using System.IO;
using System.Text;
using UnderLogic.Serialization.Toml.Types;

namespace UnderLogic.Serialization.Toml
{
    internal class TomlWriter : IDisposable
    {
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

        public void WriteStringValue(string value)
        {
            CheckIfDisposed();

            if (value == null)
            {
                WriteNullValue();
                return;
            }

            _writer.Write($"\"{value}\"");
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
        
        public void WriteIntegerValue(long value)
        {
            CheckIfDisposed();
            _writer.Write(value.ToString());
        }
        
        public void WriteFloatValue(double value)
        {
            CheckIfDisposed();
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
                WriteStringValue(stringValue.Value);
            else if (value is TomlBoolean boolValue)
                WriteBooleanValue(boolValue.Value);
            else if (value is TomlInteger intValue)
                WriteIntegerValue(intValue.Value);
            else if (value is TomlFloat floatValue)
                WriteFloatValue(floatValue.Value);
            else if (value is TomlDateTime dateTimeValue)
                WriteDateTime(dateTimeValue.Value);
            else if (value is TomlArray arrayValue)
                WriteArray(arrayValue);
            else if (value is TomlTable tableValue)
                WriteTableInline(tableValue);
            else
                throw new InvalidOperationException($"Type {value.GetType().Name} is not supported");
        }

        public void WriteArray(TomlArray array)
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

            var counter = 0;
            foreach (var value in array)
            {
                WriteValue(value);
                counter++;
                
                if (counter < array.Count)
                    _writer.Write(", ");
            }

            _writer.Write(" ]");
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

            var isFirstItem = true;
            
            if (table != null)
            {
                foreach (var keyValuePair in table)
                {
                    var key = keyValuePair.Key;
                    var value = keyValuePair.Value;

                    var childTableKey = string.IsNullOrWhiteSpace(tableKey) ? key : $"{tableKey}.{key}";
                    
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
                            if (!isFirstItem)
                                _writer.WriteLine();
                            
                            WriteTableExpanded(childTableKey, childTable);
                        }
                    }
                    else if (value is TomlTableArray childTableArray)
                    {
                        if (childTableArray.Count > 0)
                        {
                            if (!isFirstItem)
                                _writer.WriteLine();
                            
                            WriteTableArray(childTableKey, childTableArray);
                        }
                    }
                    else
                    {
                        WriteKeyValue(keyValuePair);
                        _writer.WriteLine();
                    }

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
