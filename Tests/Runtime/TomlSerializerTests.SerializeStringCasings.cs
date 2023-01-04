using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_TomlCamelCaseAttribute_ShouldCamelCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");

            Assert.Contains("camelValue = 42", lines, "Should contain camel-cased key");
        }

        [Test]
        public void Serialize_TomlPascalCaseAttribute_ShouldPascalCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");

            Assert.Contains("PascalValue = 42", lines, "Should contain pascal-cased key");
        }

        [Test]
        public void Serialize_TomlSnakeCaseAttribute_ShouldSnakeCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");

            Assert.Contains("snake_value = 42", lines, "Should contain snake-cased key (lowercase)");
        }
        
        [Test]
        public void Serialize_TomeUpperSnakeCaseAttribute_ShouldUpperSnakeCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");

            Assert.Contains("UPPER_SNAKE_VALUE = 42", lines, "Should contain snake-cased key (uppercase)");
        }

        [Test]
        public void Serialize_TomlKebabCaseAttribute_ShouldKebabCaseKey()
        {
            var wrappedValues = new WrappedCasedValues<int>(42);
            var toml = TomlSerializer.Serialize(wrappedValues);

            var lines = toml.Split("\n");

            Assert.Contains("kebab-value = 42", lines, "Should contain kebab-cased key");
        }
    }
}
