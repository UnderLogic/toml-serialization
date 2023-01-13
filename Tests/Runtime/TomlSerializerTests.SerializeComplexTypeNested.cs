using System.Linq;
using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ComplexTypeNested_ShouldSerializeFieldsFirst()
        {
            var dungeon = CreateMockDungeon();

            var toml = TomlSerializer.Serialize(dungeon);
            var fieldSection = toml.Split("\n\n").FirstOrDefault();

            var expectedFields = new StringBuilder()
                .AppendLine($"name = \"{dungeon.Name}\"")
                .AppendLine($"description = \"{dungeon.Description}\"")
                .AppendLine($"minLevel = {dungeon.MinLevel}")
                .AppendLine($"maxLevel = {dungeon.MaxLevel}")
                .ToString();

            Assert.That(fieldSection, Is.Not.Null);
            Assert.That($"{fieldSection.Trim()}\n", Is.EqualTo(expectedFields));
        }

        [Test]
        public void Serialize_ComplexTypeNested_ShouldSerializeTableArray()
        {
            var dungeon = CreateMockDungeon();
            var firstRoom = dungeon.Rooms[0];

            var toml = TomlSerializer.Serialize(dungeon);
            var firstRoomSection = toml.Split("\n\n").Skip(1).FirstOrDefault();

            var expectedRoomTableArray = new StringBuilder()
                .AppendLine("[[rooms]]")
                .AppendLine($"id = {firstRoom.Id}")
                .AppendLine($"name = \"{firstRoom.Name}\"")
                .AppendLine($"description = \"{firstRoom.Description}\"")
                .AppendLine($"width = {firstRoom.Width}")
                .AppendLine($"height = {firstRoom.Height}")
                .ToString();

            Assert.That(firstRoomSection, Is.Not.Null);
            Assert.That($"{firstRoomSection.Trim()}\n", Is.EqualTo(expectedRoomTableArray));
        }
        
        [Test]
        public void Serialize_ComplexTypeNested_ShouldNestTableArray()
        {
            var dungeon = CreateMockDungeon();
            var firstRoom = dungeon.Rooms[0];
            var firstTrap = firstRoom.Traps[0];

            var toml = TomlSerializer.Serialize(dungeon);
            var firstTrapTable = toml.Split("\n\n").Skip(2).FirstOrDefault();

            var expectedTrapTable = new StringBuilder()
                .AppendLine("[[rooms.traps]]")
                .AppendLine($"name = \"{firstTrap.Name}\"")
                .AppendLine($"type = \"{firstTrap.Type}\"")
                .AppendLine($"damage = {firstTrap.Damage}")
                .AppendLine($"x = {firstTrap.X}")
                .AppendLine($"y = {firstTrap.Y}")
                .AppendLine($"triggerCount = {firstTrap.TriggerCount}")
                .ToString();

            Assert.That(firstTrapTable, Is.Not.Null);
            Assert.That($"{firstTrapTable.Trim()}\n", Is.EqualTo(expectedTrapTable));
        }
        
        [Test]
        public void Serialize_ComplexTypeNested_ShouldNestTableArray2()
        {
            var dungeon = CreateMockDungeon();
            var firstRoom = dungeon.Rooms[0];
            var firstMonster = firstRoom.Monsters[0];

            var toml = TomlSerializer.Serialize(dungeon);
            var firstMonsterTable = toml.Split("\n\n").Skip(3).FirstOrDefault();

            var expectedMonsterTable = new StringBuilder()
                .AppendLine("[[rooms.monsters]]")
                .AppendLine($"name = \"{firstMonster.Name}\"")
                .AppendLine($"health = {firstMonster.Health}")
                .AppendLine($"attack = {firstMonster.Attack}")
                .AppendLine($"defense = {firstMonster.Defense}")
                .AppendLine($"movement = {firstMonster.Movement}")
                .ToString();

            Assert.That(firstMonsterTable, Is.Not.Null);
            Assert.That($"{firstMonsterTable.Trim()}\n", Is.EqualTo(expectedMonsterTable));
        }

        [Test]
        public void Serialize_ComplexTypeNested_ShouldNestTableWithinTableArray()
        {
            var dungeon = CreateMockDungeon();
            var firstRoom = dungeon.Rooms[0];
            var firstMonster = firstRoom.Monsters[0];
            var (key, lootTable) = firstMonster.Loot.FirstOrDefault();

            var toml = TomlSerializer.Serialize(dungeon);
            var firstLootTable = toml.Split("\n\n").Skip(5).FirstOrDefault();

            var expectedLootTable = new StringBuilder()
                .AppendLine($"[rooms.monsters.loot.{key}]")
                .AppendLine($"lootTable = \"{lootTable.LootTable}\"")
                .AppendLine($"dropChance = {lootTable.DropChance}")
                .AppendLine($"rolls = {lootTable.Rolls}")
                .AppendLine($"dropsForAllPlayers = {lootTable.DropsForAllPlayers.ToString().ToLowerInvariant()}")
                .ToString();

            Assert.That(firstLootTable, Is.Not.Null);
            Assert.That($"{firstLootTable.Trim()}\n", Is.EqualTo(expectedLootTable));
        }

        private static Dungeon CreateMockDungeon()
        {
            var dungeon = new Dungeon
            {
                Name = "Test Dungeon",
                Description = "A test dungeon",
                MinLevel = 5,
                MaxLevel = 10
            };

            var ratMonster = new DungeonMonster
            {
                Name = "Rat",
                Health = 2,
                Attack = 1,
                Defense = 1,
                Movement = 3,
                Loot =
                {
                    {
                        "common", new DungeonLoot
                        {
                            LootTable = "loot-rat-common",
                            DropChance = 50,
                            Rolls = 2
                        }
                    }
                }
            };

            dungeon.Rooms.Add(new DungeonRoom
            {
                Id = 1,
                Name = "Entrance",
                Description = "The entrance to the dungeon.",
                Width = 6,
                Height = 6,
                Monsters = { ratMonster },
                Traps = { new DungeonTrap { Name = "Spike Trap", Damage = 1, X = 2, Y = 4 } }
            });

            return dungeon;
        }
    }
}
