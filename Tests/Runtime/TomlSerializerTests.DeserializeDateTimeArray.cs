using System;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullDateTimeArray_ShouldSetNull()
        {
            var toml = "array = null\n";
            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<DateTime>>(toml);
            Assert.That(deserializedArray.IsNull, Is.EqualTo(true), "Array should be null");
        }
        
        [Test]
        public void Deserialize_EmptyDateTimeArray_ShouldSetEmpty()
        {
            var toml = "array = []\n";
            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<DateTime>>(toml);
            Assert.That(deserializedArray.Count, Is.EqualTo(0), "Array should be null");
        }
        
        [Test]
        public void Deserialize_DateTimeArray_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                new DateTime(1979, 5, 27),
                new DateTime(2000, 1, 1)
            };
            var expectedValueStrings = expectedValues.Select(x => x.ToString("yyyy-MM-dd HH:mm:ss.fffZ"));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<DateTime>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_DateTimeArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                new DateTime(1979, 5, 27),
                new DateTime(2000, 1, 1)
            };
            var expectedValueStrings = expectedValues.Select(x => x.ToString("yyyy-MM-dd HH:mm:ss.fffZ"));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<DateTime>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
