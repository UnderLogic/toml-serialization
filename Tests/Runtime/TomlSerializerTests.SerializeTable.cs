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
            var dataObject = new MockUser("John Doe", 42, new DateTime(2022, 10, 1));
            var wrappedData = new WrappedValue<MockUser>(dataObject);
            var toml = TomlSerializer.Serialize(wrappedData);

            var keyValuePairLines = new[]
            {
                $"name = \"{dataObject.Name}\"",
                $"age = {dataObject.Age}",
                $"dateOfBirth = {dataObject.DateOfBirth:yyyy-MM-dd HH:mm:ss.fffZ}"
            };
            var tableString = string.Join("\n", keyValuePairLines);

            Assert.AreEqual($"[value]\n{tableString}\n", toml);
        }

        [Test]
        public void Serialize_StructType_ShouldSerializeNamedTable()
        {
            var coord = new MockCoord(24, 42, 10);
            var wrappedData = new WrappedValue<MockCoord>(coord);
            var toml = TomlSerializer.Serialize(wrappedData);

            var keyValuePairLines = new[]
            {
                $"x = {(double)coord.X}",
                $"y = {(double)coord.Y}",
                $"z = {(double)coord.Z}"
            };
            var tableString = string.Join("\n", keyValuePairLines);

            Assert.AreEqual($"[value]\n{tableString}\n", toml);
        }

        [Test]
        public void Serialize_NestedType_ShouldSerializedDottedTables()
        {
            var player = new MockPlayer("Newbie", 1, 50, 25, 1000,
                new[]
                {
                    new MockItem("stick", "Oak Stick"),
                    new MockItem("red_potion", "Red Potion", 5, 30),
                });

            var toml = TomlSerializer.Serialize(player);

            var playerString = string.Join("\n", new[]
            {
                $"name = \"{player.Name}\"",
                $"level = {player.Level}",
                $"health = {player.Health}",
                $"mana = {player.Mana}",
                $"gold = {player.Gold}",
            });
            
            var itemStrings = player.Inventory.Select(item => string.Join("\n", new[]
            {
                "[[inventory]]",
                $"key = \"{item.Key}\"",
                $"displayName = \"{item.DisplayName}\"",
                $"quantity = {item.Quantity}",
                $"maxQuantity = {item.MaxQuantity}"
            }));
            
            var inventoryString = string.Join("\n\n", itemStrings);
            var expectedString = string.Join("\n\n", playerString, inventoryString);
            Assert.AreEqual($"{expectedString}\n", toml);
        }
    }
}
