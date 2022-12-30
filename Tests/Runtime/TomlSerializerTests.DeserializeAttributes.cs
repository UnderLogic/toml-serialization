using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_TomlKeyAttribute_ShouldMapKey()
        {
            var toml = string.Join("\n", new[]
            {
                "quest_name = \"quest_starter_gather_wood\"",
                "quest_title = \"Gather Wood\"",
                "quest_description = \"Fetch me some wood around here.\"",
                "quest_objectives = [ \"Gather 10 Wood\" ]",
                "reward = 100",
                "repeatable = true"
            });

            var deserializedQuest = new Quest();
            TomlSerializer.DeserializeInto(toml, deserializedQuest);

            Assert.AreEqual("quest_starter_gather_wood", deserializedQuest.Name);
            Assert.AreEqual("Gather Wood", deserializedQuest.Title);
            Assert.AreEqual("Fetch me some wood around here.", deserializedQuest.Description);
            Assert.AreEqual(1, deserializedQuest.Objectives.Count);
            Assert.AreEqual("Gather 10 Wood", deserializedQuest.Objectives[0]);
            Assert.AreEqual(100, deserializedQuest.GoldReward);
            Assert.AreEqual(true, deserializedQuest.IsRepeatable);
        }
    }
}
