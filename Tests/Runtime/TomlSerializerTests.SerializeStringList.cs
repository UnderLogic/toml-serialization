using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullStringList_ShouldBeNull()
        {
            var list = SerializableList<string>.Null();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyStringList_ShouldBeEmpty()
        {
            var list = SerializableList<string>.Empty();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_StringList_ShouldQuoteStrings()
        {
            var list = SerializableList<string>.WithValues("Hello", "World");
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => $"\"{x}\"");
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_StringList_ShouldEscapeStrings()
        {
            var list = SerializableList<string>.WithValues(
                "This string has a \\ backslash character",
                "This string has a \t tab character",
                "This string has a \r carriage return character",
                "This string has a \n line feed character",
                "This string has a \f form feed character",
                "This is a \"quoted\" string");
            
            var toml = TomlSerializer.Serialize(list);

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
            var expectedToml = $"list = [ {string.Join(", ", expectedStringValues)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_StringList_ShouldAllowCommentChar()
        {
            var list = SerializableList<string>.WithValues("#1 Thing", "The #2 Thing");
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => $"\"{x}\"");
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
