using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NestedClass_ShouldSerializeDotted()
        {
            var monster = new Monster { Name = "Boss" };
            var lootTables = new LootTable[]
            {
                new()
                {
                    TableName = "boss-common-easy",
                    Chance = 22.5f,
                    Rolls = 3
                },
                new()
                {
                    TableName = "boss-uncommon-easy",
                    Chance = 8.5f,
                    Rolls = 2
                },
                new()
                {
                    TableName = "boss-rare-easy",
                    Chance = 2.5f,
                    Rolls = 1,
                    DropForAllPlayers = true
                }
            };

            var lootTableSections = new[] { "common", "uncommon", "rare" };
            for (var i = 0; i < lootTableSections.Length; i++)
                monster.AddLoot(lootTableSections[i], lootTables[i]);

            var toml = TomlSerializer.Serialize(monster);

            var sections = toml.Split("\n\n");
            Assert.GreaterOrEqual(sections.Length, lootTableSections.Length + 1, "Missing one or more loot sections");

            for (var i = 0; i < lootTableSections.Length; i++)
            {
                var actualTable = sections[1 + i];
                var expectedTable = string.Join("\n", new[]
                {
                    $"[loot.{lootTableSections[i]}]",
                    $"tableName = \"{lootTables[i].TableName}\"",
                    $"chance = {(double)lootTables[i].Chance}",
                    $"rolls = {lootTables[i].Rolls}",
                    $"dropForAllPlayers = {lootTables[i].DropForAllPlayers.ToString().ToLowerInvariant()}"
                });
                
                Assert.AreEqual(expectedTable, actualTable);
            }
        }
    }
}
