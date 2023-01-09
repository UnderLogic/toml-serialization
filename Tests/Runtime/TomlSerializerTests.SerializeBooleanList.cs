using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullBooleanList_ShouldBeNull()
        {
            var list = SerializableList<bool>.Null();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyBooleanList_ShouldBeEmpty()
        {
            var list = SerializableList<bool>.Empty();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_BooleanList_ShouldBeLowercase()
        {
            var list = SerializableList<bool>.WithValues(true, false, true);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => x.ToString().ToLowerInvariant());
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
