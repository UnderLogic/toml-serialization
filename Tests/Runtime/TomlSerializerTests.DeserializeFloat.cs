using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("3.14e10", 3.14e10f)]
        [TestCase("3.14", 3.14f)]
        [TestCase("0", 0f)]
        [TestCase("-3.14", -3.14f)]
        [TestCase("-3.14e-10", -3.14e-10f)]
        public void Deserialize_FloatValue_ShouldSetValue(string stringValue, float expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<float>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("-nan", float.NaN)]
        [TestCase("nan", float.NaN)]
        [TestCase("+nan", float.NaN)]
        public void Deserialize_FloatValue_ShouldSetNaN(string stringValue, float expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<float>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("+inf", float.PositiveInfinity)]
        [TestCase("inf", float.PositiveInfinity)]
        public void Deserialize_FloatValue_ShouldSetPositiveInfinity(string stringValue, float expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<float>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("-inf", float.NegativeInfinity)]
        public void Deserialize_FloatValue_ShouldSetNegativeInfinity(string stringValue, float expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<float>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
    }
}
