using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ClassArray_ShouldSerializeTableArray()
        {
            var firstItem = new InventoryItem
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
            
            var secondItem = new InventoryItem
            {
                Slot = 3,
                Key = "item_oak_stick",
                DisplayName = "Oak Stick",
                Weight = 0.5f,
                Amount = 1,
                MaxAmount = 1,
                CanUse = false,
                CanDrop = true,
                AcquiredAt = new DateTime(1999, 8, 2)
            };

            var wrappedArray = WrappedArray<InventoryItem>.FromValues(firstItem, secondItem);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var expectedFirstItem = string.Join("\n", new[]
            {
                $"slot = {firstItem.Slot}",
                $"key = \"{firstItem.Key}\"",
                $"displayName = \"{firstItem.DisplayName}\"",
                $"weight = {(double)firstItem.Weight}",
                $"amount = {firstItem.Amount}",
                $"maxAmount = {firstItem.MaxAmount}",
                $"canUse = {firstItem.CanUse.ToString().ToLowerInvariant()}",
                $"canDrop = {firstItem.CanDrop.ToString().ToLowerInvariant()}",
                $"acquiredAt = {firstItem.AcquiredAt:yyyy-MM-dd HH:mm:ss.fffZ}"
            });

            var expectedSecondItem = string.Join("\n", new[]
            {
                $"slot = {secondItem.Slot}",
                $"key = \"{secondItem.Key}\"",
                $"displayName = \"{secondItem.DisplayName}\"",
                $"weight = {(double)secondItem.Weight}",
                $"amount = {secondItem.Amount}",
                $"maxAmount = {secondItem.MaxAmount}",
                $"canUse = {secondItem.CanUse.ToString().ToLowerInvariant()}",
                $"canDrop = {secondItem.CanDrop.ToString().ToLowerInvariant()}",
                $"acquiredAt = {secondItem.AcquiredAt:yyyy-MM-dd HH:mm:ss.fffZ}"
            });

            Assert.AreEqual($"[[array]]\n{expectedFirstItem}\n\n[[array]]\n{expectedSecondItem}\n", toml);
        }

        [Test]
        public void Serialize_StructArray_ShouldSerializeTableArray()
        {
            var firstLocation = new PlayerLocation { Map = 500, X = 24, Y = 42, ZIndex = 1 };
            var secondLocation = new PlayerLocation { Map = 3008, X = 42, Y = 99, ZIndex = 2 };

            var wrappedArray = WrappedArray<PlayerLocation>.FromValues(firstLocation, secondLocation);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var expectedCoord1 = string.Join("\n", new[]
            {
                $"map = {firstLocation.Map}",
                $"x = {firstLocation.X}",
                $"y = {firstLocation.Y}",
                $"zIndex = {firstLocation.ZIndex}",
            });

            var expectedCoord2 = string.Join("\n", new[]
            {
                $"map = {secondLocation.Map}",
                $"x = {secondLocation.X}",
                $"y = {secondLocation.Y}",
                $"zIndex = {secondLocation.ZIndex}",
            });

            Assert.AreEqual($"[[array]]\n{expectedCoord1}\n\n[[array]]\n{expectedCoord2}\n", toml);
        }
    }
}
