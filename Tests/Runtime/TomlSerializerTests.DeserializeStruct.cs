using System.Collections.Generic;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(2, 4, 6)]
        [TestCase(3.14f,  3.14f, 3.14f)]
        [TestCase(-42e10f, 0, 42e10f)]
        public void Deserialize_StructFromTable_ShouldSetFields(float x, float y, float z)
        {
            var toml = new TomlStringBuilder()
                .AppendTableHeader("value")
                .AppendKeyValue("x", x)
                .AppendKeyValue("y", y)
                .AppendKeyValue("z", z)
                .ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<SerializablePoint>>(toml);
            Assert.That(deserializedValue.Value.X, Is.EqualTo(x));
            Assert.That(deserializedValue.Value.Y, Is.EqualTo(y));
            Assert.That(deserializedValue.Value.Z, Is.EqualTo(z));
        }
        
        [TestCase(2, 4, 6)]
        [TestCase(3.14f, 3.14f, 3.14f)]
        [TestCase(-42e10f, 0, 42e10f)]
        public void Deserialize_StructFromInlineTable_ShouldSetFields(float x, float y, float z)
        {
            var inlineDict = new Dictionary<string, float>
            {
                { "x", x },
                { "y", y },
                { "z", z }
            };

            var toml = new TomlStringBuilder().AppendInlineTable("value", inlineDict).ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<SerializablePoint>>(toml);
            Assert.That(deserializedValue.Value.X, Is.EqualTo(x));
            Assert.That(deserializedValue.Value.Y, Is.EqualTo(y));
            Assert.That(deserializedValue.Value.Z, Is.EqualTo(z));
        }
    }
}
