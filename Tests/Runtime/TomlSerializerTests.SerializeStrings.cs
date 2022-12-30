using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_StringWhitespace_ShouldEscapeTab()
        {
            var values = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
            var stringValue = string.Join("\t", values);

            var wrappedString = new WrappedValue<string>(stringValue);
            var toml = TomlSerializer.Serialize(wrappedString);

            var expected = string.Join("\\t", values);
            Assert.AreEqual($"value = \"{expected}\"\n", toml);
        }
        
        [Test]
        public void Serialize_StringWhitespace_ShouldEscapeCarriageReturn()
        {
            var values = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
            var stringValue = string.Join("\r", values);

            var wrappedString = new WrappedValue<string>(stringValue);
            var toml = TomlSerializer.Serialize(wrappedString);

            var escapedString = string.Join("\\r", values);
            Assert.AreEqual($"value = \"{escapedString}\"\n", toml);
        }
        
        [Test]
        public void Serialize_StringWhitespace_ShouldEscapeLineFeed()
        {
            var values = new[] { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };
            var stringValue = string.Join("\n", values);

            var wrappedString = new WrappedValue<string>(stringValue);
            var toml = TomlSerializer.Serialize(wrappedString);

            var escapedString = string.Join("\\n", values);
            Assert.AreEqual($"value = \"{escapedString}\"\n", toml);
        }
        
        [Test]
        public void Serialize_QuotedString_ShouldEscapeQuotes()
        {
            var wrappedString = new WrappedValue<string>("This is a \"quoted\" string");
            var toml = TomlSerializer.Serialize(wrappedString);

            var escapedString = wrappedString.Value.Replace("\"", "\\\"");
            Assert.AreEqual($"value = \"{escapedString}\"\n", toml);
        }
        
        [Test]
        public void Serialize_BackslashString_ShouldEscapeBackslash()
        {
            var wrappedString = new WrappedValue<string>("\\Network\\Share\\Folder");
            var toml = TomlSerializer.Serialize(wrappedString);

            var escapedString = wrappedString.Value.Replace("\\", "\\\\");
            Assert.AreEqual($"value = \"{escapedString}\"\n", toml);
        }
    }
}
