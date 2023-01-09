using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullStringDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<string>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = "dictionary = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyStringDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<string>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = "dictionary = {}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_StringDictionary_ShouldQuoteStrings()
        {
            var dictionary = new SerializableDictionary<string>
            {
                { "greeting", "Hello, World!" },
                { "foo", "Bar" },
                { "baz", "Qux" }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedKeyValueStrings = dictionary.Select(kv => $"{kv.Key} = \"{kv.Value}\"");
            var expectedToml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_StringDictionary_ShouldEscapeStrings()
        {
            var dictionary = new SerializableDictionary<string>
            {
                { "backslash", "This string has a \\ backslash character" },
                { "tab", "This string has a \t tab character" },
                { "carriage_return", "This string has a \r carriage return character" },
                { "line_feed", "This string has a \n line feed character" },
                { "form_feed", "This string has a \f form feed character" },
                { "double_quote", "This is a \"quoted\" string" }
            };

            var toml = TomlSerializer.Serialize(dictionary);

            var expectedDictionary = new Dictionary<string, string>
            {
                { "backslash", "This string has a \\\\ backslash character" },
                { "tab", "This string has a \\t tab character" },
                { "carriage_return", "This string has a \\r carriage return character" },
                { "line_feed", "This string has a \\n line feed character" },
                { "form_feed", "This string has a \\f form feed character" },
                { "double_quote", "This is a \\\"quoted\\\" string" }
            };

            var expectedKeyValueStrings = expectedDictionary.Select(kv => $"{kv.Key} = \"{kv.Value}\"");
            var expectedToml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_StringDictionary_ShouldAllowCommentChar()
        {
            var dictionary = new SerializableDictionary<string>
            {
                { "comment", "#1 Thing" },
                { "inline_comment", "The #2 Thing" }
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedKeyValueStrings = dictionary.Select(kv => $"{kv.Key} = \"{kv.Value}\"");
            var expectedToml = $"dictionary = {{ {string.Join(", ", expectedKeyValueStrings)} }}\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
