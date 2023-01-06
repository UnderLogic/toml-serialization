using System;
using System.IO;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullEnumDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<DayOfWeek>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendNullValue("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyEnumDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<DayOfWeek>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendEmptyInlineTable("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EnumDictionary_ShouldQuoteStrings()
        {
            var dictionary = new SerializableDictionary<DayOfWeek>
            {
                { "sunday", DayOfWeek.Sunday },
                { "monday", DayOfWeek.Monday },
                { "tuesday", DayOfWeek.Tuesday },
                { "wednesday", DayOfWeek.Wednesday },
                { "thursday", DayOfWeek.Thursday },
                { "friday", DayOfWeek.Friday },
                { "saturday", DayOfWeek.Saturday }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EnumFlagsDictionary_ShouldQuoteStrings()
        {
            var dictionary = new SerializableDictionary<FileAttributes>
            {
                { "normal", FileAttributes.Normal },
                { "system", FileAttributes.System },
                { "compressed_encrypted", FileAttributes.Compressed | FileAttributes.Encrypted }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
