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
        public void Serialize_NullEnumList_ShouldBeNull()
        {
            var list = SerializableList<DayOfWeek>.Null();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyEnumList_ShouldBeEmpty()
        {
            var list = SerializableList<DayOfWeek>.Empty();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EnumList_ShouldQuoteStrings()
        {
            var list = SerializableList<DayOfWeek>.WithValues(DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday,
                DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => $"\"{x:F}\"");
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EnumFlagsList_ShouldQuoteStrings()
        {
            var list = SerializableList<FileAttributes>.WithValues(FileAttributes.Normal, FileAttributes.System,
                FileAttributes.Compressed | FileAttributes.Encrypted);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => $"\"{x:F}\"");
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
