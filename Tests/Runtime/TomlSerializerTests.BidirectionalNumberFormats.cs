using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(42)]
        [TestCase(-42)]
        [TestCase(12345)]
        [TestCase(100000)]
        public void SerializeDeserialize_DecimalIntegerValue_ShouldSetEqual(long value)
        {
            var serializedValues = new WrappedIntegerValues(value);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedIntegerValues(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.DecimalValue, deserializedValues.DecimalValue);
        }
        
        [TestCase(0xa)]
        [TestCase(0xcafe)]
        [TestCase(0xcafebabe)]
        [TestCase(0xfeedbeef)]
        public void SerializeDeserialize_HexLowerCaseIntegerValue_ShouldSetEqual(long value)
        {
            var serializedValues = new WrappedIntegerValues(value);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedIntegerValues(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.HexLowerCaseValue, deserializedValues.HexLowerCaseValue);
        }
        
        [TestCase(0xA)]
        [TestCase(0xCAFE)]
        [TestCase(0xCAFEBAFE)]
        [TestCase(0xFEEDBEEF)]
        public void SerializeDeserialize_HexUpperCaseIntegerValue_ShouldSetEqual(long value)
        {
            var serializedValues = new WrappedIntegerValues(value);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedIntegerValues(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.HexUpperCaseValue, deserializedValues.HexUpperCaseValue);
        }
        
        [TestCase(5)]
        [TestCase(42)]
        [TestCase(495)]
        [TestCase(9001)]
        public void SerializeDeserialize_OctalIntegerValue_ShouldSetEqual(long value)
        {
            var serializedValues = new WrappedIntegerValues(value);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedIntegerValues(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.OctalValue, deserializedValues.OctalValue);
        }
        
        [TestCase(0b1)]
        [TestCase(0b1010)]
        [TestCase(0b10101010)]
        public void SerializeDeserialize_BinaryIntegerValue_ShouldSetEqual(long value)
        {
            var serializedValues = new WrappedIntegerValues(value);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedIntegerValues(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.BinaryValue, deserializedValues.BinaryValue);
        }
    }
}
