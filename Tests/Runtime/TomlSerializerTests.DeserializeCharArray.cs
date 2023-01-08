using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_CharArray_ShouldSetValues()
        {
            var expectedValues = new[] { 'A', 'Z', '0', '9', '!', '@', '$', '_', '-', '+' };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<char>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
        
        [Test]
        public void Deserialize_CharArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { 'A', 'Z', '0', '9', '!', '@', '$', '_', '-', '+' };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<char>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_CharArray_ShouldUnescapeValues()
        {
            var expectedValues = new[] { '\\', '\t', '\r', '\n', '\f', '"' };
            var escapedValues = new[] { "\\\\", "\\t", "\\r", "\\n", "\\f", "\\\"" };
            var toml = new TomlStringBuilder().AppendArray("array", escapedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<char>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_CharArray_ShouldAllowCommentChar()
        {
            var expectedValues = new[] { '#' };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<char>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
