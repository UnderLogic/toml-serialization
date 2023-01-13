using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlHexNumberAttributeTests
    {
        [TestCase(0xa, "0xa")]
        [TestCase(0xc2, "0xc2")]
        [TestCase(0xcafe, "0xcafe")]
        [TestCase(0xdeadbeef, "0xdeadbeef")]
        public void Serialize_Integer_ShouldFormatAsHexLowercase(long integerValue, string expectedStringValue)
        {
            var serializedIntegers = new SerializableIntegers(integerValue);
            
            var toml = TomlSerializer.Serialize(serializedIntegers);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"lowerHexNumber = {expectedStringValue}";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("0xa", 0xa)]
        [TestCase("0xc2", 0xc2)]
        [TestCase("0xcafe", 0xcafe)]
        [TestCase("0xdeadbeef", 0xdeadbeef)]
        public void Deserialize_Integer_ShouldParseLowercaseHex(string stringValue, long expectedIntegerValue)
        {
            var toml = $"lowerHexNumber = {stringValue}\n";

            var deserializedIntegers = TomlSerializer.Deserialize<SerializableIntegers>(toml);
            Assert.That(deserializedIntegers.LowerHexNumber, Is.EqualTo(expectedIntegerValue));
        }
        
        [TestCase(0xA, "0xA")]
        [TestCase(0xC2, "0xC2")]
        [TestCase(0xCAFE, "0xCAFE")]
        [TestCase(0xDEADBEEF, "0xDEADBEEF")]
        public void Serialize_Integer_ShouldFormatAsHexUppercase(long integerValue, string expectedStringValue)
        {
            var serializedIntegers = new SerializableIntegers(integerValue);
            
            var toml = TomlSerializer.Serialize(serializedIntegers);
            var tomlLines = toml.Split("\n");
            
            var expectedToml = $"upperHexNumber = {expectedStringValue}";
            Assert.Contains(expectedToml, tomlLines);
        }

        [TestCase("0xA", 0xA)]
        [TestCase("0xC2", 0xC2)]
        [TestCase("0xCAFE", 0xCAFE)]
        [TestCase("0xDEADBEEF", 0xDEADBEEF)]
        public void Deserialize_Integer_ShouldParseUppercaseHex(string stringValue, long expectedIntegerValue)
        {
            var toml = $"upperHexNumber = {stringValue}\n";

            var deserializedIntegers = TomlSerializer.Deserialize<SerializableIntegers>(toml);
            Assert.That(deserializedIntegers.UpperHexNumber, Is.EqualTo(expectedIntegerValue));
        }
    }
}
