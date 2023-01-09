using System;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullDateTimeDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<DateTime>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = "dictionary = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyDateTimeDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<DateTime>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = "dictionary = {}\n";
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

            var expectedValueStrings = dictionary.Select(kv => $"{kv.Key} = {kv.Value:yyyy-MM-dd HH:mm:ss.fffZ}");
            var expectedToml = $"dictionary = {{ {string.Join(", ", expectedValueStrings)} }}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
