using System;
using System.Globalization;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    public partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_String_NullObject_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                TomlSerializer.Serialize(null);
            });
        }
        
        [Test]
        public void Serialize_String_NonSerializable_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var nonSerializable = new { };
                TomlSerializer.Serialize(nonSerializable);
            });
        }
        
        [Test]
        public void Serialize_Stream_NullStream_ThrowsException()
        {
            Stream stream = null;
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                TomlSerializer.Serialize(stream, new{});
            });
        }
        
        [Test]
        public void Serialize_Stream_NullObject_ThrowsException()
        {
            var stream = new MemoryStream();
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                TomlSerializer.Serialize(stream, null);
            });
        }
        
        [Test]
        public void Serialize_Stream_NonSerializable_ThrowsException()
        {
            var stream = new MemoryStream();
            
            Assert.Throws<ArgumentException>(() =>
            {
                var nonSerializable = new { };
                TomlSerializer.Serialize(stream, nonSerializable);
            });
        }
        
        [Test]
        public void Serialize_Writer_NullWriter_ThrowsException()
        {
            TextWriter writer = null;
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                TomlSerializer.Serialize(writer, null);
            });
        }
        
        [Test]
        public void Serialize_Writer_NullObject_ThrowsException()
        {
            TextWriter writer = null;
            
            Assert.Throws<ArgumentNullException>(() =>
            {
                TomlSerializer.Serialize(writer, null);
            });
        }
        
        [Test]
        public void Serialize_Writer_NonSerializable_ThrowsException()
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb, CultureInfo.InvariantCulture);
            
            Assert.Throws<ArgumentException>(() =>
            {
                var nonSerializable = new { };
                TomlSerializer.Serialize(writer, nonSerializable);
            });
        }
    }
}
