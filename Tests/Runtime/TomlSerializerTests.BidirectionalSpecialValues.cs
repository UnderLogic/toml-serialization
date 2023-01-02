using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_FloatPositiveInfinity_ShouldSetEqual()
        {
            var serializedValue = new WrappedValue<float>(float.PositiveInfinity);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<float>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }

        [Test]
        public void SerializeDeserialize_FloatNegativeInfinity_ShouldSetEqual()
        {
            var serializedValue = new WrappedValue<float>(float.NegativeInfinity);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<float>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }

        [Test]
        public void SerializeDeserialize_FloatNaN_ShouldSetEqual()
        {
            var serializedValue = new WrappedValue<float>(float.NaN);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<float>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }

        [Test]
        public void SerializeDeserialize_DoublePositiveInfinity_ShouldSetEqual()
        {
            var serializedValue = new WrappedValue<double>(double.PositiveInfinity);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<double>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }

        [Test]
        public void SerializeDeserialize_DoubleNegativeInfinity_ShouldSetEqual()
        {
            var serializedValue = new WrappedValue<double>(double.NegativeInfinity);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<double>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }

        [Test]
        public void SerializeDeserialize_DoubleNaN_ShouldSetEqual()
        {
            var serializedValue = new WrappedValue<double>(double.NaN);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<double>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
    }
}
