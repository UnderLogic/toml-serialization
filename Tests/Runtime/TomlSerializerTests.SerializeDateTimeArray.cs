using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullDateTimeArray_ShouldBeNull()
        {
            var array = SerializableArray<DateTime>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendNullValue("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyDateTimeArray_ShouldBeEmpty()
        {
            var array = SerializableArray<DateTime>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DateTimeArray_ShouldBeIso8601Format()
        {
            var array = SerializableArray<DateTime>.WithValues(
                new DateTime(1979, 5, 27),
                new DateTime(2000, 1, 1));

            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
