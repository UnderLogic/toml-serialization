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
        public void Deserialize_NullFloatDictionary_ShouldSetNull()
        {
            var toml = "dictionary = null\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<float>>(toml);
            Assert.That(deserializedDictionary.IsNull, Is.EqualTo(true), "Dictionary should be null");
        }
        
        [Test]
        public void Deserialize_EmptyFloatDictionary_ShouldSetEmpty()
        {
            var toml = "dictionary = {}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<float>>(toml);
            Assert.That(deserializedDictionary.Count, Is.EqualTo(0), "Dictionary should be empty");
        }

        [Test]
        public void Deserialize_FloatDictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, float>
            {
                { "pos_pi", 3.14f },
                { "plus_one", 1f },
                { "zero", 0f },
                { "minus_one", -1f },
                { "neg_pi", -3.14f }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv =>
                    $"{kv.Key} = {((double)kv.Value).ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<float>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
        
        [Test]
        public void Deserialize_FloatDictionaryScientific_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, float>
            {
                { "pos_pi", 3.14e10f },
                { "plus_one", 1e10f },
                { "zero", 0f },
                { "minus_one", -1e10f },
                { "neg_pi", -3.14e-10f }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv =>
                    $"{kv.Key} = {((double)kv.Value).ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<float>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }

        [Test]
        public void Deserialize_FloatDictionarySpecials_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, float>
            {
                { "nan", float.NaN },
                { "pos_infinity", float.PositiveInfinity },
                { "neg_infinity", float.NegativeInfinity },
            };

            var expectedValuesDictionary = new Dictionary<string, string>
            {
                { "nan", "nan" },
                { "pos_infinity", "+inf" },
                { "neg_infinity", "-inf" },
            };

            var expectedKeyValueStrings =
                expectedValuesDictionary.Select(kv => $"{kv.Key} = {kv.Value}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<float>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
        
        [Test]
        public void Deserialize_NullDoubleDictionary_ShouldSetNull()
        {
            var toml = "dictionary = null\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<double>>(toml);
            Assert.That(deserializedDictionary.IsNull, Is.EqualTo(true), "Dictionary should be null");
        }
        
        [Test]
        public void Deserialize_EmptyDoubleDictionary_ShouldSetEmpty()
        {
            var toml = "dictionary = {}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<double>>(toml);
            Assert.That(deserializedDictionary.Count, Is.EqualTo(0), "Dictionary should be empty");
        }

        [Test]
        public void Deserialize_DoubleDictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, double>
            {
                { "pos_pi", 3.14 },
                { "plus_one", 1 },
                { "zero", 0 },
                { "minus_one", -1 },
                { "neg_pi", -3.14 }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<double>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }

        [Test]
        public void Deserialize_DoubleDictionaryScientific_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, double>
            {
                { "pos_pi", 3.14e10 },
                { "plus_one", 1e10 },
                { "zero", 0f },
                { "minus_one", -1e10 },
                { "neg_pi", -3.14e-10 }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = {kv.Value.ToString(CultureInfo.InvariantCulture)}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<double>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }

        [Test]
        public void Deserialize_DoubleDictionarySpecials_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, double>
            {
                { "nan", double.NaN },
                { "pos_infinity", double.PositiveInfinity },
                { "neg_infinity", double.NegativeInfinity },
            };

            var expectedValuesDictionary = new Dictionary<string, string>
            {
                { "nan", "nan" },
                { "pos_infinity", "+inf" },
                { "neg_infinity", "-inf" },
            };

            var expectedKeyValueStrings =
                expectedValuesDictionary.Select(kv => $"{kv.Key} = {kv.Value}");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<double>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
    }
}
