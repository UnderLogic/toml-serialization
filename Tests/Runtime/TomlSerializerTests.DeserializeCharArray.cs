using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_CharArray_ShouldSetValues()
        {
            var expectedValues = new[] { 'A', 'Z', '0', '9', '!', '@', '$', '_', '-', '+' };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<char>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
        
        [Test]
        public void Deserialize_CharArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { 'A', 'Z', '0', '9', '!', '@', '$', '_', '-', '+' };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<char>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_CharArray_ShouldUnescapeValues()
        {
            var expectedValues = new[] { '\\', '\t', '\r', '\n', '\f', '"' };

            var escapedValues = new[] { "\\\\", "\\t", "\\r", "\\n", "\\f", "\\\"" };
            var escapedValueStrings = escapedValues.Select(x => $"\"{x}\"");
            var toml = $"array = [ {string.Join(", ", escapedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<char>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_CharArray_ShouldAllowCommentChar()
        {
            var expectedValues = new[] { '#' };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<char>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
