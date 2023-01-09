using System.Globalization;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullIntegerList_ShouldBeNull()
        {
            var list = SerializableList<long>.Null();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyIntegerList_ShouldBeEmpty()
        {
            var list = SerializableList<long>.Empty();
            var toml = TomlSerializer.Serialize(list);

            var expectedToml = "list = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int8List_ShouldBeLiterals()
        {
            var list = SerializableList<sbyte>.WithValues(-42, 0, 42);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int16List_ShouldBeLiterals()
        {
            var list = SerializableList<short>.WithValues(-42, 0, 42);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int32List_ShouldBeLiterals()
        {
            var list = SerializableList<int>.WithValues(-42, 0, 42);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_Int64List_ShouldBeLiterals()
        {
            var list = SerializableList<long>.WithValues(-42, 0, 42);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_UInt8List_ShouldBeLiterals()
        {
            var list = SerializableList<byte>.WithValues(0, 42);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_UInt16List_ShouldBeLiterals()
        {
            var list = SerializableList<ushort>.WithValues(0, 42);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_UInt32List_ShouldBeLiterals()
        {
            var list = SerializableList<uint>.WithValues(0, 42);
            var toml = TomlSerializer.Serialize(list);

            var expectedValueStrings = list.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var expectedToml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
