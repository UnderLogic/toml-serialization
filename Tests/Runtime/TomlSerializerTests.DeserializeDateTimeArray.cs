using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_DateTimeArray_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                new DateTime(1979, 5, 27),
                new DateTime(2000, 1, 1)
            };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

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
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, multiline: true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<DateTime>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
