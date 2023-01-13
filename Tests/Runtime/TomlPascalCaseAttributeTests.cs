using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlPascalCaseAttributeTests
    {
        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_Key_ShouldConvertToPascalCase(string value)
        {
            var serializedKeys = new SerializableKeys(value);
            
            var toml = TomlSerializer.Serialize(serializedKeys);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"PascalCaseValue = \"{value}\"";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("hello")]
        [TestCase("world")]
        public void Deserialize_Key_ShouldMapFromPascalCase(string value)
        {
            var toml = $"PascalCaseValue = \"{value}\"\n";

            var deserializedKeys = TomlSerializer.Deserialize<SerializableKeys>(toml);
            Assert.That(deserializedKeys.PascalCaseValue, Is.EqualTo(value));
        }
    }
}
