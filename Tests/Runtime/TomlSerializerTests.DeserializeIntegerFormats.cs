using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("10_000_000", 10000000)]
        [TestCase("0x10_21_42", 0x102142)]
        [TestCase("0o7_7_7", 511)]
        [TestCase("0b1010_1100", 172)]
        public void Deserialize_IntegerSpacing_ShouldSetValue(string stringValue, long expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<long>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("0xaa", 0xaa)]
        [TestCase("0xcafe", 0xcafe)]
        [TestCase("0xdeadbeef", 0xdeadbeef)]
        public void Deserialize_IntegerHexLowercase_ShouldSetValue(string stringValue, long expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<long>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("0xAA", 0xAA)]
        [TestCase("0xCAFE", 0xCAFE)]
        [TestCase("0xDEADBEEF", 0xDEADBEEF)]
        public void Deserialize_IntegerHexUppercase_ShouldSetValue(string stringValue, long expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<long>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("0o5", 5)]
        [TestCase("0o57", 47)]
        [TestCase("0o444", 292)]
        public void Deserialize_IntegerOctal_ShouldSetValue(string stringValue, long expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<long>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("0b1", 1)]
        [TestCase("0b10", 2)]
        [TestCase("0b101010", 42)]
        public void Deserialize_IntegerBinary_ShouldSetValue(string stringValue, long expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<long>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
    }
}
