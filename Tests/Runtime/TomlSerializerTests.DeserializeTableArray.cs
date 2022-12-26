using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_ClassArray_ShouldSetElements()
        {
            var toml = string.Join("\n", new[]
            {
                "[[array]]",
                "slot = 2",
                "key = \"item_red_potion\"",
                "displayName = \"Red Potion\"",
                "weight = 0.1",
                "amount= 5",
                "maxAmount = 30",
                "canUse = true",
                "canDrop = true",
                "acquiredAt = 1999-08-02",
                "",
                "[[array]]",
                "slot = 3",
                "key = \"item_oak_stick\"",
                "displayName = \"Oak Stick\"",
                "weight = 0.5",
                "amount= 1",
                "maxAmount = 1",
                "canUse = false",
                "canDrop = true",
                "acquiredAt = 1999-08-02"
            });

            var array = WrappedArray<InventoryItem>.Empty();
            TomlSerializer.DeserializeInto(toml, array);

            Assert.IsFalse(array.IsEmpty, "Array should not be empty");

            var firstItem = array[0];
            Assert.AreEqual(2, firstItem.Slot);
            Assert.AreEqual("item_red_potion", firstItem.Key);
            Assert.AreEqual("Red Potion", firstItem.DisplayName);
            Assert.AreEqual(0.1f, firstItem.Weight);
            Assert.AreEqual(5, firstItem.Amount);
            Assert.AreEqual(30, firstItem.MaxAmount);
            Assert.AreEqual(true, firstItem.CanUse);
            Assert.AreEqual(true, firstItem.CanDrop);
            Assert.AreEqual(new DateTime(1999, 8, 2), firstItem.AcquiredAt);

            var secondItem = array[1];
            Assert.AreEqual(3, secondItem.Slot);
            Assert.AreEqual("item_oak_stick", secondItem.Key);
            Assert.AreEqual("Oak Stick", secondItem.DisplayName);
            Assert.AreEqual(0.5f, secondItem.Weight);
            Assert.AreEqual(1, secondItem.Amount);
            Assert.AreEqual(1, secondItem.MaxAmount);
            Assert.AreEqual(false, secondItem.CanUse);
            Assert.AreEqual(true, secondItem.CanDrop);
            Assert.AreEqual(new DateTime(1999, 8, 2), secondItem.AcquiredAt);
        }

        [Test]
        public void Deserialize_StructArray_ShouldSetElements()
        {
            var toml = string.Join("\n", new[]
            {
                "[[array]]",
                "map = 500",
                "x = 4",
                "y = 8",
                "zIndex = 2",
                "",
                "[[array]]",
                "map = 3008",
                "x = 42",
                "y = 99",
                "zIndex = 10"
            });
            
            var array = WrappedArray<PlayerLocation>.Empty();
            TomlSerializer.DeserializeInto(toml, array);

            Assert.IsFalse(array.IsEmpty, "Array should not be empty");

            var firstLocation = array[0];
            Assert.AreEqual(500, firstLocation.Map);
            Assert.AreEqual(4, firstLocation.X);
            Assert.AreEqual(8, firstLocation.Y);
            Assert.AreEqual(2, firstLocation.ZIndex);

            var secondLocation = array[1];
            Assert.AreEqual(3008, secondLocation.Map);
            Assert.AreEqual(42, secondLocation.X);
            Assert.AreEqual(99, secondLocation.Y);
            Assert.AreEqual(10, secondLocation.ZIndex);
        }
    }
}
