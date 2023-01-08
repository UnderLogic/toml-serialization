using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ClassObject_ShouldSerializeRootTable()
        {
            var user = new SerializableUser();
            var toml = TomlSerializer.Serialize(user);

            var expectedToml = new TomlStringBuilder()
                .AppendKeyValue("firstName", user.FirstName)
                .AppendKeyValue("lastName", user.LastName)
                .AppendKeyValue("age", user.Age)
                .AppendKeyValue("weight", user.Weight)
                .AppendKeyValue("isAdmin", false)
                .AppendKeyValue("createdDate", user.CreatedDate)
                .ToString();

            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_ClassObject_ShouldSerializeStandardTable()
        {
            var user = new SerializableUser();
            var wrappedUser = new SerializableValue<SerializableUser>(user);
            var toml = TomlSerializer.Serialize(wrappedUser);

            var expectedToml = new TomlStringBuilder()
                .AppendTableHeader("value")
                .AppendKeyValue("firstName", user.FirstName)
                .AppendKeyValue("lastName", user.LastName)
                .AppendKeyValue("age", user.Age)
                .AppendKeyValue("weight", user.Weight)
                .AppendKeyValue("isAdmin", false)
                .AppendKeyValue("createdDate", user.CreatedDate)
                .ToString();

            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
