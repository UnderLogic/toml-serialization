using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_ObjectDict_ShouldKeepValues()
        {
            var serializedMonster = new Monster { Name = "Boss" };
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
                serializedMonster.AddLoot(lootTableSections[i], lootTables[i]);

            var tomlString = TomlSerializer.Serialize(serializedMonster);

            var deserializedMonster = new Monster();
            TomlSerializer.DeserializeInto(tomlString, deserializedMonster);

            Assert.AreEqual(serializedMonster.Name, deserializedMonster.Name);
            Assert.AreEqual(serializedMonster.Loot.Count, deserializedMonster.Loot.Count);

            foreach (var key in serializedMonster.Loot.Keys)
            {
                Assert.IsTrue(deserializedMonster.Loot.ContainsKey(key),
                    $"Monster is missing loot table section: {key}");

                var expectedLoot = serializedMonster.Loot[key];
                var actualLoot = deserializedMonster.Loot[key];

                Assert.AreEqual(expectedLoot.TableName, actualLoot.TableName);
                Assert.AreEqual(expectedLoot.Chance, actualLoot.Chance);
                Assert.AreEqual(expectedLoot.Rolls, actualLoot.Rolls);
                Assert.AreEqual(expectedLoot.DropForAllPlayers, actualLoot.DropForAllPlayers);
            }
        }
    }
}
