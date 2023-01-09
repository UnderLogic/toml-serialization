using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("-42", -42)]
        [TestCase("0", 0)]
        [TestCase("42", 42)]
        public void Deserialize_Int8Value_ShouldSetValue(string stringValue, sbyte expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<sbyte>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("-42", -42)]
        [TestCase("0", 0)]
        [TestCase("42", 42)]
        public void Deserialize_Int16Value_ShouldSetValue(string stringValue, short expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<short>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("-42", -42)]
        [TestCase("0", 0)]
        [TestCase("42", 42)]
        public void Deserialize_Int32Value_ShouldSetValue(string stringValue, int expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<int>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("-42", -42)]
        [TestCase("0", 0)]
        [TestCase("42", 42)]
        public void Deserialize_Int64Value_ShouldSetValue(string stringValue, long expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<long>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("0", 0)]
        [TestCase("42", 42)]
        public void Deserialize_UInt8Value_ShouldSetValue(string stringValue, byte expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<byte>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("0", (ushort)0)]
        [TestCase("42", (ushort)42)]
        public void Deserialize_UInt16Value_ShouldSetValue(string stringValue, ushort expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<ushort>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }

        [TestCase("0", (uint)0)]
        [TestCase("42", (uint)42)]
        public void Deserialize_UInt32Value_ShouldSetValue(string stringValue, uint expectedValue)
        {
            var toml = $"value = {stringValue}\n";

            var deserializedValue = TomlSerializer.Deserialize<SerializableValue<uint>>(toml);
            Assert.That(deserializedValue.Value, Is.EqualTo(expectedValue));
        }
    }
}
