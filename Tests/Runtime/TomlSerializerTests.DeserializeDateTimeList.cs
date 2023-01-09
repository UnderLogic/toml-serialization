using System;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullDateTimeList_ShouldSetNull()
        {
            var toml = "list = null\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<DateTime>>(toml);
            Assert.That(deserializedList.IsNull, Is.EqualTo(true), "List should be null");
        }
        
        [Test]
        public void Deserialize_EmptyDateTimeList_ShouldSetEmpty()
        {
            var toml = "list = []\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<DateTime>>(toml);
            Assert.That(deserializedList.Count, Is.EqualTo(0), "List should be null");
        }
        
        [Test]
        public void Deserialize_DateTimeList_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                new DateTime(1979, 5, 27),
                new DateTime(2000, 1, 1)
            };
            var expectedValueStrings = expectedValues.Select(x => x.ToString("yyyy-MM-dd HH:mm:ss.fffZ"));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<DateTime>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_DateTimeListMultiline_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                new DateTime(1979, 5, 27),
                new DateTime(2000, 1, 1)
            };
            var expectedValueStrings = expectedValues.Select(x => x.ToString("yyyy-MM-dd HH:mm:ss.fffZ"));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<DateTime>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }
    }
}
