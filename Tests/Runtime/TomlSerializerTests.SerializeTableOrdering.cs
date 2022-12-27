using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ComplexObject_ShouldSerializeKeyValuesFirst()
        {
            var player = new Player
            {
                CreatedAt = new DateTime(1999, 8, 2),
                LastLoginAt = new DateTime(2019, 8, 2)
            };
            
            player.AddInventoryItem(new InventoryItem { Slot = 1 });
            player.AddInventoryItem(new InventoryItem { Slot = 2 });

            var toml = TomlSerializer.Serialize(player);
            var sections = toml.Split("\n\n");

            var expectedFields = string.Join("\n", new[]
            {
                $"name = \"{player.Name}\"",
                $"title = \"{player.Title}\"",
                $"class = \"{player.Class}\"",
                $"level = {player.Level}",
                $"experience = {player.Experience}",
                $"gold = {player.Gold}",
                $"facing = \"{player.Facing}\"",
                $"statPoints = {player.StatPoints}",
                $"status = \"{player.Status:F}\"",
                $"isBanned = {player.IsBanned.ToString().ToLowerInvariant()}",
                $"createdAt = {player.CreatedAt:yyyy-MM-dd HH:mm:ss.fffZ}",
                $"lastLoginAt = {player.LastLoginAt:yyyy-MM-dd HH:mm:ss.fffZ}"
            });

            var fieldsSection = sections[0];
            Assert.AreEqual(expectedFields, fieldsSection);
        }
        
        [Test]
        public void Serialize_ComplexObject_ShouldSerializeTablesSecond()
        {
            var player = new Player();
            player.AddInventoryItem(new InventoryItem { Slot = 1 });
            player.AddInventoryItem(new InventoryItem { Slot = 2 });

            var toml = TomlSerializer.Serialize(player);
            var sections = toml.Split("\n\n");

            var expectedStats = string.Join("\n", new[]
            {
                "[stats]",
                $"health = {player.Stats.Health}",
                $"maxHealth = {player.Stats.MaxHealth}",
                $"mana = {player.Stats.Mana}",
                $"maxMana = {player.Stats.MaxMana}",
                $"strength = {player.Stats.Strength}",
                $"intelligence = {player.Stats.Intelligence}",
                $"wisdom = {player.Stats.Wisdom}",
                $"constitution = {player.Stats.Constitution}",
                $"dexterity = {player.Stats.Dexterity}"
            });

            var statsSection = sections[2];
            Assert.AreEqual(expectedStats, statsSection);
        }
        
        [Test]
        public void Serialize_ComplexObject_ShouldSerializeTableArraysLast()
        {
            var player = new Player();
            player.AddInventoryItem(new InventoryItem { Slot = 1 });
            player.AddInventoryItem(new InventoryItem { Slot = 2 });
            player.AddInventoryItem(new InventoryItem { Slot = 3 });

            var toml = TomlSerializer.Serialize(player);
            var sections = toml.Split("\n\n");

            for (var i = 0; i < player.Inventory.Count; i++)
            {
                var expectedItem = string.Join("\n", new[]
                {
                    "[[inventory]]",
                    $"slot = {player.Inventory[i].Slot}",
                    $"key = \"{player.Inventory[i].Key}\"",
                    $"displayName = \"{player.Inventory[i].DisplayName}\"",
                    $"weight = {(double)player.Inventory[i].Weight}",
                    $"amount = {player.Inventory[i].Amount}",
                    $"maxAmount = {player.Inventory[i].MaxAmount}",
                    $"canUse = {player.Inventory[i].CanUse.ToString().ToLowerInvariant()}",
                    $"canDrop = {player.Inventory[i].CanDrop.ToString().ToLowerInvariant()}",
                    $"acquiredAt = {player.Inventory[i].AcquiredAt:yyyy-MM-dd HH:mm:ss.fffZ}"
                });

                var itemSection = sections[3 + i];
                Assert.AreEqual(expectedItem, itemSection.Trim());
            }
        }
    }
}
