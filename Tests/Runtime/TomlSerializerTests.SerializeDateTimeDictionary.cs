using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullDateTimeDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<DateTime>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendNullValue("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyDateTimeDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<DateTime>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendEmptyInlineTable("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DateTimeDictionary_ShouldBeIso8601Format()
        {
            var dictionary = new SerializableDictionary<DateTime>
            {
                { "toml_date", new DateTime(1979, 5, 27) },
                { "y2k_date", new DateTime(2000, 1, 1) }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
