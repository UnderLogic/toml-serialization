using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullFloatList_ShouldBeNull()
        {
            var list = SerializableList<float>.Null();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendNullKeyValue("list").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyFloatList_ShouldBeEmpty()
        {
            var list = SerializableList<float>.Empty();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("list").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatList_ShouldBeLiterals()
        {
            var list = SerializableList<float>.WithValues(-3.14f, 0f, 3.14f);
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendArray("list", list).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatSpecialsList_ShouldBeLiterals()
        {
            var list = SerializableList<float>.WithValues(float.NaN, float.NegativeInfinity, float.PositiveInfinity);
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendArray("list", list).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_NullDoubleList_ShouldBeNull()
        {
            var list = SerializableList<double>.Null();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendNullKeyValue("list").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyDoubleList_ShouldBeEmpty()
        {
            var list = SerializableList<double>.Empty();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("list").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleList_ShouldBeLiterals()
        {
            var list = SerializableList<double>.WithValues(-3.14, 0, 3.14);
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendArray("list", list).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleSpecialsList_ShouldBeLiterals()
        {
            var list = SerializableList<double>.WithValues(double.NaN, double.NegativeInfinity,
                double.PositiveInfinity);
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendArray("list", list).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
