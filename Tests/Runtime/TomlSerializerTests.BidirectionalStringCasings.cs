using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_TomlCamelCaseAttribute_ShouldSetEqual()
        {
            var serializedValues = new WrappedCasedValues<int>(42);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedCasedValues<int>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.CamelValue, deserializedValues.CamelValue);
        }
        
        [Test]
        public void SerializeDeserialize_TomlPascalCaseAttribute_ShouldSetEqual()
        {
            var serializedValues = new WrappedCasedValues<int>(42);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedCasedValues<int>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.PascalValue, deserializedValues.PascalValue);
        }
        
        [Test]
        public void SerializeDeserialize_TomlSnakeCaseAttribute_ShouldSetEqual()
        {
            var serializedValues = new WrappedCasedValues<int>(42);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedCasedValues<int>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.SnakeValue, deserializedValues.SnakeValue);
        }
        
        [Test]
        public void SerializeDeserialize_TomlUpperSnakeCaseAttribute_ShouldSetEqual()
        {
            var serializedValues = new WrappedCasedValues<int>(42);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedCasedValues<int>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.UpperSnakeValue, deserializedValues.UpperSnakeValue);
        }
        
        [Test]
        public void SerializeDeserialize_TomlKebabCaseAttribute_ShouldSetEqual()
        {
            var serializedValues = new WrappedCasedValues<int>(42);
            var tomlString = TomlSerializer.Serialize(serializedValues);

            var deserializedValues = new WrappedCasedValues<int>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValues);

            Assert.AreEqual(serializedValues.KebabValue, deserializedValues.KebabValue);
        }
    }
}
