using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullCharDictionary_ShouldSetNull()
        {
            var toml = "dictionary = null\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<char>>(toml);
            Assert.That(deserializedDictionary.IsNull, Is.EqualTo(true), "Dictionary should be null");
        }
        
        [Test]
        public void Deserialize_EmptyCharDictionary_ShouldSetEmpty()
        {
            var toml = "dictionary = {}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<char>>(toml);
            Assert.That(deserializedDictionary.Count, Is.EqualTo(0), "Dictionary should be empty");
        }

        [Test]
        public void Deserialize_CharDictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, char>
            {
                { "letter_a", 'A' },
                { "letter_z", 'Z' },
                { "digit_0", '0' },
                { "digit_9", '9' },
                { "underscore", '_' },
                { "minus", '-' },
                { "plus", '+' }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = \"{kv.Value}\"");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<char>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }

        [Test]
        public void Deserialize_CharDictionary_ShouldUnescapeChars()
        {
            var expectedDictionary = new Dictionary<string, char>
            {
                { "backslash", '\\' },
                { "tab", '\t' },
                { "carriage_return", '\r' },
                { "line_feed", '\n' },
                { "form_feed", '\f' },
                { "double_quote", '"' },
            };

            var escapedDictionary = new Dictionary<string, string>
            {
                { "backslash", "\\\\" },
                { "tab", "\\t" },
                { "carriage_return", "\\r" },
                { "line_feed", "\\n" },
                { "form_feed", "\\f" },
                { "double_quote", "\\\"" },
            };

            var escapedKeyValueStrings =
                escapedDictionary.Select(kv => $"{kv.Key} = \"{kv.Value}\"");
            var toml = $"dictionary = {{ {string.Join(", ", escapedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<char>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }

        [Test]
        public void Deserialize_CharDictionary_ShouldAllowCommentChar()
        {
            var expectedDictionary = new Dictionary<string, char>
            {
                { "comment_char", '#' }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = \"{kv.Value}\"");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<char>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
    }
}
