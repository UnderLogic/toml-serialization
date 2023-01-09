using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullCharList_ShouldSetNull()
        {
            var toml = "list = null\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<char>>(toml);
            Assert.That(deserializedList.IsNull, Is.EqualTo(true), "List should be null");
        }
        
        [Test]
        public void Deserialize_EmptyCharList_ShouldSetEmpty()
        {
            var toml = "list = []\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<char>>(toml);
            Assert.That(deserializedList.Count, Is.EqualTo(0), "List should be null");
        }
        
        [Test]
        public void Deserialize_CharList_ShouldSetValues()
        {
            var expectedValues = new[] { 'A', 'Z', '0', '9', '!', '@', '$', '_', '-', '+' };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<char>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }
        
        [Test]
        public void Deserialize_CharListMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { 'A', 'Z', '0', '9', '!', '@', '$', '_', '-', '+' };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<char>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_CharList_ShouldUnescapeValues()
        {
            var expectedValues = new[] { '\\', '\t', '\r', '\n', '\f', '"' };

            var escapedValues = new[] { "\\\\", "\\t", "\\r", "\\n", "\\f", "\\\"" };
            var escapedValueStrings = escapedValues.Select(x => $"\"{x}\"");
            var toml = $"list = [ {string.Join(", ", escapedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<char>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_CharList_ShouldAllowCommentChar()
        {
            var expectedValues = new[] { '#' };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<char>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }
    }
}
