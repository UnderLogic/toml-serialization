using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullStringValue_ShouldSetNull()
        {
            var toml = new TomlStringBuilder().AppendNullKeyValue("value").ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<string>>(toml);
            Assert.That(deserializedValue.Value, Is.Null);
        }
        
        [TestCase("Hello, World!")]
        [TestCase("The quick brown fox jumps over the lazy dog.")]
        public void Deserialize_StringValue_ShouldSetValue(string stringValue)
        {
            var toml = new TomlStringBuilder().AppendKeyValue("value", stringValue).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<string>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(stringValue));
        }

        [TestCase("This string has a \\\\ backslash character", "This string has a \\ backslash character")]
        [TestCase("This string has a \\t tab character", "This string has a \t tab character")]
        [TestCase("This string has a \\r carriage return character", "This string has a \r carriage return character")]
        [TestCase("This string has a \\n line feed character", "This string has a \n line feed character")]
        [TestCase("This string has a \\f form feed character", "This string has a \f form feed character")]
        [TestCase("This is a \\\"quoted\\\" string", "This is a \"quoted\" string")]
        public void Deserialize_StringValue_ShouldUnescapeChars(string stringValue, string expectedValue)
        {
            var toml = new TomlStringBuilder().AppendKeyValue("value", stringValue).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<string>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("#1 Thing")]
        [TestCase("The #2 Thing")]
        public void Deserialize_StringValue_ShouldAllowCommentChar(string stringValue)
        {
            var toml = new TomlStringBuilder().AppendKeyValue("value", stringValue).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<string>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(stringValue));
        }

        [TestCase(@"'This is a literal string'", @"This is a literal string")]
        [TestCase(@"'This is a ""quoted"" string'", @"This is a ""quoted"" string")]
        [TestCase(@"'C:\Windows\System32'", @"C:\Windows\System32")]
        [TestCase(@"'\\Network\\Share\\Folder'", @"\\Network\\Share\\Folder")]
        [TestCase(@"'<\i\c*\s*>'", @"<\i\c*\s*>")]
        public void Deserialize_LiteralStringValue_ShouldSetValue(string stringValue, string expectedValue)
        {
            var toml = new TomlStringBuilder().AppendKey("value").Append(stringValue).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<string>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("\"\"\"This is a \"quoted\" multiline string\"\"\"", "This is a \"quoted\" multiline string")]
        [TestCase("\"\"\"\nThis is a multiline string\"\"\"", "This is a multiline string")]
        [TestCase("\"\"\"\nThis\nstring\nspans\nmultiple\nlines\n\"\"\"", "This\nstring\nspans\nmultiple\nlines\n")]
        [TestCase("\"\"\"\nThis \\\n  string \\\n  should be \\\n  a single \\\n  line\"\"\"", "This string should be a single line")]
        public void Deserialize_MultilineStringValue_ShouldSetValue(string stringValue, string expectedValue)
        {
            var toml = new TomlStringBuilder().AppendKey("value").Append(stringValue).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<string>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase(@"'This is a literal string'", @"This is a literal string")]
        [TestCase(@"'This is a ""quoted"" string'", @"This is a ""quoted"" string")]
        [TestCase(@"'C:\Windows\System32'", @"C:\Windows\System32")]
        [TestCase(@"'\\Network\\Share\\Folder'", @"\\Network\\Share\\Folder")]
        [TestCase(@"'<\i\c*\s*>'", @"<\i\c*\s*>")]
        public void Deserialize_MultilineLiteralStringValue_ShouldSetValue(string stringValue, string expectedValue)
        {
            var toml = new TomlStringBuilder().AppendKey("value").Append(stringValue).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<string>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
    }
}
