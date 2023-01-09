using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("3.14_15_9", 3.14159f)]
        [TestCase("-3.14_15_9", -3.14159f)]
        public void Deserialize_FloatSpacing_ShouldSetValue(string stringValue, float expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<float>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("3.14e-10", 3.14e-10f)]
        [TestCase("-3.14e+10", -3.14e10f)]
        public void Deserialize_FloatScientificNotation_ShouldSetValue(string stringValue, float expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<float>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("3.14_15_9", 3.14159)]
        [TestCase("-3.14_15_9", -3.14159)]
        public void Deserialize_DoubleSpacing_ShouldSetValue(string stringValue, double expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<double>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
        
        [TestCase("3.14e-10", 3.14e-10)]
        [TestCase("-3.14e+10", -3.14e10)]
        public void Deserialize_DoubleScientificNotation_ShouldSetValue(string stringValue, double expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<double>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
    }
}
