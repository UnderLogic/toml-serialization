using System;
using System.Linq;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ClassType_ShouldSerializeNamedTable()
        {
            var classObject = new MockSimpleClass
            {
                Id = 99,
                Name = "Hidden Item",
                Weight = 0.5f,
                Hidden = true,
                CreatedAt = new DateTime(2022, 10, 1)
            };
            
            var wrappedData = new WrappedValue<MockSimpleClass>(classObject);
            var toml = TomlSerializer.Serialize(wrappedData);

            var keyValuePairLines = new[]
            {
                $"id = {classObject.Id}",
                $"name = \"{classObject.Name}\"",
                $"weight = {classObject.Weight}",
                $"hidden = {classObject.Hidden.ToString().ToLowerInvariant()}",
                $"createdAt = {classObject.CreatedAt:yyyy-MM-dd HH:mm:ss.fffZ}"
            };
            var tableString = string.Join("\n", keyValuePairLines);

            Assert.AreEqual($"[value]\n{tableString}\n", toml);
        }

        [Test]
        public void Serialize_StructType_ShouldSerializeNamedTable()
        {
            var structObject = new MockSimpleStruct
            {
                Index = 42,
                X = 0.125f,
                Y = 1.421f,
                Z = 3.141f
            };
            
            var wrappedData = new WrappedValue<MockSimpleStruct>(structObject);
            var toml = TomlSerializer.Serialize(wrappedData);

            var keyValuePairLines = new[]
            {
                $"index = {structObject.Index}",
                $"x = {(double)structObject.X}",
                $"y = {(double)structObject.Y}",
                $"z = {(double)structObject.Z}"
            };
            var tableString = string.Join("\n", keyValuePairLines);

            Assert.AreEqual($"[value]\n{tableString}\n", toml);
        }

        [Test]
        public void Serialize_NestedClass_ShouldSerializeTableArray()
        {
            var nestedClassObject = new MockNestedClass
            {
                Id = 42,
                Name = "Test Player",
                Level = 99,
                Health = 1000,
                MaxHealth = 2000,
                Mana = 500,
                MaxMana = 750,
                Gold = 1234567890,
                Experience = 1000000,
            };

            var nestedApple = new MockNestedItem
            {
                Id = 1,
                Key = "item_apple",
                DisplayName = "Apple",
                Weight = 0.33f,
                Identified = true,
                Quantity = 3,
                MaxQuantity = 10
            };
            
            var nestedSword = new MockNestedItem
            {
                Id = 2,
                Key = "item_sword",
                DisplayName = "Sword (Unidentified)",
                Weight = 1.5f,
                Identified = false,
                Quantity = 1,
                MaxQuantity = 1
            };
            
            nestedApple.AddModifier("health", 10);
            nestedSword.AddModifier("strength", 1);
            
            nestedClassObject.AddItem(nestedApple);
            nestedClassObject.AddItem(nestedSword);
            
            var toml = TomlSerializer.Serialize(nestedClassObject);
            
            var keyValuePairLines = new[]
            {
                $"id = {nestedClassObject.Id}",
                $"name = \"{nestedClassObject.Name}\"",
                $"level = {nestedClassObject.Level}",
                $"health = {nestedClassObject.Health}",
                $"maxHealth = {nestedClassObject.MaxHealth}",
                $"mana = {nestedClassObject.Mana}",
                $"maxMana = {nestedClassObject.MaxMana}",
                $"gold = {nestedClassObject.Gold}",
                $"experience = {nestedClassObject.Experience}",
            };
            var tableString = string.Join("\n", keyValuePairLines);

            var nestedItemLines = nestedClassObject.Inventory.Select(item => string.Join("\n", new[]
            {
                "[[inventory]]",
                $"id = {item.Id}",
                $"key = \"{item.Key}\"",
                $"displayName = \"{item.DisplayName}\"",
                $"identified = {item.Identified.ToString().ToLowerInvariant()}",
                $"weight = {(double)item.Weight}",
                $"quantity = {item.Quantity}",
                $"maxQuantity = {item.MaxQuantity}",
                $"modifiers = {{ {string.Join(", ", item.Modifiers.Select(modifier => $"{modifier.Key} = {modifier.Value}"))} }}",
            }));
            var nestedItemsString = string.Join("\n\n", nestedItemLines);
            
            Assert.AreEqual($"{tableString}\n\n{nestedItemsString}\n", toml);
        }

        [Test]
        public void Serialize_UnorderedClass_ShouldSerializeInOrder()
        {
            var monster = new MockUnorderedClass()
            {
                Id = 42,
                Name = "Test Monster",
                AggroRadius = 3.14159f,
                CurrentTarget = "Player",
                Gold = 25,
                Experience = 125,
                SpawnedAt = new DateTime(2020, 10, 1),
                DespawnAt = new DateTime(2021, 10, 1)
            };

            var monsterStats = new MockUnorderedStats
            {
                Health = 600,
                MaxHealth = 600,
                Mana = 50,
                MaxMana = 50,
                Strength = 2,
                Dexterity = 1,
                Intelligence = 1,
                Wisdom = 1,
                Constitution = 3,
                Charisma = 1,
                Luck = 25
            };

            var commonLoot = new MockUnorderedItem
            {
                Index = 1,
                LootTable = "Common",
                DropChance = 12.5f,
                CanPickpocket = true
            };

            var rareLoot = new MockUnorderedItem
            {
                Index = 2,
                LootTable = "Rare",
                DropChance = 1.25f,
                CanPickpocket = false
            };

            monster.Stats = monsterStats;
            monster.AddLoot(commonLoot);
            monster.AddLoot(rareLoot);

            monster.SetAggro("Player", 99);
            monster.SetAggro("Healer", 42);

            var toml = TomlSerializer.Serialize(monster);

            var aggroTableLines = string.Join(", ", monster.AggroTable.Select(aggro => $"{aggro.Key} = {aggro.Value}"));

            var keyValuePairLines = string.Join("\n", new[]
            {
                $"id = {monster.Id}",
                $"name = \"{monster.Name}\"",
                $"aggroRadius = {(double)monster.AggroRadius}",
                $"aggroTable = {{ {aggroTableLines} }}",
                $"currentTarget = \"{monster.CurrentTarget}\"",
                $"gold = {monster.Gold}",
                $"experience = {monster.Experience}",
                $"spawnedAt = {monster.SpawnedAt:yyyy-MM-dd HH:mm:ss.fffZ}",
                $"despawnAt = {monster.DespawnAt:yyyy-MM-dd HH:mm:ss.fffZ}",
            });

            var statLines = string.Join("\n", new[]
            {
                "[stats]",
                $"health = {monsterStats.Health}",
                $"maxHealth = {monsterStats.MaxHealth}",
                $"mana = {monsterStats.Mana}",
                $"maxMana = {monsterStats.MaxMana}",
                $"strength = {monsterStats.Strength}",
                $"dexterity = {monsterStats.Dexterity}",
                $"intelligence = {monsterStats.Intelligence}",
                $"wisdom = {monsterStats.Wisdom}",
                $"constitution = {monsterStats.Constitution}",
                $"charisma = {monsterStats.Charisma}",
                $"luck = {monsterStats.Luck}"
            });

            var lootLines = string.Join("\n\n", monster.Loot.Select(item => string.Join("\n", new[]
            {
                "[[loot]]",
                $"index = {item.Index}",
                $"lootTable = \"{item.LootTable}\"",
                $"dropChance = {(double)item.DropChance}",
                $"canPickpocket = {item.CanPickpocket.ToString().ToLowerInvariant()}"
            })));
            
            var tableString = $"{keyValuePairLines}\n\n{statLines}\n\n{lootLines}\n";
            Assert.AreEqual(tableString, toml);
        }
    }
}
