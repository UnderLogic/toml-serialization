using System;
using System.IO;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_EnumArray_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<DayOfWeek>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_EnumArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday
            };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<DayOfWeek>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_EnumFlagsArray_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                FileAttributes.Normal,
                FileAttributes.System,
                FileAttributes.Compressed | FileAttributes.Encrypted
            };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<FileAttributes>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_EnumFlagsArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                FileAttributes.Normal,
                FileAttributes.System,
                FileAttributes.Compressed | FileAttributes.Encrypted
            };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<FileAttributes>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
