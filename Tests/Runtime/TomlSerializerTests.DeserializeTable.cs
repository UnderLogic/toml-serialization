using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_ClassType_ShouldSetFields()
        {
            var inventoryItem = new InventoryItem();
            var wrappedObject = new WrappedValue<InventoryItem>(inventoryItem);

            var tableValues = string.Join("\n", new[]
            {
                "slot = 2",
                "key = \"item_red_potion\"",
                "displayName = \"Red Potion\"",
                "weight = 0.1",
                "amount= 5",
                "maxAmount = 30",
                "canUse = true",
                "canDrop = true",
                "acquiredAt = 1999-08-02"
            });

            var toml = $"[value]\n{tableValues}\n";
            TomlSerializer.DeserializeInto(toml, wrappedObject);

            var deserializedItem = wrappedObject.Value;
            Assert.AreEqual(2, deserializedItem.Slot);
            Assert.AreEqual("item_red_potion", deserializedItem.Key);
            Assert.AreEqual("Red Potion", deserializedItem.DisplayName);
            Assert.AreEqual(0.1f, deserializedItem.Weight);
            Assert.AreEqual(5, deserializedItem.Amount);
            Assert.AreEqual(30, deserializedItem.MaxAmount);
            Assert.AreEqual(true, deserializedItem.CanUse);
            Assert.AreEqual(true, deserializedItem.CanDrop);
            Assert.AreEqual(new DateTime(1999, 8, 2), deserializedItem.AcquiredAt);
        }

        [Test]
        public void Deserialize_StructType_ShouldSetField()
        {
            var location = new PlayerLocation();
            var wrappedObject = new WrappedValue<PlayerLocation>(location);

            var tableValues = string.Join("\n", new[]
            {
                "map = 500",
                "x = 24",
                "y = 12",
                "zIndex = 1",
            });

            var toml = $"[value]\n{tableValues}\n";
            TomlSerializer.DeserializeInto(toml, wrappedObject);

            var deserializedLocation = wrappedObject.Value;
            Assert.AreEqual(500, deserializedLocation.Map);
            Assert.AreEqual(24, deserializedLocation.X);
            Assert.AreEqual(12, deserializedLocation.Y);
            Assert.AreEqual(1, deserializedLocation.ZIndex);
        }
    }
}
