using System;
using System.Globalization;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {

        [Test]
        public void SerializeToString_NullObject_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.Serialize(null); });
        }

        [Test]
        public void SerializeToString_NonSerializable_ThrowsException()
        {
            var nonSerializable = new { };
            Assert.Throws<InvalidOperationException>(() => { TomlSerializer.Serialize(nonSerializable); });
        }

        [Test]
        public void SerializeToStream_NullStream_ThrowsException()
        {
            Stream stream = null;
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.Serialize(stream, new { }); });
        }

        [Test]
        public void SerializeToStream_NullObject_ThrowsException()
        {
            var stream = new MemoryStream();

            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.Serialize(stream, null); });
        }

        [Test]
        public void SerializeToStream_NonSerializable_ThrowsException()
        {
            var stream = new MemoryStream();
            var nonSerializable = new { };

            Assert.Throws<InvalidOperationException>(() => { TomlSerializer.Serialize(stream, nonSerializable); });
        }

        [Test]
        public void SerializeToWriter_NullWriter_ThrowsException()
        {
            TextWriter writer = null;

            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.Serialize(writer, null); });
        }

        [Test]
        public void SerializeToWriter_NullObject_ThrowsException()
        {
            TextWriter writer = null;

            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.Serialize(writer, null); });
        }

        [Test]
        public void SerializeToWriter_NonSerializable_ThrowsException()
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb, CultureInfo.InvariantCulture);
            var nonSerializable = new { };

            Assert.Throws<InvalidOperationException>(() => { TomlSerializer.Serialize(writer, nonSerializable); });
        }
    }
}
