using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlOctalNumberAttributeTests
    {
        [TestCase(2, "0o2")]
        [TestCase(42, "0o52")]
        [TestCase(511, "0o777")]
        public void Serialize_Integer_ShouldFormatAsOctal(int integerValue, string expectedStringValue)
        {
            var serializableIntegers = new SerializableIntegers(integerValue);
            
            var toml = TomlSerializer.Serialize(serializableIntegers);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"octalNumber = {expectedStringValue}";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("0o2", 2)]
        [TestCase("0o52", 42)]
        [TestCase("0o777", 511)]
        public void Deserialize_Integer_ShouldParseOctal(string stringValue, int expectedIntegerValue)
        {
            var toml = $"octalNumber = {stringValue}\n";

            var deserializedIntegers = TomlSerializer.Deserialize<SerializableIntegers>(toml);
            Assert.That(deserializedIntegers.OctalNumber, Is.EqualTo(expectedIntegerValue));
        }
    }
}
