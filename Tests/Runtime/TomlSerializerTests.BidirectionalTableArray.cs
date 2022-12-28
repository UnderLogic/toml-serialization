using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_ClassType_ShouldSetObjectArrays()
        {
            var serializedPlayer = new Player();
            var inventoryItems = new[]
            {
                new()
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
                },
                new InventoryItem
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
                }
            };

            foreach (var inventoryItem in inventoryItems)
                serializedPlayer.AddInventoryItem(inventoryItem);

            var tomlString = TomlSerializer.Serialize(serializedPlayer);

            var deserializePlayer = new Player();
            TomlSerializer.DeserializeInto(tomlString, deserializePlayer);

            Assert.IsNotEmpty(deserializePlayer.Inventory, "Inventory should not be empty");

            for (var i = 0; i < inventoryItems.Length; i++)
            {
                var expectedItem = inventoryItems[i];
                var actualItem = deserializePlayer.Inventory[i];

                Assert.AreEqual(expectedItem.Slot, actualItem.Slot);
                Assert.AreEqual(expectedItem.Key, actualItem.Key);
                Assert.AreEqual(expectedItem.DisplayName, actualItem.DisplayName);
                Assert.AreEqual(expectedItem.Weight, actualItem.Weight);
                Assert.AreEqual(expectedItem.Amount, actualItem.Amount);
                Assert.AreEqual(expectedItem.MaxAmount, actualItem.MaxAmount);
                Assert.AreEqual(expectedItem.CanUse, actualItem.CanUse);
                Assert.AreEqual(expectedItem.CanDrop, actualItem.CanDrop);
                Assert.AreEqual(expectedItem.AcquiredAt, actualItem.AcquiredAt);
            }
        }
    }
}
