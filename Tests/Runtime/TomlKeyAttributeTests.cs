using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlKeyAttributeTests
    {
        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_Key_ShouldRenameKey(string value)
        {
            var serializedKeys = new SerializableKeys(value);
            
            var toml = TomlSerializer.Serialize(serializedKeys);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"renamedValue = \"{value}\"";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("hello")]
        [TestCase("world")]
        public void Deserialize_Key_ShouldMapFromRenamedKey(string value)
        {
            var toml = $"renamedValue = \"{value}\"\n";

            var deserializedKeys = TomlSerializer.Deserialize<SerializableKeys>(toml);
            Assert.That(deserializedKeys.Value, Is.EqualTo(value));
        }
    }
}
