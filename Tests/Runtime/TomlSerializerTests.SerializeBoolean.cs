using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

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

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", boolValue).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
