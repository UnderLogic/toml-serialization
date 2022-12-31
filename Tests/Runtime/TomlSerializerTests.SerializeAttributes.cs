using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_TomlKeyAttribute_ShouldRenameKey()
        {
            var wrappedValue = new WrappedRenamedValue<string>("The quick brown fox jumps over the lazy dog.");
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"renamedValue = \"{wrappedValue.Value}\"\n", toml);
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldLowerCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("lowervalue = 42", lines, "Should contain lower-cased key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldUpperCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("UPPERVALUE = 42", lines, "Should contain upper-cased key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldCamelCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("camelValue = 42", lines, "Should contain camel-cased key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldPascalCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("PascalValue = 42", lines, "Should contain pascal-cased key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldSnakeCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("snake_value = 42", lines, "Should contain snake-cased key key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldKebabCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("kebab-value = 42", lines, "Should contain kebab-cased key");
        }
        
        [Test]
        public void Serialize_TomlMultilineAttribute_ShouldUseTripleQuotes()
        {
            var quest = new Quest();
            var toml = TomlSerializer.Serialize(quest);
    
            var actualLine = GetMultilineStringKey(toml, "description");
            var expectedLine = $"description = \"\"\"\n{quest.Description}\"\"\"\n";
            Assert.AreEqual(expectedLine, actualLine, "Should contain multi-line string");
        }
        
        [Test]
        public void Serialize_TomlMultilineAttribute_ShouldPreserveWhitespace()
        {
            var quest = new Quest
            {
                Description = "\tThis is a quest.\n   It is indented.\n\n\nAnd has additional new lines.\n"
            };
            var toml = TomlSerializer.Serialize(quest);
    
            var actualLine = GetMultilineStringKey(toml, "description");
            var expectedLine = $"description = \"\"\"\n{quest.Description}\"\"\"\n";
            Assert.AreEqual(expectedLine, actualLine, "Should contain multi-line string with whitespace");
        }

        [Test]
        public void Serialize_TomlLiteralAttribute_ShouldUseSingleQuotes()
        {
            var quest = new Quest
            {
                ScriptPath = @"C:\Scripts\Quests\GatherWood.lua",
            };

            var toml = TomlSerializer.Serialize(quest);
            var lines = toml.Split("\n");

            var expectedLine = $"scriptPath = '{quest.ScriptPath}'";
            Assert.Contains(expectedLine, lines, "Should contain single-quoted literal string");
        }
        
        [Test]
        public void Serialize_TomlLiteralAttribute_ShouldEscapeSingleQuote()
        {
            var quest = new Quest
            {
                ScriptPath = @"C:\John's Scripts\Quests\GatherWood.lua",
            };

            var toml = TomlSerializer.Serialize(quest);
            var lines = toml.Split("\n");

            var expectedLine = $"scriptPath = '''{quest.ScriptPath}'''";
            Assert.Contains(expectedLine, lines, "Should escape literal string with triple-quotes");
        }

        [Test]
        public void Serialize_TomlLiteralMultilineAttribute_ShouldUseTripleQuotes()
        {
            var quest = new Quest();
            var toml = TomlSerializer.Serialize(quest);
    
            var actualLine = GetMultilineStringKey(toml, "summary", true);
            var expectedLine = $"summary = '''\n{quest.Summary}'''\n";
            Assert.AreEqual(expectedLine, actualLine, "Should contain multi-line literal string");
        }
        
        [Test]
        public void Serialize_TomlLiteralMultilineAttribute_ShouldPreserveWhitespace()
        {
            var quest = new Quest
            {
                Summary = "\tThis is a quest.\n   It is indented.\n\n\nAnd has additional new lines.\n"
            };
            var toml = TomlSerializer.Serialize(quest);
    
            var actualLine = GetMultilineStringKey(toml, "summary", true);
            var expectedLine = $"summary = '''\n{quest.Summary}'''\n";
            Assert.AreEqual(expectedLine, actualLine, "Should contain multi-line literal string");
        }

        private static string GetMultilineStringKey(string toml, string key, bool isLiteral = false)
        {
            var quotes = isLiteral ? "'''" : "\"\"\"";
            var tomlLines = toml.Split("\n");
            var lineBuffer = new StringBuilder();

            var found = false;
            foreach (var line in tomlLines)
            {
                if (line.StartsWith($"{key} = {quotes}"))
                {
                    found = true;
                    lineBuffer.AppendLine(line);
                    continue;
                }

                if (!found)
                    continue;

                lineBuffer.AppendLine(line);

                if (line.EndsWith(quotes))
                    return lineBuffer.ToString();
            }

            return null;
        }
    }
}
