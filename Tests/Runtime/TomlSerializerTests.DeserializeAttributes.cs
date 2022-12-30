using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_TomlKeyAttribute_ShouldMapKey()
        {
            const string toml = "renamedValue = \"The quick brown fox jumps over the lazy dog.\"";

            var deserializedValue = new WrappedRenamedValue<string>();
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            Assert.AreEqual("The quick brown fox jumps over the lazy dog.", deserializedValue.Value);
        }
        
        [Test]
        public void Deserialize_TomlCasingAttribute_ShouldMapLowerCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "lowervalue = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.LowerValue);
        }

        [Test]
        public void Deserialize_TomlCasingAttribute_ShouldMapUpperCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "UPPERVALUE = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.UpperValue);
        }

        [Test]
        public void Deserialize_TomlCasingAttribute_ShouldMapCamelCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "camelValue = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.CamelValue);
        }
        
        [Test]
        public void Deserialize_TomlCasingAttribute_ShouldMapPascalCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "PascalValue = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.PascalValue);
        }
        
        [Test]
        public void Deserialize_TomlCasingAttribute_ShouldMapSnakeCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "snake_value = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.SnakeValue);
        }
        
        [Test]
        public void Deserialize_TomlCasingAttribute_ShouldMapKebabCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(0);
            const string toml = "kebab-value = 42\n";

            TomlSerializer.DeserializeInto(toml, wrappedValues);
            Assert.AreEqual(42, wrappedValues.KebabValue);
        }
    }
}
