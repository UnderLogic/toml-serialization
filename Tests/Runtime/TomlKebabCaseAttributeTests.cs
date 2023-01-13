using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlKebabCaseAttributeTests
    {
        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_Key_ShouldConvertToKebabCase(string value)
        {
            var serializedKeys = new SerializableKeys(value);
            
            var toml = TomlSerializer.Serialize(serializedKeys);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"kebab-case-value = \"{value}\"";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("hello")]
        [TestCase("world")]
        public void Deserialize_Key_ShouldMapFromKebabCase(string value)
        {
            var toml = $"kebab-case-value = \"{value}\"\n";

            var deserializedKeys = TomlSerializer.Deserialize<SerializableKeys>(toml);
            Assert.That(deserializedKeys.KebabCaseValue, Is.EqualTo(value));
        }
    }
}
