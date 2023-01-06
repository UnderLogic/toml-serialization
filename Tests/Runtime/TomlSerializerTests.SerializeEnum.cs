using System;
using System.IO;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Monday)]
        [TestCase(DayOfWeek.Tuesday)]
        [TestCase(DayOfWeek.Wednesday)]
        [TestCase(DayOfWeek.Thursday)]
        [TestCase(DayOfWeek.Friday)]
        [TestCase(DayOfWeek.Saturday)]
        public void Serialize_EnumValue_ShouldQuoteString(DayOfWeek enumValue)
        {
            var value = new SerializableValue<DayOfWeek>(enumValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", enumValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [TestCase(FileAttributes.Normal)]
        [TestCase(FileAttributes.System)]
        [TestCase(FileAttributes.Compressed | FileAttributes.Encrypted)]
        public void Serialize_EnumFlagsValue_ShouldQuoteString(FileAttributes flagsValue)
        {
            var value = new SerializableValue<FileAttributes>(flagsValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", flagsValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
