using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ClassArray_ShouldSerializeTableArray()
        {
            var alice = new MockPerson("Alice", 21, new DateTime(2022, 10, 1));
            var bob = new MockPerson("Bob", 42, new DateTime(2022, 10, 1));

            var wrappedArray = WrappedArray<MockPerson>.FromValues(alice, bob);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var expectedAlice = string.Join("\n", new[]
            {
                $"name = \"{alice.Name}\"",
                $"age = {alice.Age}",
                $"dateOfBirth = {alice.DateOfBirth:yyyy-MM-dd HH:mm:ss.fffZ}",
            });

            var expectedBob = string.Join("\n", new[]
            {
                $"name = \"{bob.Name}\"",
                $"age = {bob.Age}",
                $"dateOfBirth = {bob.DateOfBirth:yyyy-MM-dd HH:mm:ss.fffZ}",
            });

            Assert.AreEqual($"[[array]]\n{expectedAlice}\n\n[[array]]\n{expectedBob}\n", toml);
        }

        [Test]
        public void Serialize_StructArray_ShouldSerializeTableArray()
        {
            var firstCoord = new MockCoord(1, 3, 5);
            var secondCoord = new MockCoord(2, 4, 6);

            var wrappedArray = WrappedArray<MockCoord>.FromValues(firstCoord, secondCoord);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var expectedCoord1 = string.Join("\n", new[]
            {
                $"x = {(double)firstCoord.X}",
                $"y = {(double)firstCoord.Y}",
                $"z = {(double)firstCoord.Z}",
            });

            var expectedCoord2 = string.Join("\n", new[]
            {
                $"x = {(double)secondCoord.X}",
                $"y = {(double)secondCoord.Y}",
                $"z = {(double)secondCoord.Z}",
            });

            Assert.AreEqual($"[[array]]\n{expectedCoord1}\n\n[[array]]\n{expectedCoord2}\n", toml);
        }
    }
}
