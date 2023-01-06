using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullFloatDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<float>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendNullValue("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyFloatDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<float>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendEmptyInlineTable("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatDictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<float>
            {
                { "neg_pi", -3.14f },
                { "zero", 0f },
                { "pos_pi", 3.14f }

            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatSpecialsDictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<float>
            {
                { "NotANumber", float.NaN },
                { "PositiveInfinity", float.PositiveInfinity },
                { "NegativeInfinity", float.NegativeInfinity }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_NullDoubleDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<double>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendNullValue("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyDoubleDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<double>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendEmptyInlineTable("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleDictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<double>
            {
                { "neg_pi", -3.14 },
                { "zero", 0 },
                { "pos_pi", 3.14 }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleSpecialsDictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<double>
            {
                { "NotANumber", double.NaN },
                { "PositiveInfinity", double.PositiveInfinity },
                { "NegativeInfinity", double.NegativeInfinity }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
