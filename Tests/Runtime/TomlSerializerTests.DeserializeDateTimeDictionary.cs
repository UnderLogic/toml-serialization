using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullDateTimeDictionary_ShouldSetNull()
        {
            var toml = "dictionary = null\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<DateTime>>(toml);
            Assert.That(deserializedDictionary.IsNull, Is.EqualTo(true), "Dictionary should be null");
        }
        
        [Test]
        public void Deserialize_EmptyDateTimeDictionary_ShouldSetEmpty()
        {
            var toml = "dictionary = {}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<DateTime>>(toml);
            Assert.That(deserializedDictionary.Count, Is.EqualTo(0), "Dictionary should be empty");
        }

        [Test]
        public void Deserialize_DateTimeDictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, DateTime>
            {
                { "early_date", new DateTime(1979, 5, 27) },
                { "y2k_date", new DateTime(2000, 1, 1) }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value:yyy-MM-dd HH:mm:ss.fffZ}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<DateTime>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
    }
}
