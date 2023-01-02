using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("0xC2", 0xC2)]
        [TestCase("0xCAFE", 0xCAFE)]
        [TestCase("0xCAFEBABE", 0xCAFEBABE)]
        [TestCase("0xdead_beef", 0xdeadbeef)]
        public void Deserialize_HexInteger_ShouldSetValue(string valueString, long expectedValue)
        {
            var wrappedValue = new WrappedValue<long>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);

            Assert.AreEqual(expectedValue, wrappedValue.Value);
        }
        
        [TestCase("0o5", 5)]
        [TestCase("0o55", 45)]
        [TestCase("0o757", 495)]
        public void Deserialize_OctInteger_ShouldSetValue(string valueString, long expectedValue)
        {
            var wrappedValue = new WrappedValue<long>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);
            
            Assert.AreEqual(expectedValue, wrappedValue.Value);
        }
        
        [TestCase("0b1", 0b1)]
        [TestCase("0b1010", 0b1010)]
        [TestCase("0b111010101", 0b111010101)]
        public void Deserialize_BinInteger_ShouldSetValue(string valueString, long expectedValue)
        {
            var wrappedValue = new WrappedValue<long>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);

            Assert.AreEqual(expectedValue, wrappedValue.Value);
        }

        [TestCase("1_00", 100)]
        [TestCase("-9_999", -9999)]
        [TestCase("52_530", 52530)]
        [TestCase("1_234_567", 1234567)]
        public void Deserialize_IntegerWithUnderscores_ShouldSetValue(string valueString, long expectedValue)
        {
            var wrappedValue = new WrappedValue<long>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);
            Assert.AreEqual(expectedValue, wrappedValue.Value);
        }

        [TestCase("1_00.00", 100.00)]
        [TestCase("-9_999.99", -9999.99)]
        [TestCase("52_530.535_902", 52530.535902)]
        [TestCase("1_234_567e-4", 1234567e-4)]
        public void Deserialize_DoubleWithUnderscores_ShouldSetValue(string valueString, double expectedValue)
        {
            var wrappedValue = new WrappedValue<double>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);

            Assert.AreEqual(expectedValue, wrappedValue.Value);
        }
    }
}
