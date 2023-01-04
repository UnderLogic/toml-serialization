using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_DateTimeFormat_ShouldSetEqual()
        {
            var serializedDateTime = new WrappedFormattedDate(new DateTime(1979, 5, 27));
            var tomlString = TomlSerializer.Serialize(serializedDateTime);

            var deserializedDateTime = new WrappedFormattedDate(DateTime.UtcNow);
            TomlSerializer.DeserializeInto(tomlString, deserializedDateTime);

            Assert.AreEqual(serializedDateTime.Value, deserializedDateTime.Value);
        }
    }
}
