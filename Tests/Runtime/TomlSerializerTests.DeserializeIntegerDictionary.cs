using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullIntegerDictionary_ShouldSetNull()
        {
            var toml = "dictionary = null\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<long>>(toml);
            Assert.That(deserializedDictionary.IsNull, Is.EqualTo(true), "Dictionary should be null");
        }
        
        [Test]
        public void Deserialize_EmptyIntegerDictionary_ShouldSetEmpty()
        {
            var toml = "dictionary = {}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<long>>(toml);
            Assert.That(deserializedDictionary.Count, Is.EqualTo(0), "Dictionary should be empty");
        }

        [Test]
        public void Deserialize_Int8Dictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, sbyte>
            {
                { "neg_forty_two", -42 },
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<sbyte>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
        
        [Test]
        public void Deserialize_Int16Dictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, short>
            {
                { "neg_forty_two", -42 },
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<short>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
        
        [Test]
        public void Deserialize_Int32Dictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, int>
            {
                { "neg_forty_two", -42 },
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<int>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
        
        [Test]
        public void Deserialize_Int64Dictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, sbyte>
            {
                { "neg_forty_two", -42 },
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<long>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
        
        [Test]
        public void Deserialize_UInt8Dictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, sbyte>
            {
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<byte>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
        
        [Test]
        public void Deserialize_UInt16Dictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, sbyte>
            {
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<ushort>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
        
        [Test]
        public void Deserialize_UInt32Dictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, uint>
            {
                { "zero", 0 },
                { "pos_forty_two", 42 }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<uint>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
    }
}
