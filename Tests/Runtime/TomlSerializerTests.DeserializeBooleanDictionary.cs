using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullBoolDictionary_ShouldSetNull()
        {
            var toml = "dictionary = null\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<bool>>(toml);
            Assert.That(deserializedDictionary.IsNull, Is.EqualTo(true), "Dictionary should be null");
        }
        
        [Test]
        public void Deserialize_EmptyBoolDictionary_ShouldSetEmpty()
        {
            var toml = "dictionary = {}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<bool>>(toml);
            Assert.That(deserializedDictionary.Count, Is.EqualTo(0), "Dictionary should be empty");
        }
        
        [Test]
        public void Deserialize_BoolDictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, bool>
            {
                { "no", false },
                { "yes", true }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString().ToLowerInvariant()}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<bool>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
    }
}
