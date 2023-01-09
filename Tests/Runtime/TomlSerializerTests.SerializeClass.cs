using System.Globalization;
using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ClassObject_ShouldSerializeRootTable()
        {
            var user = new SerializableUser();
            var toml = TomlSerializer.Serialize(user);

            var expectedToml = new StringBuilder()
                .AppendLine($"firstName = \"{user.FirstName}\"")
                .AppendLine($"lastName = \"{user.LastName}\"")
                .AppendLine($"age = {user.Age}")
                .AppendLine($"weight = {user.Weight.ToString(CultureInfo.InvariantCulture)}")
                .AppendLine($"isAdmin = {user.IsAdmin.ToString().ToLowerInvariant()}")
                .AppendLine($"createdDate = {user.CreatedDate:yyyy-MM-dd HH:mm:ss.fffZ}")
                .ToString();

            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_ClassObject_ShouldSerializeStandardTable()
        {
            var user = new SerializableUser();
            var wrappedUser = new SerializableValue<SerializableUser>(user);
            var toml = TomlSerializer.Serialize(wrappedUser);

            var expectedToml = new StringBuilder()
                .AppendLine("[value]")
                .AppendLine($"firstName = \"{user.FirstName}\"")
                .AppendLine($"lastName = \"{user.LastName}\"")
                .AppendLine($"age = {user.Age}")
                .AppendLine($"weight = {user.Weight.ToString(CultureInfo.InvariantCulture)}")
                .AppendLine($"isAdmin = {user.IsAdmin.ToString().ToLowerInvariant()}")
                .AppendLine($"createdDate = {user.CreatedDate:yyyy-MM-dd HH:mm:ss.fffZ}")
                .ToString();

            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
