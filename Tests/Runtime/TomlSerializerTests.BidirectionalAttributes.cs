using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;
using UnityEditor.VersionControl;

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
    }
}
