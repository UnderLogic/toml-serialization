using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(-3.14f)]
        [TestCase(0.0f)]
        [TestCase(3.14f)]
        public void Serialize_FloatValue_ShouldBeLiteral(float floatValue)
        {
            var value = new SerializableValue<float>(floatValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", floatValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatNaN_ShouldBeNaN()
        {
            var value = new SerializableValue<float>(float.NaN);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendNaNValue("value").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatPositiveInfinity_ShouldBePositiveInfinity()
        {
            var value = new SerializableValue<float>(float.PositiveInfinity);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendPositiveInfinityValue("value").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatNegativeInfinity_ShouldBeNegativeInfinity()
        {
            var value = new SerializableValue<float>(float.NegativeInfinity);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendNegativeInfinityValue("value").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [TestCase(-3.14)]
        [TestCase(0.0)]
        [TestCase(3.14)]
        public void Serialize_DoubleValue_ShouldBeLiteral(double floatValue)
        {
            var value = new SerializableValue<double>(floatValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", floatValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleNaN_ShouldBeNaN()
        {
            var value = new SerializableValue<double>(double.NaN);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendNaNValue("value").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoublePositiveInfinity_ShouldBePositiveInfinity()
        {
            var value = new SerializableValue<double>(double.PositiveInfinity);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendPositiveInfinityValue("value").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleNegativeInfinity_ShouldBeNegativeInfinity()
        {
            var value = new SerializableValue<double>(double.NegativeInfinity);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendNegativeInfinityValue("value").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
