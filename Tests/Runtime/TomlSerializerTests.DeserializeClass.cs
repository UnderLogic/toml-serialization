using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("Alice", "User", 24, 120.5f, false)]
        [TestCase("Bob", "User", 42, 180.25f, true)]
        public void Deserialize_ClassObject_ShouldParseStandardTable(string firstName, string lastName, int age, float weight, bool isAdmin)
        {
            var now = DateTime.UtcNow;

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
            
        }
    }
}
