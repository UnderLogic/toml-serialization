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
        public void Serialize_NullEnumArray_ShouldBeNull()
        {
            var array = SerializableArray<DayOfWeek>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendNullValue("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyEnumArray_ShouldBeEmpty()
        {
            var array = SerializableArray<DayOfWeek>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EnumArray_ShouldQuoteStrings()
        {
            var array = SerializableArray<DayOfWeek>.WithValues(DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday,
                DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EnumFlagsArray_ShouldQuoteStrings()
        {
            var array = SerializableArray<FileAttributes>.WithValues(FileAttributes.Normal, FileAttributes.System,
                FileAttributes.Compressed | FileAttributes.Encrypted);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
