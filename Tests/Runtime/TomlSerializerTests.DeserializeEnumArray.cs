using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

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
            var expectedValueStrings = expectedValues.Select(x => $"\"{x:F}\"");
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

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
            var expectedValueStrings = expectedValues.Select(x => $"\"{x:F}\"");
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

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
            var expectedValueStrings = expectedValues.Select(x => $"\"{x:F}\"");
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

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
            var expectedValueStrings = expectedValues.Select(x => $"\"{x:F}\"");
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<FileAttributes>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
