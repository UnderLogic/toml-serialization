using System;
using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("Alice", "User", 24, 120.5f, false)]
        [TestCase("Bob", "User", 42, 180.25f, true)]
        public void Deserialize_ClassFromTable_ShouldSetFields(string firstName, string lastName, int age, float weight,
            bool isAdmin)
        {
            var now = DateTime.Now.Date;

            var toml = new StringBuilder()
                .AppendLine("[value]")
                .AppendLine($"firstName = \"{firstName}\"")
                .AppendLine($"lastName = \"{lastName}\"")
                .AppendLine($"age = {age}")
                .AppendLine($"weight = {weight}")
                .AppendLine($"isAdmin = {isAdmin.ToString().ToLowerInvariant()}")
                .AppendLine($"createdDate = {now:yyyy-MM-dd HH:mm:ss.fffZ}")
                .ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<SerializableUser>>(toml);
            Assert.That(deserializedValue.Value.FirstName, Is.EqualTo(firstName));
            Assert.That(deserializedValue.Value.LastName, Is.EqualTo(lastName));
            Assert.That(deserializedValue.Value.Age, Is.EqualTo(age));
            Assert.That(deserializedValue.Value.Weight, Is.EqualTo(weight));
            Assert.That(deserializedValue.Value.IsAdmin, Is.EqualTo(isAdmin));
            Assert.That(deserializedValue.Value.CreatedDate, Is.EqualTo(now));
        }

        [TestCase("Alice", "User", 24, 120.5f, false)]
        [TestCase("Bob", "User", 42, 180.25f, true)]
        public void Deserialize_ClassFromTableInline_ShouldSetFields(string firstName, string lastName, int age,
            float weight,
            bool isAdmin)
        {
            var now = DateTime.Now.Date;

            var toml = new StringBuilder()
                .Append("value = { ")
                .Append($"firstName = \"{firstName}\", ")
                .Append($"lastName = \"{lastName}\", ")
                .Append($"age = {age}, ")
                .Append($"weight = {weight}, ")
                .Append($"isAdmin = {isAdmin.ToString().ToLowerInvariant()}, ")
                .Append($"createdDate = {now:yyyy-MM-dd HH:mm:ss.fffZ}")
                .AppendLine(" }")
                .ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<SerializableUser>>(toml);
            Assert.That(deserializedValue.Value.FirstName, Is.EqualTo(firstName));
            Assert.That(deserializedValue.Value.LastName, Is.EqualTo(lastName));
            Assert.That(deserializedValue.Value.Age, Is.EqualTo(age));
            Assert.That(deserializedValue.Value.Weight, Is.EqualTo(weight));
            Assert.That(deserializedValue.Value.IsAdmin, Is.EqualTo(isAdmin));
            Assert.That(deserializedValue.Value.CreatedDate, Is.EqualTo(now));
        }
    }
}
