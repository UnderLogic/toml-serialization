using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlSnakeCaseAttributeTests
    {
        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_Key_ShouldConvertToSnakeCase(string value)
        {
            var serializedKeys = new SerializableKeys(value);
            
            var toml = TomlSerializer.Serialize(serializedKeys);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"snake_case_value = \"{value}\"";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("hello")]
        [TestCase("world")]
        public void Deserialize_Key_ShouldMapFromSnakeCase(string value)
        {
            var toml = $"snake_case_value = \"{value}\"\n";

            var deserializedKeys = TomlSerializer.Deserialize<SerializableKeys>(toml);
            Assert.That(deserializedKeys.SnakeCaseValue, Is.EqualTo(value));
        }
        
        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_Key_ShouldConvertToUpperSnakeCase(string value)
        {
            var serializedKeys = new SerializableKeys(value);
            
            var toml = TomlSerializer.Serialize(serializedKeys);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"UPPER_SNAKE_CASE_VALUE = \"{value}\"";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("hello")]
        [TestCase("world")]
        public void Deserialize_Key_ShouldMapFromUpperSnakeCase(string value)
        {
            var toml = $"UPPER_SNAKE_CASE_VALUE = \"{value}\"\n";

            var deserializedKeys = TomlSerializer.Deserialize<SerializableKeys>(toml);
            Assert.That(deserializedKeys.UpperSnakeCaseValue, Is.EqualTo(value));
        }
    }
}
