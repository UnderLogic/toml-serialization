using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_ClassType_ShouldSetFields()
        {
            var serializedItem = new InventoryItem
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
            var tomlString = TomlSerializer.Serialize(serializedItem);

            var deserializeItem = new InventoryItem();
            TomlSerializer.DeserializeInto(tomlString, deserializeItem);

            Assert.AreEqual(serializedItem.Slot, deserializeItem.Slot);
            Assert.AreEqual(serializedItem.Key, deserializeItem.Key);
            Assert.AreEqual(serializedItem.DisplayName, deserializeItem.DisplayName);
            Assert.AreEqual(serializedItem.Weight, deserializeItem.Weight);
            Assert.AreEqual(serializedItem.Amount, deserializeItem.Amount);
            Assert.AreEqual(serializedItem.MaxAmount, deserializeItem.MaxAmount);
            Assert.AreEqual(serializedItem.CanUse, deserializeItem.CanUse);
            Assert.AreEqual(serializedItem.CanDrop, deserializeItem.CanDrop);
            Assert.AreEqual(serializedItem.AcquiredAt, deserializeItem.AcquiredAt);
        }

        [Test]
        public void SerializeDeserialize_ClassType_ShouldSetNestedObjects()
        {
            var serializedPlayer = new Player
            {
                Stats = new PlayerStats
                {
                    Health = 1250,
                    MaxHealth = 2500,
                    Mana = 750,
                    MaxMana = 1000,
                    Strength = 10,
                    Intelligence = 20,
                    Wisdom = 30,
                    Constitution = 40,
                    Dexterity = 50
                }
            };
            
            var tomlString = TomlSerializer.Serialize(serializedPlayer);
            
            var deserializePlayer = new Player();
            TomlSerializer.DeserializeInto(tomlString, deserializePlayer);
            
            Assert.AreEqual(serializedPlayer.Stats.Health, deserializePlayer.Stats.Health);
            Assert.AreEqual(serializedPlayer.Stats.MaxHealth, deserializePlayer.Stats.MaxHealth);
            Assert.AreEqual(serializedPlayer.Stats.Mana, deserializePlayer.Stats.Mana);
            Assert.AreEqual(serializedPlayer.Stats.MaxMana, deserializePlayer.Stats.MaxMana);
            Assert.AreEqual(serializedPlayer.Stats.Strength, deserializePlayer.Stats.Strength);
            Assert.AreEqual(serializedPlayer.Stats.Intelligence, deserializePlayer.Stats.Intelligence);
            Assert.AreEqual(serializedPlayer.Stats.Wisdom, deserializePlayer.Stats.Wisdom);
            Assert.AreEqual(serializedPlayer.Stats.Constitution, deserializePlayer.Stats.Constitution);
            Assert.AreEqual(serializedPlayer.Stats.Dexterity, deserializePlayer.Stats.Dexterity);
        }
        
        [Test]
        public void SerializeDeserialize_ClassType_ShouldSetNestedStructs()
        {
            var serializedPlayer = new Player
            {
                Location = new PlayerLocation
                {
                    Map = 500,
                    X = 24,
                    Y = 42,
                    ZIndex = 1
                }
            };
            
            var tomlString = TomlSerializer.Serialize(serializedPlayer);
            
            var deserializePlayer = new Player();
            TomlSerializer.DeserializeInto(tomlString, deserializePlayer);
            
            Assert.AreEqual(serializedPlayer.Location.Map, deserializePlayer.Location.Map);
            Assert.AreEqual(serializedPlayer.Location.X, deserializePlayer.Location.X);
            Assert.AreEqual(serializedPlayer.Location.Y, deserializePlayer.Location.Y);
            Assert.AreEqual(serializedPlayer.Location.ZIndex, deserializePlayer.Location.ZIndex);
        }

        [Test]
        public void SerializeDeserialize_StructType_ShouldSetFields()
        {
            var location = new PlayerLocation
            {
                Map = 500,
                X = 24,
                Y = 42,
                ZIndex = 1
            };
            var serializedLocation = new WrappedValue<PlayerLocation>(location);
            var tomlString = TomlSerializer.Serialize(serializedLocation);

            var deserializeLocation = new WrappedValue<PlayerLocation>();
            TomlSerializer.DeserializeInto(tomlString, deserializeLocation);

            Assert.AreEqual(location.Map, deserializeLocation.Value.Map);
            Assert.AreEqual(location.X, deserializeLocation.Value.X);
            Assert.AreEqual(location.Y, deserializeLocation.Value.Y);
            Assert.AreEqual(location.ZIndex, deserializeLocation.Value.ZIndex);
        }
    }
}
