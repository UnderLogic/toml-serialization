using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullFloatArray_ShouldBeNull()
        {
            var array = SerializableArray<float>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendNullValue("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyFloatArray_ShouldBeEmpty()
        {
            var array = SerializableArray<float>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatArray_ShouldBeLiterals()
        {
            var array = SerializableArray<float>.WithValues(-3.14f, 0f, 3.14f);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatSpecialsArray_ShouldBeLiterals()
        {
            var array = SerializableArray<float>.WithValues(float.NaN, float.NegativeInfinity, float.PositiveInfinity);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_NullDoubleArray_ShouldBeNull()
        {
            var array = SerializableArray<double>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendNullValue("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyDoubleArray_ShouldBeEmpty()
        {
            var array = SerializableArray<double>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleArray_ShouldBeLiterals()
        {
            var array = SerializableArray<double>.WithValues(-3.14, 0, 3.14);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleSpecialsArray_ShouldBeLiterals()
        {
            var array = SerializableArray<double>.WithValues(double.NaN, double.NegativeInfinity,
                double.PositiveInfinity);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
