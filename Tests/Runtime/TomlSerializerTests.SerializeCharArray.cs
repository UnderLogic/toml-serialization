using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullCharArray_ShouldBeNull()
        {
            var array = SerializableArray<char>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyCharArray_ShouldBeEmpty()
        {
            var array = SerializableArray<char>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_CharArray_ShouldBeQuoted()
        {
            var array = SerializableArray<char>.WithValues('A', 'Z', '0', '9', '_', '-', '+');
            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = array.Select(x => $"\"{x}\"");
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_CharArray_ShouldBeEscaped()
        {
            var array = SerializableArray<char>.WithValues('\\', '\t', '\r', '\n', '\f', '\"');
            var toml = TomlSerializer.Serialize(array);

            var expectedValues = new[] { "\\\\", "\\t", "\\r", "\\n", "\\f", "\\\"" };
            var expectedValueStrings = expectedValues.Select(x => $"\"{x}\"");
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_CharArray_ShouldAllowCommentChar()
        {
            var array = SerializableArray<char>.WithValues('#');
            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = array.Select(x => $"\"{x}\"");
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
