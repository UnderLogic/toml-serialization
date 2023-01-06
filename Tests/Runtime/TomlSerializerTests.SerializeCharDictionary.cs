using System.Collections.Generic;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullCharDictionary_ShouldBeNull()
        {
            var dictionary = SerializableDictionary<char>.Null();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendNullValue("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyCharDictionary_ShouldBeEmpty()
        {
            var dictionary = SerializableDictionary<char>.Empty();
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendEmptyInlineTable("dictionary").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_CharDictionary_ShouldBeQuoted()
        {
            var dictionary = new SerializableDictionary<char>
            {
                { "letter_a", 'A' },
                { "letter_z", 'Z' },
                { "digit_0", '0' },
                { "digit_9", '9' },
                { "underscore", '_' },
                { "minus_sign", '-' },
                { "plus_sign", '+' },
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_CharDictionary_ShouldBeEscaped()
        {
            var dictionary = new SerializableDictionary<char>
            {
                { "backslash", '\\' },
                { "tab", '\t' },
                { "carriage_return", '\r' },
                { "line_feed", '\n' },
                { "form_feed", '\f' },
                { "double_quote", '\"' },
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedDictionary = new Dictionary<string, string>
            {
                { "backslash", "\\\\" },
                { "tab", "\\t" },
                { "carriage_return", "\\r" },
                { "line_feed", "\\n" },
                { "form_feed", "\\f" },
                { "double_quote", "\\\"" },
            };
            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", expectedDictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_CharDictionary_ShouldAllowCommentChar()
        {
            var dictionary = new SerializableDictionary<char>
            {
                { "comment_char", '#' },
            };
            var toml = TomlSerializer.Serialize(dictionary);

            var expectedToml = new TomlStringBuilder().AppendInlineTable("dictionary", dictionary).AppendLine()
                .ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
