using System;
using System.IO;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("Sunday", DayOfWeek.Sunday)]
        [TestCase("Monday", DayOfWeek.Monday)]
        [TestCase("Tuesday", DayOfWeek.Tuesday)]
        [TestCase("Wednesday", DayOfWeek.Wednesday)]
        [TestCase("Thursday", DayOfWeek.Thursday)]
        [TestCase("Friday", DayOfWeek.Friday)]
        [TestCase("Saturday", DayOfWeek.Saturday)]
        public void Deserialize_EnumValue_ShouldSetValue(string stringValue, DayOfWeek expectedValue)
        {
            var toml = new TomlStringBuilder().AppendKeyValue("value", stringValue).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<DayOfWeek>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("Normal", FileAttributes.Normal)]
        [TestCase("System", FileAttributes.System)]
        [TestCase("Compressed, Encrypted", FileAttributes.Compressed | FileAttributes.Encrypted)]
        public void Deserialize_EnumFlagsValue_ShouldSetValue(string stringValue, FileAttributes expectedValue)
        {
            var toml = new TomlStringBuilder().AppendKeyValue("value", stringValue).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<FileAttributes>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
    }
}
