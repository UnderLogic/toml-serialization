using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("true", true)]
        [TestCase("True", true)]
        [TestCase("TRUE", true)]
        [TestCase("false", false)]
        [TestCase("False", false)]
        [TestCase("FALSE", false)]
        public void Deserialize_BooleanValue_ShouldSetValue(string stringValue, bool expectedValue)
        {
            var toml = new TomlStringBuilder().AppendKey("value").AppendLine(stringValue).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<bool>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
    }
}
