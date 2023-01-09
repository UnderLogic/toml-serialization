using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullBooleanArray_ShouldBeNull()
        {
            var array = SerializableArray<bool>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyBooleanArray_ShouldBeEmpty()
        {
            var array = SerializableArray<bool>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_BooleanArray_ShouldBeLowercase()
        {
            var array = SerializableArray<bool>.WithValues(true, false, true);
            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = array.Select(x => x.ToString().ToLowerInvariant());
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
