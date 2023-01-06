using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullBooleanArray_ShouldBeNull()
        {
            var array = SerializableArray<bool>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendNullKeyValue("array").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyBooleanArray_ShouldBeEmpty()
        {
            var array = SerializableArray<bool>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("array").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_BooleanArray_ShouldBeLowercase()
        {
            var array = SerializableArray<bool>.WithValues(true, false, true);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
