using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullStringArray_ShouldBeNull()
        {
            var array = SerializableArray<string>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyStringArray_ShouldBeEmpty()
        {
            var array = SerializableArray<string>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_StringArray_ShouldQuoteStrings()
        {
            var array = SerializableArray<string>.WithValues("Hello", "World");
            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = array.Select(x => $"\"{x}\"");
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_StringArray_ShouldEscapeStrings()
        {
            var array = SerializableArray<string>.WithValues(
                "This string has a \\ backslash character",
                "This string has a \t tab character",
                "This string has a \r carriage return character",
                "This string has a \n line feed character",
                "This string has a \f form feed character",
                "This is a \"quoted\" string");
            
            var toml = TomlSerializer.Serialize(array);

            var expectedValues = new[]
            {
                "This string has a \\\\ backslash character",
                "This string has a \\t tab character",
                "This string has a \\r carriage return character",
                "This string has a \\n line feed character",
                "This string has a \\f form feed character",
                "This is a \\\"quoted\\\" string"
            };
            
            var expectedStringValues = expectedValues.Select(x => $"\"{x}\"");
            var expectedToml = $"array = [ {string.Join(", ", expectedStringValues)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_StringArray_ShouldAllowCommentChar()
        {
            var array = SerializableArray<string>.WithValues("#1 Thing", "The #2 Thing");
            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = array.Select(x => $"\"{x}\"");
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
