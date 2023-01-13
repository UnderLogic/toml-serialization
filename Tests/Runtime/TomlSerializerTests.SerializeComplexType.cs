using System.Linq;
using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ComplexType_ShouldSerializeFieldsFirst()
        {
            var player = new PlayerCharacter
            {
                Level = 3,
                Health = 50,
                MaxHealth = 100,
                Mana = 25,
                MaxMana = 50,
                StatPoints = 4,
                Gold = 100,
            };
            var toml = TomlSerializer.Serialize(player);
            var fieldSection = toml.Split("\n\n").FirstOrDefault();

            var expectedFields = new StringBuilder()
                .AppendLine($"name = \"{player.Name}\"")
                .AppendLine($"level = {player.Level}")
                .AppendLine($"health = {player.Health}")
                .AppendLine($"maxHealth = {player.MaxHealth}")
                .AppendLine($"mana = {player.Mana}")
                .AppendLine($"maxMana = {player.MaxMana}")
                .AppendLine($"statPoints = {player.StatPoints}")
                .AppendLine($"inventory = []")
                .AppendLine($"gold = {player.Gold}")
                .ToString();
            
            Assert.That(fieldSection, Is.Not.Null);
            Assert.That($"{fieldSection.Trim()}\n", Is.EqualTo(expectedFields));
        }
        
        [Test]
        public void Serialize_ComplexType_ShouldSerializeTablesAfterFields()
        {
            var player = new PlayerCharacter
            {
                Stats =
                {
                    Strength = 1,
                    Intelligence = 2,
                    Wisdom = 3,
                    Constitution = 4,
                    Dexterity = 5
                }
            };

            var toml = TomlSerializer.Serialize(player);
            var statsTable = toml.Split("\n\n").Skip(1).FirstOrDefault();

            var expectedStatsTable = new StringBuilder()
                .AppendLine("[stats]")
                .AppendLine($"strength = {player.Stats.Strength}")
                .AppendLine($"intelligence = {player.Stats.Intelligence}")
                .AppendLine($"wisdom = {player.Stats.Wisdom}")  
                .AppendLine($"constitution = {player.Stats.Constitution}")
                .AppendLine($"dexterity = {player.Stats.Dexterity}")
                .ToString();
            
            Assert.That(statsTable, Is.Not.Null);
            Assert.That($"{statsTable.Trim()}\n", Is.EqualTo(expectedStatsTable));
        }
        
        [Test]
        public void Serialize_ComplexType_ShouldSerializeNestedTables()
        {
            var player = new PlayerCharacter();
            player.Equipment.Add("weapon", new EquipmentItem
            {
                Name = "Sword",
                AttackPower = 15,
                Durability = 50,
                MaxDurability = 100
            });
            player.Equipment.Add("shield", new EquipmentItem
            {
                Name = "Magic Shield",
                ArmorClass = 10,
                SpellPower = 5,
                MagicResist = 2,
                Durability = 90,
                MaxDurability = 100
            });

            var toml = TomlSerializer.Serialize(player);
            var equipmentTables = toml.Split("\n\n").Skip(2).ToList();

            foreach (var kv in player.Equipment)
            {
                var tableName = $"[equipment.{kv.Key}]";
                var equipmentTable = equipmentTables.FirstOrDefault(t => t.StartsWith(tableName));

                var expectedTable = new StringBuilder()
                    .AppendLine(tableName)
                    .AppendLine($"name = \"{kv.Value.Name}\"")
                    .AppendLine($"attackPower = {kv.Value.AttackPower}")
                    .AppendLine($"armorClass = {kv.Value.ArmorClass}")
                    .AppendLine($"spellPower = {kv.Value.SpellPower}")
                    .AppendLine($"magicResist = {kv.Value.MagicResist}")
                    .AppendLine($"durability = {kv.Value.Durability}")
                    .AppendLine($"maxDurability = {kv.Value.MaxDurability}")
                    .ToString();
                
                Assert.That(equipmentTable, Is.Not.Null);
                Assert.That($"{equipmentTable.Trim()}\n", Is.EqualTo(expectedTable));
            }
        }

        [Test]
        public void Serialize_ComplexType_ShouldSerializeTableArraysAfterTables()
        {
            var player = new PlayerCharacter();
            player.Inventory.Add(new InventoryItem
            {
                Id = 1,
                Name = "item_apple",
                DisplayName = "Apple",
                Quantity = 3,
                MaxQuantity = 10,
                CanDrop = true
            });
            player.Inventory.Add(new InventoryItem
            {
                Id = 2,
                Name = "quest_item_branch",
                DisplayName = "Old Branch",
                Quantity = 3,
                MaxQuantity = 5,
                CanDrop = false
            });

            var toml = TomlSerializer.Serialize(player);
            var inventoryTables = toml.Split("\n\n").Skip(2).ToList();

            Assert.That(inventoryTables.Count, Is.EqualTo(player.Inventory.Count));
            
            for (var i = 0; i < player.Inventory.Count; i++)
            {
                var item = player.Inventory[i];
                var inventoryTable = inventoryTables[i];
                
                var expectedInventoryTable = new StringBuilder()
                    .AppendLine($"[[inventory]]")
                    .AppendLine($"id = {item.Id}")
                    .AppendLine($"name = \"{item.Name}\"")
                    .AppendLine($"displayName = \"{item.DisplayName}\"")
                    .AppendLine($"quantity = {item.Quantity}")
                    .AppendLine($"maxQuantity = {item.MaxQuantity}")
                    .AppendLine($"canDrop = {item.CanDrop.ToString().ToLowerInvariant()}")
                    .ToString();
                
                Assert.That(inventoryTable, Is.Not.Null);
                Assert.That($"{inventoryTable.Trim()}\n", Is.EqualTo(expectedInventoryTable));
            }
        }
    }
}
