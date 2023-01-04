using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_TomlCamelCaseAttribute_ShouldMapCamelCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "camelValue = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.CamelValue);
        }
        
        [Test]
        public void Deserialize_TomlPascalCaseAttribute_ShouldMapPascalCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "PascalValue = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.PascalValue);
        }
        
        [Test]
        public void Deserialize_TomlSnakeCaseAttribute_ShouldMapSnakeCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "snake_value = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.SnakeValue);
        }
        
        [Test]
        public void Deserialize_TomlUpperSnakeCaseAttribute_ShouldMapSnakeCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "UPPER_SNAKE_VALUE = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.UpperSnakeValue);
        }
        
        [Test]
        public void Deserialize_TomlKebabCaseAttribute_ShouldMapKebabCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "kebab-value = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.KebabValue);
        }
    }
}
