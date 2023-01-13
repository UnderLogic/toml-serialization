using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlBinaryNumberAttributeTests
    {
        [TestCase(10, "0b1010")]
        [TestCase(42, "0b101010")]
        [TestCase(250, "0b11111010")]
        public void Serialize_Integer_ShouldFormatAsBinary(int integerValue, string expectedStringValue)
        {
            var serializedIntegers = new SerializableIntegers(integerValue);
            
            var toml = TomlSerializer.Serialize(serializedIntegers);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"binaryNumber = {expectedStringValue}";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("0b1010", 10)]
        [TestCase("0b101010", 42)]
        [TestCase("0b11111010", 250)]
        public void Deserialize_Integer_ShouldParseBinary(string stringValue, int expectedIntegerValue)
        {
            var toml = $"binaryNumber = {stringValue}\n";

            var deserializedIntegers = TomlSerializer.Deserialize<SerializableIntegers>(toml);
            Assert.That(deserializedIntegers.BinaryNumber, Is.EqualTo(expectedIntegerValue));
        }
    }
}
