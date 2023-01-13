using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlLiteralAttributeTests
    {
        [TestCase("Hello, world!")]
        [TestCase("The quick brown fox jumps over the lazy dog.")]
        public void Serialize_LiteralString_ShouldUseSingleQuotes(string value)
        {
            var serializedStrings = new SerializableStrings(value);
            
            var toml = TomlSerializer.Serialize(serializedStrings);
            var tomlLines = toml.Split("\n");

            var expectedToml = $"literalString = '{value}'";
            Assert.Contains(expectedToml, tomlLines);
        }
        
        [TestCase(@"C:\Users\username\Documents\")]
        [TestCase(@"\\Network\\Share\\Folder")]
        [TestCase(@"<html><body><h1>Hello, world!</h1></body></html>")]
        [TestCase(@"This is a ""quoted"" string")]
        public void Serialize_LiteralString_ShouldNotEscapeChars(string value)
        {
            var serializedStrings = new SerializableStrings(value);
            
            var toml = TomlSerializer.Serialize(serializedStrings);
            var tomlLines = toml.Split("\n");

            var expectedToml = $"literalString = '{value}'";
            Assert.Contains(expectedToml, tomlLines);
        }
        
        [TestCase("This is a single 'quoted' string")]
        [TestCase("'Hey,' he said")]
        public void Serialize_LiteralString_ShouldEscapeSingleQuotes(string value)
        {
            var serializedStrings = new SerializableStrings(value);
            
            var toml = TomlSerializer.Serialize(serializedStrings);
            var tomlLines = toml.Split("\n");

            var expectedToml = $"literalString = '''{value}'''";
            Assert.Contains(expectedToml, tomlLines);
        }
    }
}
