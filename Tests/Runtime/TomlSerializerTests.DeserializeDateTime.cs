using System;
using System.Globalization;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("1979-05-27T07:32:00Z")]
        [TestCase("1979-05-27T00:32:00-07:00")]
        [TestCase("1979-05-27T00:32:00.999999-07:00")]
        [TestCase("1979-05-27 07:32:00Z")]
        [TestCase("1979-05-27 00:32:00-07:00")]
        [TestCase("1979-05-27 00:32:00.999999-07:00")]
        [TestCase("1979-05-27T07:32:00")]
        [TestCase("1979-05-27 07:32:00")]
        [TestCase("1979-05-27")]
        [TestCase("07:32:00")]
        [TestCase("00:32:00.999")]
        public void Deserialize_DateTimeValue_ShouldParseFormats(string stringValue)
        {
            var expectedDate = DateTime.Parse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<DateTime>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedDate));
        }
    }
}
