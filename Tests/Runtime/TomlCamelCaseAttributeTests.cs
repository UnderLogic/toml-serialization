using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlCamelCaseAttributeTests
    {
        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_Key_ShouldConvertToCamelCase(string value)
        {
            var serializedKeys = new SerializableKeys(value);
            
            var toml = TomlSerializer.Serialize(serializedKeys);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"camelCaseValue = \"{value}\"";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("hello")]
        [TestCase("world")]
        public void Deserialize_Key_ShouldMapFromCamelCase(string value)
        {
            var toml = $"camelCaseValue = \"{value}\"\n";

            var deserializedKeys = TomlSerializer.Deserialize<SerializableKeys>(toml);
            Assert.That(deserializedKeys.CamelCaseValue, Is.EqualTo(value));
        }
    }
}
