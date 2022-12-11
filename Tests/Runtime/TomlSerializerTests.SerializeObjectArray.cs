using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ObjectArray_AsArray()
        {
            var createdDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var document = new MockDocument(new[]
            {
                new MockRecord { id = 1, name = "John", x = 1, y = 2, z = 3, enabled = true, createdAt = createdDate },
                new MockRecord { id = 2, name = "Jane", x = 2, y = 4, z = 6, enabled = false, createdAt = createdDate }
            });

            var expectedString = document.ToTomlString();
            var tomlString = TomlSerializer.Serialize(document);

            Assert.AreEqual(expectedString.Trim(), tomlString.Trim());
        }
    }
}
