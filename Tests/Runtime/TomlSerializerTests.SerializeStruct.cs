using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_StructObject_ShouldSerializeTable()
        {
            var point = new SerializablePoint(2, 4, 6);
            var wrappedPoint = new SerializableValue<SerializablePoint>(point);
            var toml = TomlSerializer.Serialize(wrappedPoint);

            var expectedToml = new TomlStringBuilder()
                .AppendTableHeader("value")
                .AppendKeyValue("x", point.X)
                .AppendKeyValue("y", point.Y)
                .AppendKeyValue("z", point.Z)
                .ToString();

            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
