using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullStringArray_ShouldSetNull()
        {
            var toml = "array = null\n";
            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray.IsNull, Is.EqualTo(true), "Array should be null");
        }
        
        [Test]
        public void Deserialize_EmptyStringArray_ShouldSetEmpty()
        {
            var toml = "array = []\n";
            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray.Count, Is.EqualTo(0), "Array should be null");
        }
        
        [Test]
        public void Deserialize_StringArrayOfNulls_ShouldSetNulls()
        {
            var expectedValues = new string[] { null, null, null };
            var expectedValueStrings = expectedValues.Select(x => "null");
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
        
        [Test]
        public void Deserialize_StringArray_ShouldSetValues()
        {
            var expectedValues = new[] { "Hello, World!", "The quick brown fox jumps over the lazy dog." };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_StringArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog." };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_StringArray_ShouldUnescapeValues()
        {
            var expectedValues = new[] { "\\", "\t", "\r", "\n", "\f", "\"" };
            
            var escapedValues = new[] { "\\\\", "\\t", "\\r", "\\n", "\\f", "\\\"" };
            var escapedValueStrings = escapedValues.Select(x => $"\"{x}\"");
            var toml = $"array = [ {string.Join(", ", escapedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_StringArray_ShouldAllowCommentChar()
        {
            var expectedValues = new[] { "#1 Thing", "The #2 Thing" };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<string>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
