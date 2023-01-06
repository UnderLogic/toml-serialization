using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullCharList_ShouldBeNull()
        {
            var list = SerializableList<char>.Null();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendNullKeyValue("list").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_EmptyCharList_ShouldBeEmpty()
        {
            var list = SerializableList<char>.Empty();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("list").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_CharList_ShouldBeQuoted()
        {
            var list = SerializableList<char>.WithValues('A', 'Z', '0', '9', '_', '-', '+');
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendArray("list", list).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_CharList_ShouldBeEscaped()
        {
            var list = SerializableList<char>.WithValues('\\', '\t', '\r', '\n', '\f', '\"');
            var toml = TomlSerializer.Serialize(list);

            var expectedValues = new[] { "\\\\", "\\t", "\\r", "\\n", "\\f", "\\\"" };
            var expectedToml = new TomlStringBuilder().AppendArray("list", expectedValues).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_CharList_ShouldAllowCommentChar()
        {
            var list = SerializableList<char>.WithValues('#');
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = new TomlStringBuilder().AppendArray("list", list).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
