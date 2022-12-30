using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_TomlKeyAttribute_ShouldRenameKey()
        {
            var wrappedValue = new WrappedRenamedValue<string>("The quick brown fox jumps over the lazy dog.");
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"renamedValue = \"{wrappedValue.Value}\"\n", toml);
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldLowerCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("lowervalue = 42", lines, "Should contain lower-cased key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldUpperCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("UPPERVALUE = 42", lines, "Should contain upper-cased key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldCamelCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("camelValue = 42", lines, "Should contain camel-cased key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldPascalCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("PascalValue = 42", lines, "Should contain pascal-cased key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldSnakeCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("snake_value = 42", lines, "Should contain snake-cased key key");
        }
        
        [Test]
        public void Serialize_TomlCasingAttribute_ShouldKebabCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");
            
            Assert.Contains("kebab-value = 42", lines, "Should contain kebab-cased key");
        }
    }
}
