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
                Title = "Gather Wood"
            };
            
            var toml = TomlSerializer.Serialize(quest);
            
            var expected = string.Join("\n", new[]
            {
                "[quest]",
                "quest_name = \"quest_gather_wood\"",
                "quest_title = \"Gather Wood\"",
            });
            
            Assert.AreEqual($"{expected}\n", toml);
        }
    }
}
