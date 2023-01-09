using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullStringList_ShouldSetNull()
        {
            var toml = "list = null\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<string>>(toml);
            Assert.That(deserializedList.IsNull, Is.EqualTo(true), "List should be null");
        }
        
        [Test]
        public void Deserialize_EmptyStringList_ShouldSetEmpty()
        {
            var toml = "list = []\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<string>>(toml);
            Assert.That(deserializedList.Count, Is.EqualTo(0), "List should be null");
        }
        
        [Test]
        public void Deserialize_StringListOfNulls_ShouldSetNulls()
        {
            var expectedValues = new string[] { null, null, null };
            var expectedValueStrings = expectedValues.Select(x => "null");
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<string>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }
        
        [Test]
        public void Deserialize_StringList_ShouldSetValues()
        {
            var expectedValues = new[] { "Hello, World!", "The quick brown fox jumps over the lazy dog." };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<string>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_StringListMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog." };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<string>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_StringList_ShouldUnescapeValues()
        {
            var expectedValues = new[] { "\\", "\t", "\r", "\n", "\f", "\"" };
            
            var escapedValues = new[] { "\\\\", "\\t", "\\r", "\\n", "\\f", "\\\"" };
            var escapedValueStrings = escapedValues.Select(x => $"\"{x}\"");
            var toml = $"list = [ {string.Join(", ", escapedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<string>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_StringList_ShouldAllowCommentChar()
        {
            var expectedValues = new[] { "#1 Thing", "The #2 Thing" };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<string>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }
    }
}
