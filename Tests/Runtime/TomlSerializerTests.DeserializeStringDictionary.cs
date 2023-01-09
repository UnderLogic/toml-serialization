using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullStringDictionary_ShouldSetNull()
        {
            var toml = "dictionary = null\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<string>>(toml);
            Assert.That(deserializedDictionary.IsNull, Is.EqualTo(true), "Dictionary should be null");
        }
        
        [Test]
        public void Deserialize_EmptyStringDictionary_ShouldSetEmpty()
        {
            var toml = "dictionary = {}\n";
            
            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<string>>(toml);
            Assert.That(deserializedDictionary.Count, Is.EqualTo(0), "Dictionary should be empty");
        }

        [Test]
        public void Deserialize_StringDictionary_ShouldSetValues()
        {
            var expectedDictionary = new Dictionary<string, string>
            {
                { "greeting", "Hello, World!" },
                { "farewell", "Goodbye, World!" }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = \"{kv.Value}\"");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<string>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }

        [Test]
        public void Deserialize_StringDictionary_ShouldUnescapeChars()
        {
            var expectedDictionary = new Dictionary<string, string>
            {
                { "backslash", "\\" },
                { "tab", "\t" },
                { "carriage_return", "\r" },
                { "line_feed", "\n" },
                { "form_feed", "\f" },
                { "double_quote", "\"" },
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

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<string>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }

        [Test]
        public void Deserialize_StringDictionary_ShouldAllowCommentChar()
        {
            var expectedDictionary = new Dictionary<string, string>
            {
                { "comment", "#1 Thing" },
                { "inline_comment", "The #2 Thing" }
            };

            var expectedKeyValueStrings =
                expectedDictionary.Select(kv => $"{kv.Key} = \"{kv.Value}\"");
            var toml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";

            var deserializedDictionary = TomlSerializer.Deserialize<SerializableDictionary<string>>(toml);
            Assert.That(deserializedDictionary, Is.EqualTo(expectedDictionary));
        }
    }
}
