using System.Globalization;
using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(2, 4, 6)]
        [TestCase(3.14f, 3.14f, 3.14f)]
        [TestCase(-42e10f, 0, 42e10f)]
        public void Deserialize_StructFromTable_ShouldSetFields(float x, float y, float z)
        {
            var toml = new StringBuilder()
                .AppendLine("[value]")
                .AppendLine($"x = {x.ToString(CultureInfo.InvariantCulture)}")
                .AppendLine($"y = {y.ToString(CultureInfo.InvariantCulture)}")
                .AppendLine($"z = {z.ToString(CultureInfo.InvariantCulture)}")
                .ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<SerializablePoint>>(toml);
            Assert.That(deserializedValue.Value.X, Is.EqualTo(x));
            Assert.That(deserializedValue.Value.Y, Is.EqualTo(y));
            Assert.That(deserializedValue.Value.Z, Is.EqualTo(z));
        }

        [TestCase(2, 4, 6)]
        [TestCase(3.14f, 3.14f, 3.14f)]
        [TestCase(-42e10f, 0, 42e10f)]
        public void Deserialize_StructFromTableInline_ShouldSetFields(float x, float y, float z)
        {
            var toml = new StringBuilder()
                .Append("value = { ")
                .Append($"x = {x.ToString(CultureInfo.InvariantCulture)}, ")
                .Append($"y = {y.ToString(CultureInfo.InvariantCulture)}, ")
                .Append($"z = {z.ToString(CultureInfo.InvariantCulture)}")
                .AppendLine(" }")
                .ToString();

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<SerializablePoint>>(toml);
            Assert.That(deserializedValue.Value.X, Is.EqualTo(x));
            Assert.That(deserializedValue.Value.Y, Is.EqualTo(y));
            Assert.That(deserializedValue.Value.Z, Is.EqualTo(z));
        }
    }
}
