using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullEnumDictionary_ShouldSetNull()
        {
            var toml = "dictionary = null\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<DayOfWeek>>(toml);
            Assert.That(deserializedDictionary.IsNull, Is.EqualTo(true), "Dictionary should be null");
        }
        
        [Test]
        public void Deserialize_EmptyEnumDictionary_ShouldSetEmpty()
        {
            var toml = "dictionary = {}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<DayOfWeek>>(toml);
            Assert.That(deserializedDictionary.Count, Is.EqualTo(0), "Dictionary should be empty");
        }
        
        [Test]
        public void Deserialize_EnumDictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, DayOfWeek>
            {
                { "sunday", DayOfWeek.Sunday },
                { "monday", DayOfWeek.Monday },
                { "tuesday", DayOfWeek.Tuesday },
                { "wednesday", DayOfWeek.Wednesday },
                { "thursday", DayOfWeek.Thursday },
                { "friday", DayOfWeek.Friday },
                { "saturday", DayOfWeek.Saturday }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = \"{kv.Value:F}\"");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<DayOfWeek>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
        
        [Test]
        public void Deserialize_EnumFlagsDictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, FileAttributes>
            {
                { "normal", FileAttributes.Normal },
                { "system", FileAttributes.System },
                { "compressed_encrypted", FileAttributes.Compressed | FileAttributes.Encrypted },
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = \"{kv.Value:F}\"");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<FileAttributes>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
    }
}
