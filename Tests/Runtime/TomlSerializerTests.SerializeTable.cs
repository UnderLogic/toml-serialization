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
                { };

            var nestedItem = new MockNestedItem
            {
                Id = 1,
                Key = "item_apple",
                DisplayName = "Apple",
                Weight = 0.33f,
                Identified = true,
                Quantity = 3,
                MaxQuantity = 10
            };
            
            nestedItem.AddModifier("health", 10);
            nestedClassObject.AddItem(nestedItem);
            
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
            var nestedItemsString = string.Join("\n", nestedItemLines);
            
            Assert.AreEqual($"{tableString}\n\n{nestedItemsString}\n", toml);
        }
    }
}
