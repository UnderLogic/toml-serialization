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
        public void Deserialize_NullEnumList_ShouldSetNull()
        {
            var toml = "list = null\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<DayOfWeek>>(toml);
            Assert.That(deserializedList.IsNull, Is.EqualTo(true), "List should be null");
        }
        
        [Test]
        public void Deserialize_EmptyEnumList_ShouldSetEmpty()
        {
            var toml = "list = []\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<DayOfWeek>>(toml);
            Assert.That(deserializedList.Count, Is.EqualTo(0), "List should be null");
        }
        
        [Test]
        public void Deserialize_EnumList_ShouldSetValues()
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
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<DayOfWeek>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_EnumListMultiline_ShouldSetValues()
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
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<DayOfWeek>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_EnumFlagsList_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                FileAttributes.Normal,
                FileAttributes.System,
                FileAttributes.Compressed | FileAttributes.Encrypted
            };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x:F}\"");
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<FileAttributes>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_EnumFlagsListMultiline_ShouldSetValues()
        {
            var expectedValues = new[]
            {
                FileAttributes.Normal,
                FileAttributes.System,
                FileAttributes.Compressed | FileAttributes.Encrypted
            };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x:F}\"");
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<FileAttributes>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }
    }
}
