using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullStringValue_ShouldBeNull()
        {
            var value = new SerializableValue<string>(null);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = "value = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [TestCase("Hello, World!")]
        [TestCase("Bird's the word!")]
        [TestCase("The quick brown fox jumps over the lazy dog.")]
        public void Serialize_StringValue_ShouldQuoteString(string stringValue)
        {
            var value = new SerializableValue<string>(stringValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = $"value = \"{stringValue}\"\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [TestCase("This string has a \\ backslash character", "This string has a \\\\ backslash character")]
        [TestCase("This string has a \t tab character", "This string has a \\t tab character")]
        [TestCase("This string has a \r carriage return character", "This string has a \\r carriage return character")]
        [TestCase("This string has a \n line feed character", "This string has a \\n line feed character")]
        [TestCase("This string has a \f form feed character", "This string has a \\f form feed character")]
        [TestCase("This is a \"quoted\" string", "This is a \\\"quoted\\\" string")]
        public void Serialize_StringValue_ShouldEscapeString(string stringValue, string expectedValue)
        {
            var value = new SerializableValue<string>(stringValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = $"value = \"{expectedValue}\"\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [TestCase("#1 Thing")]
        [TestCase("The #2 Thing")]
        public void Serialize_StringValue_ShouldAllowCommentChar(string stringValue)
        {
            var value = new SerializableValue<string>(stringValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = $"value = \"{stringValue}\"\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
