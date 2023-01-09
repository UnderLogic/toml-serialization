using System;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullDateTimeArray_ShouldBeNull()
        {
            var array = SerializableArray<DateTime>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyDateTimeArray_ShouldBeEmpty()
        {
            var array = SerializableArray<DateTime>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DateTimeArray_ShouldBeIso8601Format()
        {
            var array = SerializableArray<DateTime>.WithValues(
                new DateTime(1979, 5, 27),
                new DateTime(2000, 1, 1));

            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = array.Select(x => x.ToString("yyyy-MM-dd HH:mm:ss.fffZ"));
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
