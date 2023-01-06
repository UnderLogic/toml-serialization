using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullBooleanDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<bool>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendNullKeyValue("dictionary").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyBooleanDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<bool>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendEmptyInlineTable("dictionary").ToString();
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

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
