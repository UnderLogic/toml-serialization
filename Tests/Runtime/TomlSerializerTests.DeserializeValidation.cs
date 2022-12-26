using System;
using System.IO;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void DeserializeFromString_NullString_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.Deserialize<object>(null as string); });
        }

        [Test]
        public void DeserializeFromStream_NullStream_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.Deserialize<object>(null as Stream); });
        }

        [Test]
        public void DeserializeFromReader_NullReader_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.Deserialize<object>(null as TextReader); });
        }

        [Test]
        public void DeserializeIntoFromString_NullString_ThrowsException()
        {
            var obj = new Player();
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.DeserializeInto(null as string, obj); });
        }

        [Test]
        public void DeserializeIntoFromString_NullObject_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.DeserializeInto(string.Empty, null); });
        }

        [Test]
        public void DeserializeIntoFromStream_NullStream_ThrowsException()
        {
            var obj = new Player();
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.DeserializeInto(null as Stream, obj); });
        }

        [Test]
        public void DeserializeIntoFromStream_NullObject_ThrowsException()
        {
            var stream = new MemoryStream();
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.DeserializeInto(stream, null); });
        }

        [Test]
        public void DeserializeIntoFromReader_NullReader_ThrowsException()
        {
            var obj = new Player();
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.DeserializeInto(null as TextReader, obj); });
        }

        [Test]
        public void DeserializeIntoFromReader_NullObject_ThrowsException()
        {
            var reader = new StringReader("");
            Assert.Throws<ArgumentNullException>(() => { TomlSerializer.DeserializeInto(reader, null); });
        }
    }
}
