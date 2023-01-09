using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public void Serialize_BooleanValue_ShouldBeLowercase(bool boolValue)
        {
            var value = new SerializableValue<bool>(boolValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = $"value = {boolValue.ToString().ToLowerInvariant()}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
