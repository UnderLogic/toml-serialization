using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlMultilineAttributeTests
    {
        [TestCase("Hello, world!")]
        [TestCase("The quick brown fox jumps over the lazy dog.")]
        public void Serialize_MultilineString_ShouldUseTripleQuotes(string value)
        {
            var serializedStrings = new SerializableStrings(value);
            
            var toml = TomlSerializer.Serialize(serializedStrings);

            var expectedToml = $"multilineBasicString = \"\"\"\n{value}\"\"\"";
            Assert.That(expectedToml, Is.SubsetOf(toml));
        }
        
        [TestCase("C:\\Windows\\System32\\", "C:\\\\Windows\\\\System32\\\\")]
        [TestCase("\\Network\\Share\\Folder", "\\\\Network\\\\Share\\\\Folder")]
        public void Serialize_MultilineString_ShouldEscapeBackslashes(string value, string expectedValue)
        {
            var serializedStrings = new SerializableStrings(value);
            
            var toml = TomlSerializer.Serialize(serializedStrings);

            var expectedToml = $"multilineBasicString = \"\"\"\n{expectedValue}\"\"\"";
            Assert.That(expectedToml, Is.SubsetOf(toml));
        }
        
        [TestCase("The\n\"First\"\nThing")]
        public void Serialize_MultilineString_ShouldNotEscapeQuotes(string value)
        {
            var serializedStrings = new SerializableStrings(value);
            
            var toml = TomlSerializer.Serialize(serializedStrings);
            var tomlLines = toml.Split("\n");

            var expectedToml = $"multilineBasicString = \"\"\"\n{value}\"\"\"";
            Assert.That(expectedToml, Is.SubsetOf(toml));
        }
        
        [TestCase("Hello, world!")]
        [TestCase("The quick brown fox jumps over the lazy dog.")]
        public void Serialize_MultilineLiteralString_ShouldUseTripleQuotes(string value)
        {
            var serializedStrings = new SerializableStrings(value);
            
            var toml = TomlSerializer.Serialize(serializedStrings);

            var expectedToml = $"multilineLiteralString = '''\n{value}'''";
            Assert.That(expectedToml, Is.SubsetOf(toml));
        }
        
        [TestCase(@"C:\\Windows\\System32\\")]
        [TestCase(@"\\Network\\Share\\Folder")]
        public void Serialize_MultilineLiteralString_ShouldNotEscapeChars(string value)
        {
            var serializedStrings = new SerializableStrings(value);
            
            var toml = TomlSerializer.Serialize(serializedStrings);

            var expectedToml = $"multilineLiteralString = '''\n{value}'''";
            Assert.That(expectedToml, Is.SubsetOf(toml));
        }
        
        [TestCase("The\n\"First\"\nThing")]
        public void Serialize_MultilineLiteralString_ShouldNotEscapeQuotes(string value)
        {
            var serializedStrings = new SerializableStrings(value);
            
            var toml = TomlSerializer.Serialize(serializedStrings);

            var expectedToml = $"multilineLiteralString = '''\n{value}'''";
            Assert.That(expectedToml, Is.SubsetOf(toml));
        }

        [Test]
        public void Serialize_MultilineArray_ShouldPlaceItemsOnNewLine()
        {
            var serializedArray = SerializableMultilineArray<int>.WithValues(1, 2, 3, 4, 5);

            var toml = TomlSerializer.Serialize(serializedArray);

            var expectedToml = new StringBuilder()
                .AppendLine("array = [")
                .AppendLine("    1,")
                .AppendLine("    2,")
                .AppendLine("    3,")
                .AppendLine("    4,")
                .AppendLine("    5,")
                .AppendLine("]")
                .ToString();
            
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
