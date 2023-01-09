using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullBooleanDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<bool>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = "dictionary = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyBooleanDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<bool>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = "dictionary = {}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_BooleanDictionary_ShouldBeLowercase()
        {
            var dictionary = new SerializableDictionary<bool>
            {
                { "no", false },
                { "yes", true }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedKeyValueStrings =
                dictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString().ToLowerInvariant()}");
            
            var expectedToml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
