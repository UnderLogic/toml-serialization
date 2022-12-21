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
            var classObject = new MockSimpleClass
            {
                Id = 99,
                Name = "Hidden Item",
                Weight = 0.5f,
                Hidden = true,
                CreatedAt = new DateTime(2022, 10, 1)
            };
            
            var wrappedData = new WrappedValue<MockSimpleClass>(classObject);
            var toml = TomlSerializer.Serialize(wrappedData);

            var keyValuePairLines = new[]
            {
                $"id = {classObject.Id}",
                $"name = \"{classObject.Name}\"",
                $"weight = {classObject.Weight}",
                $"hidden = {classObject.Hidden.ToString().ToLowerInvariant()}",
                $"createdAt = {classObject.CreatedAt:yyyy-MM-dd HH:mm:ss.fffZ}"
            };
            var tableString = string.Join("\n", keyValuePairLines);

            Assert.AreEqual($"[value]\n{tableString}\n", toml);
        }

        [Test]
        public void Serialize_StructType_ShouldSerializeNamedTable()
        {
            var structObject = new MockSimpleStruct
            {
                Index = 42,
                X = 0.125f,
                Y = 1.421f,
                Z = 3.141f
            };
            
            var wrappedData = new WrappedValue<MockSimpleStruct>(structObject);
            var toml = TomlSerializer.Serialize(wrappedData);

            var keyValuePairLines = new[]
            {
                $"index = {structObject.Index}",
                $"x = {(double)structObject.X}",
                $"y = {(double)structObject.Y}",
                $"z = {(double)structObject.Z}"
            };
            var tableString = string.Join("\n", keyValuePairLines);

            Assert.AreEqual($"[value]\n{tableString}\n", toml);
        }

        [Test]
        public void Serialize_NestedClass_ShouldSerializeTableArray()
        {
            var nestedClassObject = new MockNestedClass
            {
                Id = 42,
                Name = "Test Player",
                Level = 99,
                Health = 1000,
                MaxHealth = 2000,
                Mana = 500,
                MaxMana = 750,
                Gold = 1234567890,
                Experience = 1000000,
            };

            var nestedApple = new MockNestedItem
            {
                Id = 1,
                Key = "item_apple",
                DisplayName = "Apple",
                Weight = 0.33f,
                Identified = true,
                Quantity = 3,
                MaxQuantity = 10
            };
            
            var nestedSword = new MockNestedItem
            {
                Id = 2,
                Key = "item_sword",
                DisplayName = "Sword (Unidentified)",
                Weight = 1.5f,
                Identified = false,
                Quantity = 1,
                MaxQuantity = 1
            };
            
            nestedApple.AddModifier("health", 10);
            nestedSword.AddModifier("strength", 1);
            
            nestedClassObject.AddItem(nestedApple);
            nestedClassObject.AddItem(nestedSword);
            
            var toml = TomlSerializer.Serialize(nestedClassObject);
            
            var keyValuePairLines = new[]
            {
                $"id = {nestedClassObject.Id}",
                $"name = \"{nestedClassObject.Name}\"",
                $"level = {nestedClassObject.Level}",
                $"health = {nestedClassObject.Health}",
                $"maxHealth = {nestedClassObject.MaxHealth}",
                $"mana = {nestedClassObject.Mana}",
                $"maxMana = {nestedClassObject.MaxMana}",
                $"gold = {nestedClassObject.Gold}",
                $"experience = {nestedClassObject.Experience}",
            };
            var tableString = string.Join("\n", keyValuePairLines);

            var nestedItemLines = nestedClassObject.Inventory.Select(item => string.Join("\n", new[]
            {
                "[[inventory]]",
                $"id = {item.Id}",
                $"key = \"{item.Key}\"",
                $"displayName = \"{item.DisplayName}\"",
                $"identified = {item.Identified.ToString().ToLowerInvariant()}",
                $"weight = {(double)item.Weight}",
                $"quantity = {item.Quantity}",
                $"maxQuantity = {item.MaxQuantity}",
                $"modifiers = {{ {string.Join(", ", item.Modifiers.Select(modifier => $"{modifier.Key} = {modifier.Value}"))} }}",
            }));
            var nestedItemsString = string.Join("\n\n", nestedItemLines);
            
            Assert.AreEqual($"{tableString}\n\n{nestedItemsString}\n", toml);
        }
    }
}
