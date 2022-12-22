using System;
using System.IO;
using System.Text;
using UnderLogic.Serialization.Toml.Types;

namespace UnderLogic.Serialization.Toml
{
    internal class TomlReader : IDisposable
    {
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss.fffZ";

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
            return new TomlTable();
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
