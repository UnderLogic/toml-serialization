using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase('A')]
        [TestCase('Z')]
        [TestCase('0')]
        [TestCase('9')]
        [TestCase('!')]
        [TestCase('@')]
        [TestCase('$')]
        [TestCase('%')]
        [TestCase('&')]
        [TestCase('_')]
        [TestCase('+')]
        [TestCase('-')]
        public void Serialize_CharValue_ShouldQuoteChars(char charValue)
        {
            var value = new SerializableValue<char>(charValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", charValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [TestCase('\t', "\\t")]
        [TestCase('\r', "\\r")]
        [TestCase('\n', "\\n")]
        [TestCase('"', "\\\"")]
        public void Serialize_CharValue_ShouldEscapeChars(char charValue, string expectedValue)
        {
            var value = new SerializableValue<char>(charValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", expectedValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [TestCase('#')]
        public void Serialize_CharValue_ShouldAllowCommentChar(char charValue)
        {
            var value = new SerializableValue<char>(charValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", charValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
