using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(1, "1")]
        [TestCase(100, "100")]
        [TestCase(10000, "10000")]
        public void Serialize_DecimalLowerCaseInteger_ShouldSerializeAsDecimal(long value, string expectedString)
        {
            var wrappedValues = new WrappedIntegerValues(value);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            Assert.Contains($"decimalValue = {expectedString}", lines, "Should contain decimal number format");
        }

        [TestCase(0xc2, "0xc2")]
        [TestCase(0xcafe, "0xcafe")]
        [TestCase(0xdeadbeef, "0xdeadbeef")]
        public void Serialize_HexLowerCaseInteger_ShouldSerializeAsHex(long value, string expectedString)
        {
            var wrappedValues = new WrappedIntegerValues(value);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            Assert.Contains($"hexLowerCaseValue = {expectedString}", lines,
                "Should contain hex number format (lowercase)");
        }

        [TestCase(0xC2, "0xC2")]
        [TestCase(0xCAFE, "0xCAFE")]
        [TestCase(0xDEADBEEF, "0xDEADBEEF")]
        public void Serialize_HexUpperCaseInteger_ShouldSerializeAsHex(long value, string expectedString)
        {
            var wrappedValues = new WrappedIntegerValues(value);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            Assert.Contains($"hexUpperCaseValue = {expectedString}", lines,
                "Should contain hex number format (uppercase)");
        }

        [TestCase(5, "0o5")]
        [TestCase(42, "0o52")]
        [TestCase(495, "0o757")]
        public void Serialize_OctalInteger_ShouldSerializeAsOctal(long value, string expectedString)
        {
            var wrappedValues = new WrappedIntegerValues(value);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            Assert.Contains($"octalValue = {expectedString}", lines, "Should contain octal number format");
        }

        [TestCase(0b1, "0b1")]
        [TestCase(0b1010, "0b1010")]
        [TestCase(0b10110101, "0b10110101")]
        public void Serialize_BinaryInteger_ShouldSerializeAsBinary(long value, string expectedString)
        {
            var wrappedValues = new WrappedIntegerValues(value);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            Assert.Contains($"binaryValue = {expectedString}", lines, "Should contain binary number format");
        }
    }
}
