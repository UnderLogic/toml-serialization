using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

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

            var toml = new TomlStringBuilder()
                .AppendTableHeader("value")
                .AppendKeyValue("firstName", firstName)
                .AppendKeyValue("lastName", lastName)
                .AppendKeyValue("age", age)
                .AppendKeyValue("weight", weight)
                .AppendKeyValue("isAdmin", isAdmin)
                .AppendKeyValue("createdDate", now)
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
        public void Deserialize_ClassFromInlineTable_ShouldSetFields(string firstName, string lastName, int age, float weight,
            bool isAdmin)
        {
            var now = DateTime.Now.Date;

            var inlineDict = new Dictionary<string, object>
            {
                { "firstName", firstName },
                { "lastName", lastName },
                { "age", age },
                { "weight", weight },
                { "isAdmin", isAdmin },
                { "createdDate", now }
            };
            
            var toml = new TomlStringBuilder().AppendInlineTable("value", inlineDict).ToString();

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
