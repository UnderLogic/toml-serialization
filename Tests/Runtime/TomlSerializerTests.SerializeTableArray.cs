using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ClassArray_ShouldSerializeTableArray()
        {
            var firstItem = new MockSimpleClass
            {
                Id = 10,
                Name = "First Item",
                Weight = 1.5f,
                Hidden = false,
                CreatedAt = new DateTime(2022, 10, 1)
            };

            var secondItem = new MockSimpleClass
            {
                Id = 10,
                Name = "First Item",
                Weight = 1.5f,
                Hidden = false,
                CreatedAt = new DateTime(2022, 10, 1)
            };

            var wrappedArray = WrappedArray<MockSimpleClass>.FromValues(firstItem, secondItem);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var expectedFirstItem = string.Join("\n", new[]
            {
                $"id = {firstItem.Id}",
                $"name = \"{firstItem.Name}\"",
                $"weight = {firstItem.Weight}",
                $"hidden = {firstItem.Hidden.ToString().ToLowerInvariant()}",
                $"createdAt = {firstItem.CreatedAt:yyyy-MM-dd HH:mm:ss.fffZ}",
            });

            var expectedSecondItem = string.Join("\n", new[]
            {
                $"id = {secondItem.Id}",
                $"name = \"{secondItem.Name}\"",
                $"weight = {secondItem.Weight}",
                $"hidden = {secondItem.Hidden.ToString().ToLowerInvariant()}",
                $"createdAt = {secondItem.CreatedAt:yyyy-MM-dd HH:mm:ss.fffZ}",
            });

            Assert.AreEqual($"[[array]]\n{expectedFirstItem}\n\n[[array]]\n{expectedSecondItem}\n", toml);
        }

        [Test]
        public void Serialize_StructArray_ShouldSerializeTableArray()
        {
            var firstCoord = new MockSimpleStruct { Index = 10, X = 1.1f, Y = 2.2f, Z = 3.3f };
            var secondCoord = new MockSimpleStruct { Index = 20, X = 2.2f, Y = 4.4f, Z = 6.6f };

            var wrappedArray = WrappedArray<MockSimpleStruct>.FromValues(firstCoord, secondCoord);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var expectedCoord1 = string.Join("\n", new[]
            {
                $"index = {firstCoord.Index}",
                $"x = {(double)firstCoord.X}",
                $"y = {(double)firstCoord.Y}",
                $"z = {(double)firstCoord.Z}",
            });

            var expectedCoord2 = string.Join("\n", new[]
            {
                $"index = {secondCoord.Index}",
                $"x = {(double)secondCoord.X}",
                $"y = {(double)secondCoord.Y}",
                $"z = {(double)secondCoord.Z}",
            });

            Assert.AreEqual($"[[array]]\n{expectedCoord1}\n\n[[array]]\n{expectedCoord2}\n", toml);
        }
    }
}
