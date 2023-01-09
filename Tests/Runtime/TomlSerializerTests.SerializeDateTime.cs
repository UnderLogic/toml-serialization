using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_DateTimeValue_ShouldBeIso8601Format()
        {
            var expectedDateTime = new DateTime(1979, 5, 27);
            var value = new SerializableValue<DateTime>(expectedDateTime);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = $"value = {expectedDateTime:yyyy-MM-dd HH:mm:ss.fffZ}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
