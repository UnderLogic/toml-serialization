using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("A", 'A')]
        [TestCase("Z", 'Z')]
        [TestCase("0", '0')]
        [TestCase("9", '9')]
        [TestCase("!", '!')]
        [TestCase("@", '@')]
        [TestCase("$", '$')]
        [TestCase("%", '%')]
        [TestCase("&", '&')]
        [TestCase("_", '_')]
        [TestCase("-", '-')]
        [TestCase("+", '+')]
        public void Deserialize_CharValue_ShouldSetValue(string stringValue, char expectedValue)
        {
            var toml = $"value = \"{stringValue}\"\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<char>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("\\\\", '\\')]
        [TestCase("\\t", '\t')]
        [TestCase("\\r", '\r')]
        [TestCase("\\n", '\n')]
        [TestCase("\\f", '\f')]
        [TestCase("\\\"", '"')]
        public void Deserialize_CharValue_ShouldUnescapeChars(string stringValue, char expectedValue)
        {
            var toml = $"value = \"{stringValue}\"\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<char>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("#", '#')]
        public void Deserialize_CharValue_ShouldAllowCommentChar(string stringValue, char expectedValue)
        {
            var toml = $"value = \"{stringValue}\"\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<char>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
    }
}
