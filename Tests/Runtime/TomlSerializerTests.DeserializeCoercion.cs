using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(-1.5f)]
        [TestCase(-0.5f)]
        [TestCase(0.5f)]
        [TestCase(1.1f)]
        [TestCase(98.6f)]
        public void Deserialize_FloatToInt8_ShouldTruncate(float value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<sbyte>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (sbyte)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5f)]
        [TestCase(-0.5f)]
        [TestCase(0.5f)]
        [TestCase(1.1f)]
        [TestCase(98.6f)]
        public void Deserialize_FloatToInt16_ShouldTruncate(float value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<short>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (short)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5f)]
        [TestCase(-0.5f)]
        [TestCase(0.5f)]
        [TestCase(1.1f)]
        [TestCase(98.6f)]
        public void Deserialize_FloatToInt32_ShouldTruncate(float value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<int>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (int)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5f)]
        [TestCase(-0.5f)]
        [TestCase(0.5f)]
        [TestCase(1.1f)]
        [TestCase(98.6f)]
        public void Deserialize_FloatToInt64_ShouldTruncate(float value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<long>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (long)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5f)]
        [TestCase(-0.5f)]
        [TestCase(0.5f)]
        [TestCase(1.1f)]
        [TestCase(98.6f)]
        public void Deserialize_FloatToUInt8_ShouldTruncate(float value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<byte>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (byte)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5f)]
        [TestCase(-0.5f)]
        [TestCase(0.5f)]
        [TestCase(1.1f)]
        [TestCase(98.6f)]
        public void Deserialize_FloatToUInt16_ShouldTruncate(float value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<ushort>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (ushort)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5f)]
        [TestCase(-0.5f)]
        [TestCase(0.5f)]
        [TestCase(1.1f)]
        [TestCase(98.6f)]
        public void Deserialize_FloatToUInt32_ShouldTruncate(float value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<uint>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (uint)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5)]
        [TestCase(-0.5)]
        [TestCase(0.5)]
        [TestCase(1.1)]
        [TestCase(98.6)]
        public void Deserialize_DoubleToInt8_ShouldTruncate(double value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<sbyte>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (sbyte)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5)]
        [TestCase(-0.5)]
        [TestCase(0.5)]
        [TestCase(1.1)]
        [TestCase(98.6)]
        public void Deserialize_DoubleToInt16_ShouldTruncate(double value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<short>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (short)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5)]
        [TestCase(-0.5)]
        [TestCase(0.5)]
        [TestCase(1.1)]
        [TestCase(98.6)]
        public void Deserialize_DoubleToInt32_ShouldTruncate(double value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<int>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (int)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5)]
        [TestCase(-0.5)]
        [TestCase(0.5)]
        [TestCase(1.1)]
        [TestCase(98.6)]
        public void Deserialize_DoubleToInt64_ShouldTruncate(double value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<long>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (long)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5)]
        [TestCase(-0.5)]
        [TestCase(0.5)]
        [TestCase(1.1)]
        [TestCase(98.6)]
        public void Deserialize_DoubleToUInt8_ShouldTruncate(double value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<byte>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (byte)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5)]
        [TestCase(-0.5)]
        [TestCase(0.5)]
        [TestCase(1.1)]
        [TestCase(98.6)]
        public void Deserialize_DoubleToUInt16_ShouldTruncate(double value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<ushort>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (ushort)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }

        [TestCase(-1.5)]
        [TestCase(-0.5)]
        [TestCase(0.5)]
        [TestCase(1.1)]
        [TestCase(98.6)]
        public void Deserialize_DoubleToUInt32_ShouldTruncate(double value)
        {
            var toml = $"value = {value}";
            var deserializedValue = new WrappedValue<uint>(42);
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            var expectedInt = (uint)value;
            Assert.AreEqual(expectedInt, deserializedValue.Value);
        }
    }
}
