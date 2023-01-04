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

        [Test]
        public void Serialize_TomlInlineAttribute_ShouldWriteInlineTable()
        {
            var guard = new Guardian();
            guard.AddWaypoint("entrance", new PlayerLocation(500, 24, 42));
            guard.AddWaypoint("tower", new PlayerLocation(500, 42, 24));

            var toml = TomlSerializer.Serialize(guard);
            var lines = toml.Split("\n");

            var expectedLine =
                $"waypoints = {{ entrance = {{ map = 500, x = 24, y = 42, zIndex = 1 }}, tower = {{ map = 500, x = 42, y = 24, zIndex = 1 }} }}";
            Assert.Contains(expectedLine, lines, "Should contain inline table");
        }

        [Test]
        public void Serialize_TomlExpandAttribute_ShouldWriteInlineTable()
        {
            var guard = new Guardian();
            guard.AddDialogueChoice("hail", "Hello, friend.");
            guard.AddDialogueChoice("farewell", "Goodbye, friend.");

            var toml = TomlSerializer.Serialize(guard);
            var lines = toml.Split("\n");

            Assert.Contains("[dialogueChoices]", lines, "Should contain expanded table name");
            Assert.Contains("hail = \"Hello, friend.\"", lines);
            Assert.Contains("farewell = \"Goodbye, friend.\"", lines);
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
