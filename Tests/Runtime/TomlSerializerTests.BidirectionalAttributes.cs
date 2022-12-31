using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_TomlKeyAttribute_ShouldSetEqual()
        {
            var serializedValue = new WrappedRenamedValue<string>("Hello, World!");
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedRenamedValue<string>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }

        [Test]
        public void SerializeDeserialize_TomlCasingAttribute_ShouldSetEqual()
        {
            var serializedValues = new WrappedCasedValues<int>(42);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedCasedValues<int>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.LowerValue, deserializedValues.LowerValue);
            Assert.AreEqual(serializedValues.UpperValue, deserializedValues.UpperValue);
            Assert.AreEqual(serializedValues.CamelValue, deserializedValues.CamelValue);
            Assert.AreEqual(serializedValues.PascalValue, deserializedValues.PascalValue);
            Assert.AreEqual(serializedValues.SnakeValue, deserializedValues.SnakeValue);
            Assert.AreEqual(serializedValues.KebabValue, deserializedValues.KebabValue);
        }

        [TestCase("This is a quest.\nYou should complete it.\n")]
        [TestCase("  \tThis is a quest.\n    You should complete it.\n\nFor some loot.\n  ")]
        [TestCase("Even a quest with a \"quote\" in it.\nThis is supported by TOML.\nAlso 'single quotes'.\n")]
        public void SerializeDeserialize_MultilineString_ShouldSetEqual(string expectedValue)
        {
            var serializedQuest = new Quest
            {
                Description = expectedValue
            };

            var tomlString = TomlSerializer.Serialize(serializedQuest);

            var deserializedQuest = new Quest();
            TomlSerializer.DeserializeInto(tomlString, deserializedQuest);

            Assert.AreEqual(serializedQuest.Description, deserializedQuest.Description);
        }

        [TestCase(@"C:\Scripts\Quests\GatherWood.lua")]
        [TestCase(@"C:\John's Scripts\Quests\GatherWood.lua")]
        [TestCase(@"\\Network\\Share\\Scripts\\Quests\\GatherWood.lua")]
        [TestCase(@"You can use ""quotes"" in paths.")]
        public void SerializeDeserialize_LiteralString_ShouldSetEqual(string expectedValue)
        {
            var serializedQuest = new Quest
            {
                ScriptPath = expectedValue
            };

            var tomlString = TomlSerializer.Serialize(serializedQuest);

            var deserializedQuest = new Quest();
            TomlSerializer.DeserializeInto(tomlString, deserializedQuest);

            Assert.AreEqual(serializedQuest.Summary, deserializedQuest.Summary);
        }

        [TestCase("This is a quest.\nYou should complete it.\n")]
        [TestCase("  \tThis is a quest.\n    You should complete it.\n\nFor some loot.\n  ")]
        [TestCase("Even a quest with a \"quote\" in it.\nThis is supported by TOML.\nAlso 'single quotes'.\n")]
        public void SerializeDeserialize_MultilineLiteralString_ShouldSetEqual(string expectedValue)
        {
            var serializedQuest = new Quest
            {
                Summary = expectedValue
            };

            var tomlString = TomlSerializer.Serialize(serializedQuest);

            var deserializedQuest = new Quest();
            TomlSerializer.DeserializeInto(tomlString, deserializedQuest);

            Assert.AreEqual(serializedQuest.Summary, deserializedQuest.Summary);
        }

        [Test]
        public void SerializeDeserialize_TomlInlineAttribute_ShouldSetEqual()
        {
            var serializedGuard = new Guardian
            {
                Name = "Town Guard",
                AggroRadius = 5.5f
            };
            serializedGuard.AddWaypoint("town_square", new PlayerLocation(500, 24, 42));
            serializedGuard.AddWaypoint("town_gate", new PlayerLocation(500, 42, 24));

            var tomlString = TomlSerializer.Serialize(serializedGuard);

            var deserializedGuard = new Guardian();
            TomlSerializer.DeserializeInto(tomlString, deserializedGuard);

            Assert.AreEqual(serializedGuard.Name, deserializedGuard.Name);
            Assert.AreEqual(serializedGuard.AggroRadius, deserializedGuard.AggroRadius);

            Assert.AreEqual(serializedGuard.Waypoints.Count, deserializedGuard.Waypoints.Count);
            Assert.AreEqual(serializedGuard.Waypoints["town_square"], deserializedGuard.Waypoints["town_square"]);
            Assert.AreEqual(serializedGuard.Waypoints["town_gate"], deserializedGuard.Waypoints["town_gate"]);
        }

        [Test]
        public void SerializeDeserialize_TomlExpandAttribute_ShouldSetEqual()
        {
            var serializedGuard = new Guardian
            {
                Name = "Town Guard",
                AggroRadius = 5.5f
            };
            serializedGuard.AddDialogueChoice("hail", "Hello, traveler.");
            serializedGuard.AddDialogueChoice("farewell", "Goodbye, traveler.");

            var tomlString = TomlSerializer.Serialize(serializedGuard);

            var deserializedGuard = new Guardian();
            TomlSerializer.DeserializeInto(tomlString, deserializedGuard);

            Assert.AreEqual(serializedGuard.Name, deserializedGuard.Name);
            Assert.AreEqual(serializedGuard.AggroRadius, deserializedGuard.AggroRadius);

            Assert.AreEqual(deserializedGuard.DialogueChoices.Count, deserializedGuard.DialogueChoices.Count);
            Assert.AreEqual(serializedGuard.DialogueChoices["hail"], deserializedGuard.DialogueChoices["hail"]);
            Assert.AreEqual(serializedGuard.DialogueChoices["farewell"], deserializedGuard.DialogueChoices["farewell"]);
        }
    }
}
