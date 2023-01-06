using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullIntegerArray_ShouldBeNull()
        {
            var array = SerializableArray<long>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendNullKeyValue("array").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyIntegerArray_ShouldBeEmpty()
        {
            var array = SerializableArray<long>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendEmptyArray("array").ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int8Array_ShouldBeLiterals()
        {
            var array = SerializableArray<sbyte>.WithValues(-42, 0, 42);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int16Array_ShouldBeLiterals()
        {
            var array = SerializableArray<short>.WithValues(-42, 0, 42);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int32Array_ShouldBeLiterals()
        {
            var array = SerializableArray<int>.WithValues(-42, 0, 42);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int64Array_ShouldBeLiterals()
        {
            var array = SerializableArray<long>.WithValues(-42, 0, 42);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_UInt8Array_ShouldBeLiterals()
        {
            var array = SerializableArray<byte>.WithValues(0, 42);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_UInt16Array_ShouldBeLiterals()
        {
            var array = SerializableArray<ushort>.WithValues(0, 42);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_UInt32Array_ShouldBeLiterals()
        {
            var array = SerializableArray<uint>.WithValues(0, 42);
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = new TomlStringBuilder().AppendArray("array", array).ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
