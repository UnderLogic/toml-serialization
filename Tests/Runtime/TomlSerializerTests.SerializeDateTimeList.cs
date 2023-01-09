using System;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullDateTimeList_ShouldBeNull()
        {
            var list = SerializableList<DateTime>.Null();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyDateTimeList_ShouldBeEmpty()
        {
            var list = SerializableList<DateTime>.Empty();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DateTimeList_ShouldBeIso8601Format()
        {
            var list = SerializableList<DateTime>.WithValues(
                new DateTime(1979, 5, 27),
                new DateTime(2000, 1, 1));

            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => x.ToString("yyyy-MM-dd HH:mm:ss.fffZ"));
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
