using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullCharArray_ShouldBeNull()
        {
            var array = SerializableArray<char>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendNullValue("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyCharArray_ShouldBeEmpty()
        {
            var array = SerializableArray<char>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("array").AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_CharArray_ShouldBeQuoted()
        {
            var array = SerializableArray<char>.WithValues('A', 'Z', '0', '9', '_', '-', '+');
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_CharArray_ShouldBeEscaped()
        {
            var array = SerializableArray<char>.WithValues('\\', '\t', '\r', '\n', '\f', '\"');
            var toml = TomlSerializer.Serialize(array);

            var expectedValues = new[] { "\\\\", "\\t", "\\r", "\\n", "\\f", "\\\"" };
            var expectedToml = new TomlStringBuilder().AppendArray("array", expectedValues).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_CharArray_ShouldAllowCommentChar()
        {
            var array = SerializableArray<char>.WithValues('#');
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
