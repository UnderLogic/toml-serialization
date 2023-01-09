using System.Globalization;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

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

            var expectedToml = $"value = {((double)floatValue).ToString(CultureInfo.InvariantCulture)}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatNaN_ShouldBeNaN()
        {
            var value = new SerializableValue<float>(float.NaN);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = "value = nan\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatPositiveInfinity_ShouldBePositiveInfinity()
        {
            var value = new SerializableValue<float>(float.PositiveInfinity);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = "value = +inf\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatNegativeInfinity_ShouldBeNegativeInfinity()
        {
            var value = new SerializableValue<float>(float.NegativeInfinity);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = "value = -inf\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [TestCase(-3.14)]
        [TestCase(0.0)]
        [TestCase(3.14)]
        public void Serialize_DoubleValue_ShouldBeLiteral(double floatValue)
        {
            var value = new SerializableValue<double>(floatValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = $"value = {floatValue.ToString(CultureInfo.InvariantCulture)}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleNaN_ShouldBeNaN()
        {
            var value = new SerializableValue<double>(double.NaN);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = "value = nan\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoublePositiveInfinity_ShouldBePositiveInfinity()
        {
            var value = new SerializableValue<double>(double.PositiveInfinity);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = "value = +inf\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleNegativeInfinity_ShouldBeNegativeInfinity()
        {
            var value = new SerializableValue<double>(double.NegativeInfinity);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = "value = -inf\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
