using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullIntegerDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<long>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendNullKeyValue("dictionary").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyIntegerDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<long>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendEmptyInlineTable("dictionary").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int8Dictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<sbyte>
            {
                { "neg_forty_two", -42 },
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int16Dictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<short>
            {
                { "neg_forty_two", -42 },
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int32Dictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<int>
            {
                { "neg_forty_two", -42 },
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int64Dictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<long>
            {
                { "neg_forty_two", -42 },
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_UInt8Dictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<byte>
            {
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_UInt16Dictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<ushort>
            {
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_UInt32Dictionary_ShouldBeLiterals()
        {
            var dictionary = new SerializableDictionary<uint>
            {
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
