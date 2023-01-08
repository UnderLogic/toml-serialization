using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullStringArray_ShouldSetNulls()
        {
            var expectedValues = new string[] { null, null, null };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
        
        [Test]
        public void Deserialize_StringArray_ShouldSetValues()
        {
            var expectedValues = new[] { "Hello, World!", "The quick brown fox jumps over the lazy dog." };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_StringArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog." };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_StringArray_ShouldUnescapeValues()
        {
            var expectedValues = new[] { "\\", "\t", "\r", "\n", "\f", "\"" };
            var escapedValues = new[] { "\\\\", "\\t", "\\r", "\\n", "\\f", "\\\"" };
            var toml = new TomlStringBuilder().AppendArray("array", escapedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_StringArray_ShouldAllowCommentChar()
        {
            var expectedValues = new[] { "#1 Thing", "The #2 Thing" };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
