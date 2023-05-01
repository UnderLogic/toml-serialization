using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("Alice", 2, 50, 100, 25, 50, 2, 100)]
        [TestCase("Bob", 11, 100, 200, 50, 100, 4, 200)]
        [TestCase("Charlie", 22, 200, 400, 75, 150, 6, 300)]
        public void Deserialize_ComplexType_ShouldSetFields(string name, int level, int health, int maxHealth, int mana,
            int maxMana, int statPoints, int gold)
        {
            var toml = new StringBuilder()
                .AppendLine($"name = \"{name}\"")
                .AppendLine($"level = {level}")
                .AppendLine($"health = {health}")
                .AppendLine($"maxHealth = {maxHealth}")
                .AppendLine($"mana = {mana}")
                .AppendLine($"maxMana = {maxMana}")
                .AppendLine($"statPoints = {statPoints}")
                .AppendLine($"gold = {gold}")
                .ToString();

            var deserializedPlayer = TomlSerializer.Deserialize<PlayerCharacter>(toml);
            Assert.That(deserializedPlayer.Name, Is.EqualTo(name));
            Assert.That(deserializedPlayer.Level, Is.EqualTo(level));
            Assert.That(deserializedPlayer.Health, Is.EqualTo(health));
            Assert.That(deserializedPlayer.MaxHealth, Is.EqualTo(maxHealth));
            Assert.That(deserializedPlayer.Mana, Is.EqualTo(mana));
            Assert.That(deserializedPlayer.MaxMana, Is.EqualTo(maxMana));
            Assert.That(deserializedPlayer.StatPoints, Is.EqualTo(statPoints));
            Assert.That(deserializedPlayer.Gold, Is.EqualTo(gold));
        }
        
        [TestCase(1,2,3,4,5)]
        [TestCase(2,4,6,8,10)]
        [TestCase(3,6,9,12,15)]
        public void Deserialize_ComplexType_ShouldSetNestedFields(int strength, int intelligence, int wisdom, int constitution, int dexterity)
        {
            var toml = new StringBuilder()
                .AppendLine("[stats]")
                .AppendLine($"strength = {strength}")
                .AppendLine($"intelligence = {intelligence}")
                .AppendLine($"wisdom = {wisdom}")
                .AppendLine($"constitution = {constitution}")
                .AppendLine($"dexterity = {dexterity}")
                .ToString();

            var deserializedPlayer = TomlSerializer.Deserialize<PlayerCharacter>(toml);
            Assert.That(deserializedPlayer.Stats.Strength, Is.EqualTo(strength));
            Assert.That(deserializedPlayer.Stats.Intelligence, Is.EqualTo(intelligence));
            Assert.That(deserializedPlayer.Stats.Wisdom, Is.EqualTo(wisdom));
            Assert.That(deserializedPlayer.Stats.Constitution, Is.EqualTo(constitution));
            Assert.That(deserializedPlayer.Stats.Dexterity, Is.EqualTo(dexterity));
        }

        [Test]
        public void Deserialize_ComplexType_ShouldAddToArray()
        {
            var inventoryItems = new[]
            {
                new InventoryItem
                {
                    Id = 1,
                    Name = "item_apple",
                    DisplayName = "Apple",
                    Quantity = 3,
                    MaxQuantity = 10,
                    CanDrop = true
                },
                new InventoryItem
                {
                    Id = 2,
                    Name = "quest_item_branch",
                    DisplayName = "Old Branch",
                    Quantity = 3,
                    MaxQuantity = 5,
                    CanDrop = false
                },
            };

            var sb = new StringBuilder();
            foreach (var item in inventoryItems)
            {
                sb.AppendLine("[[inventory]]");
                sb.AppendLine($"id = {item.Id}");
                sb.AppendLine($"name = \"{item.Name}\"");
                sb.AppendLine($"displayName = \"{item.DisplayName}\"");
                sb.AppendLine($"quantity = {item.Quantity}");
                sb.AppendLine($"maxQuantity = {item.MaxQuantity}");
                sb.AppendLine($"canDrop = {item.CanDrop.ToString().ToLowerInvariant()}");
                sb.AppendLine();
            }

            var toml = sb.ToString();

            var deserializedPlayer = TomlSerializer.Deserialize<PlayerCharacter>(toml);
            Assert.That(deserializedPlayer.Inventory.Count, Is.EqualTo(inventoryItems.Length));

            for (var i = 0; i < deserializedPlayer.Inventory.Count; i++)
                Assert.IsTrue(deserializedPlayer.Inventory[i].IsEquivalentTo(inventoryItems[i]));
        }
        
        [Test]
        public void Deserialize_ComplexType_ShouldAddToDictionary()
        {
            var equipmentItems = new Dictionary<string, EquipmentItem>
            {
                {
                    "weapon", new EquipmentItem
                    {
                        Name = "Sword",
                        AttackPower = 15,
                        Durability = 40,
                        MaxDurability = 80
                    }
                },
                {
                    "shield", new EquipmentItem
                    {
                        Name = "Magic Shield",
                        ArmorClass = 10,
                        SpellPower = 5,
                        MagicResist = 2,
                        Durability = 80,
                        MaxDurability = 90
                    }
                }
            };

            var sb = new StringBuilder();
            foreach (var (slot, item) in equipmentItems)
            {
                sb.AppendLine($"[equipment.{slot}]");
                sb.AppendLine($"name = \"{item.Name}\"");
                sb.AppendLine($"attackPower = {item.AttackPower}");
                sb.AppendLine($"armorClass = {item.ArmorClass}");
                sb.AppendLine($"spellPower = {item.SpellPower}");
                sb.AppendLine($"magicResist = {item.MagicResist}");
                sb.AppendLine($"durability = {item.Durability}");
                sb.AppendLine($"maxDurability = {item.MaxDurability}");
                sb.AppendLine();
            }

            var toml = sb.ToString();

            var deserializedPlayer = TomlSerializer.Deserialize<PlayerCharacter>(toml);
            Assert.That(deserializedPlayer.Equipment.Count, Is.EqualTo(equipmentItems.Count));
        }
    }
}
