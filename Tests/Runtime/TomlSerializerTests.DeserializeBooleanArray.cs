using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_BooleanArray_ShouldSetValues()
        {
            var expectedValues = new[] { true, false, true };
            var expectedValueStrings = expectedValues.Select(x => x.ToString().ToLowerInvariant());
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<bool>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_BooleanArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { true, false, true };
            var expectedValueStrings = expectedValues.Select(x => x.ToString().ToLowerInvariant());
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<bool>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
