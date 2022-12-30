using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_TomlKeyAttribute_ShouldRenameKey()
        {
            var quest = new Quest
            {
                Name = "quest_gather_wood",
                Title = "Gather Wood",
                Description = "Fetch me some wood around town.",
                GoldReward = 50,
                IsRepeatable = true
            };

            quest.Objectives.Add("Gather 10 Wood");

            var toml = TomlSerializer.Serialize(quest);

            var expected = string.Join("\n", new[]
            {
                $"quest_name = \"{quest.Name}\"",
                $"quest_title = \"{quest.Title}\"",
                $"quest_description = \"{quest.Description}\"",
                $"quest_objectives = [ {string.Join(", ", quest.Objectives.Select(obj => $"\"{obj}\""))} ]",
                $"reward = {quest.GoldReward}",
                $"repeatable = {quest.IsRepeatable.ToString().ToLowerInvariant()}",
            });

            Assert.AreEqual($"{expected}\n", toml);
        }
    }
}
