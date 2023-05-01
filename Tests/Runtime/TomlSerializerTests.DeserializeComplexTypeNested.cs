using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("Test Dungeon", "Test Dungeon", 5, 10)]
        public void Deserialize_ComplexTypeNested_ShouldSetFields(string name, string description, int minLevel,
            int maxLevel)
        {
            var toml = new StringBuilder()
                .AppendLine($"name = \"{name}\"")
                .AppendLine($"description = \"{description}\"")
                .AppendLine($"minLevel = {minLevel}")
                .AppendLine($"maxLevel = {maxLevel}")
                .ToString();

            var deserializedDungeon = TomlSerializer.Deserialize<Dungeon>(toml);
            Assert.That(deserializedDungeon.Name, Is.EqualTo(name));
            Assert.That(deserializedDungeon.Description, Is.EqualTo(description));
            Assert.That(deserializedDungeon.MinLevel, Is.EqualTo(minLevel));
            Assert.That(deserializedDungeon.MaxLevel, Is.EqualTo(maxLevel));
        }

        [TestCase(42, "First Room", "Test Room", 16, 12)]
        public void Deserialize_ComplexTypeNested_ShouldSetList(int id, string name, string description, int width,
            int height)
        {
            var toml = new StringBuilder()
                .AppendLine("[[rooms]]")
                .AppendLine($"id = {id}")
                .AppendLine($"name = \"{name}\"")
                .AppendLine($"description = \"{description}\"")
                .AppendLine($"width = {width}")
                .AppendLine($"height = {height}")
                .ToString();

            var deserializedDungeon = TomlSerializer.Deserialize<Dungeon>(toml);
            Assert.That(deserializedDungeon.Rooms.Count, Is.EqualTo(1));

            var firstRoom = deserializedDungeon.Rooms[0];
            Assert.That(firstRoom.Name, Is.EqualTo(name));
            Assert.That(firstRoom.Description, Is.EqualTo(description));
            Assert.That(firstRoom.Width, Is.EqualTo(width));
            Assert.That(firstRoom.Height, Is.EqualTo(height));
        }

        [Test]
        public void Deserialize_ComplexTypeNested_ShouldSetNestedList()
        {
            var expectedTraps = new[]
            {
                new DungeonTrap
                {
                    Name = "Spike Trap", Type = "Physical", Damage = 1, X = 2, Y = 4, TriggerCount = 1
                },
                new DungeonTrap
                {
                    Name = "Poison Gas", Type = "Poison", Damage = 3, X = 1, Y = 5, TriggerCount = 3
                },
            };

            var sb = new StringBuilder()
                .AppendLine("[[rooms]]")
                .AppendLine("id = 1");

            foreach (var trap in expectedTraps)
            {
                sb.AppendLine();
                sb.AppendLine("[[rooms.traps]]");
                sb.AppendLine($"name = \"{trap.Name}\"");
                sb.AppendLine($"type = \"{trap.Type}\"");
                sb.AppendLine($"damage = {trap.Damage}");
                sb.AppendLine($"x = {trap.X}");
                sb.AppendLine($"y = {trap.Y}");
                sb.AppendLine($"triggerCount = {trap.TriggerCount}");
            }
            
            var toml = sb.ToString();

            var deserializedDungeon = TomlSerializer.Deserialize<Dungeon>(toml);
            Assert.That(deserializedDungeon.Rooms.Count, Is.EqualTo(1));

            var firstRoom = deserializedDungeon.Rooms[0];
            Assert.AreEqual(expectedTraps.Length, firstRoom.Traps.Count);

            for (var i = 0; i < expectedTraps.Length; i++)
                Assert.IsTrue(expectedTraps[i].IsEquivalentTo(firstRoom.Traps[i]));
        }
        
        [Test]
        public void Deserialize_ComplexTypeNested_ShouldSetNestedDictionaries()
        {
            var expectedLoot = new Dictionary<string, DungeonLoot>
            {
                {"quest", new DungeonLoot
                {
                    LootTable = "loot-goblin-quest",
                    DropChance = 100,
                    Rolls = 1,
                    DropsForAllPlayers = true
                }},
                {"common", new DungeonLoot
                {
                    LootTable = "loot-goblin-common",
                    DropChance = 30,
                    Rolls = 3,
                }},
                {"uncommon", new DungeonLoot
                {
                    LootTable = "loot-goblin-uncommon",
                    DropChance = 10,
                    Rolls = 2,
                }},
                {"rare", new DungeonLoot
                {
                    LootTable = "loot-goblin-rare",
                    DropChance = 5,
                    Rolls = 1,
                }},
            };

            var sb = new StringBuilder()
                .AppendLine("[[rooms]]")
                .AppendLine("id = 1")
                .AppendLine()
                .AppendLine("[[rooms.monsters]]")
                .AppendLine("name = \"Goblin\"")
                .AppendLine("health = 5")
                .AppendLine("attack = 2")
                .AppendLine("defense = 1")
                .AppendLine("movement = 3");

            foreach (var (key, loot) in expectedLoot)
            {
                sb.AppendLine();
                sb.AppendLine($"[rooms.monsters.loot.{key}]");
                sb.AppendLine($"lootTable = \"{loot.LootTable}\"");
                sb.AppendLine($"dropChance = {loot.DropChance}");
                sb.AppendLine($"rolls = {loot.Rolls}");
                sb.AppendLine($"dropsForAllPlayers = {loot.DropsForAllPlayers.ToString().ToLowerInvariant()}");
            }
            
            var toml = sb.ToString();

            var deserializedDungeon = TomlSerializer.Deserialize<Dungeon>(toml);
            Assert.That(deserializedDungeon.Rooms.Count, Is.EqualTo(1));

            var firstRoom = deserializedDungeon.Rooms[0];
            Assert.That(firstRoom.Monsters.Count, Is.EqualTo(1));
            
            var firstMonster = firstRoom.Monsters[0];
            Assert.AreEqual(expectedLoot.Count, firstMonster.Loot.Count);

            foreach (var pair in expectedLoot)
            {
                var key = pair.Key;
                Assert.IsTrue(firstMonster.Loot.ContainsKey(key));

                var expectedLootItem = pair.Value;
                var actualLootItem = firstMonster.Loot[key];
                
                Assert.IsTrue(expectedLootItem.IsEquivalentTo(actualLootItem));
            }
        }
    }
}
