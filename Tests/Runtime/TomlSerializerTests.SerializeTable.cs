using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ClassType_ShouldSerializeNamedTable()
        {
            var inventoryItem = new InventoryItem
            {
                Slot = 2,
                Key = "item_red_potion",
                DisplayName = "Red Potion",
                Weight = 0.1f,
                Amount = 5,
                MaxAmount = 30,
                CanUse = true,
                CanDrop = true,
                AcquiredAt = new DateTime(1999, 8, 2)
            };
            
            var wrappedData = new WrappedValue<InventoryItem>(inventoryItem);
            var toml = TomlSerializer.Serialize(wrappedData);

            var tableString = string.Join("\n", new[]
            {
                $"slot = {inventoryItem.Slot}",
                $"key = \"{inventoryItem.Key}\"",
                $"displayName = \"{inventoryItem.DisplayName}\"",
                $"weight = {(double)inventoryItem.Weight}",
                $"amount = {inventoryItem.Amount}",
                $"maxAmount = {inventoryItem.MaxAmount}",
                $"canUse = {inventoryItem.CanUse.ToString().ToLowerInvariant()}",
                $"canDrop = {inventoryItem.CanDrop.ToString().ToLowerInvariant()}",
                $"acquiredAt = {inventoryItem.AcquiredAt:yyyy-MM-dd HH:mm:ss.fffZ}"
            });

            Assert.AreEqual($"[value]\n{tableString}\n", toml);
        }

        [Test]
        public void Serialize_StructType_ShouldSerializeNamedTable()
        {
            var location = new PlayerLocation
            {
                Map = 500,
                X = 24,
                Y = 42,
                ZIndex = 2
            };
            
            var wrappedData = new WrappedValue<PlayerLocation>(location);
            var toml = TomlSerializer.Serialize(wrappedData);

            var keyValuePairLines = new[]
            {
                $"map = {location.Map}",
                $"x = {location.X}",
                $"y = {location.Y}",
                $"zIndex = {location.ZIndex}"
            };
            var tableString = string.Join("\n", keyValuePairLines);

            Assert.AreEqual($"[value]\n{tableString}\n", toml);
        }
    }
}
