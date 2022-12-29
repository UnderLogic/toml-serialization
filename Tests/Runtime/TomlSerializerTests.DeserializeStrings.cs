using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_BasicString_ShouldSetValue()
        {
            var expectedString = "Hello World!";
            var toml = $"value = \"{expectedString}\"\n";

            var wrappedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(toml, wrappedValue);

            Assert.AreEqual(expectedString, wrappedValue.Value);
        }
        
        [Test]
        public void Deserialize_BasicQuotedString_ShouldSetValue()
        {
            var expectedString = "This is a \"quoted\" string";
            var escapedString = expectedString.Replace("\"", "\\\"");
            var toml = $"value = \"{escapedString}\"\n";

            var wrappedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(toml, wrappedValue);

            Assert.AreEqual(expectedString, wrappedValue.Value);
        }
        
        [Test]
        public void Deserialize_BasicBackslashString_ShouldSetValue()
        {
            var expectedString = "C:\\Windows\\System32";
            var escapedString = expectedString.Replace("\\", "\\\\");
            var toml = $"value = \"{escapedString}\"\n";

            var wrappedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(toml, wrappedValue);

            Assert.AreEqual(expectedString, wrappedValue.Value);
        }
        
        [Test]
        public void Deserialize_BasicUnicodeString_ShouldSetValue()
        {
            var expectedString = "Jos\u00E9";
            const string toml = "value = \"Jos\\u00E9\"\n";

            var wrappedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(toml, wrappedValue);

            Assert.AreEqual(expectedString, wrappedValue.Value);
        }
        
        [Test]
        public void Deserialize_BasicMultilineString_ShouldSetValue()
        {
            var expectedString = "Roses are red\nViolets are blue";
            var toml = $"value = \"\"\"\n{expectedString}\"\"\"\n";

            var wrappedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(toml, wrappedValue);

            Assert.AreEqual(expectedString, wrappedValue.Value);
        }
        
        [Test]
        public void Deserialize_BasicMultilineContinuedString_ShouldSetValue()
        {
            var expectedString = "The quick brown fox jumps over the lazy dog";
            const string toml = "value = \"\"\"\nThe quick brown \\\n\n\nfox jumps over \\\n  the lazy dog.\"\"\"\n";

            var wrappedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(toml, wrappedValue);

            Assert.AreEqual(expectedString, wrappedValue.Value);
        }
        
        [Test]
        public void Deserialize_BasicMultilineQuotedString_ShouldSetValue()
        {
            var expectedString = "Here are two quotation marks: \"\". Simple enough.";
            var toml = $"value = \"\"\"{expectedString}\"\"\"\n";

            var wrappedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(toml, wrappedValue);

            Assert.AreEqual(expectedString, wrappedValue.Value);
        }
        
        [TestCase(@"C:\Windows\System32")]
        [TestCase(@"\\Network\\Share\\Folder")]
        [TestCase(@"This is a ""quoted"" string")]
        [TestCase(@"<\i\c*\s*>")]
        public void Deserialize_LiteralString_ShouldSetValue(string expectedString)
        {
            var toml = $"value = '{expectedString}'\n";
            var wrappedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(toml, wrappedValue);

            Assert.AreEqual(expectedString, wrappedValue.Value);
        }
        
        [TestCase(@"I [dw]on't need \d{2} apples")]
        [TestCase(@"The first newline is\ntrimmed in raw strings.\n   All other whitespace\n   is preserved.\n")]
        [TestCase(@"Here are two quotation marks: """". Simple enough.")]
        [TestCase(@"'That, ' she said, 'is not true.'")]
        public void Deserialize_LiteralMultilineString_ShouldSetValue(string expectedString)
        {
            var toml = $"value = '''\n{expectedString}'''\n";
            var wrappedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(toml, wrappedValue);

            Assert.AreEqual(expectedString, wrappedValue.Value);
        }
    }
}
