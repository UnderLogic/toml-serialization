using System.Globalization;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullIntegerList_ShouldSetNull()
        {
            var toml = "list = null\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<long>>(toml);
            Assert.That(deserializedList.IsNull, Is.EqualTo(true), "List should be null");
        }
        
        [Test]
        public void Deserialize_EmptyIntegerList_ShouldSetEmpty()
        {
            var toml = "list = []\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<long>>(toml);
            Assert.That(deserializedList.Count, Is.EqualTo(0), "List should be null");
        }
        
        [Test]
        public void Deserialize_Int8List_ShouldSetValues()
        {
            var expectedValues = new sbyte[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<sbyte>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int8ListMultiline_ShouldSetValues()
        {
            var expectedValues = new sbyte[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<sbyte>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int16List_ShouldSetValues()
        {
            var expectedValues = new short[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<short>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int16ListMultiline_ShouldSetValues()
        {
            var expectedValues = new short[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<short>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int32List_ShouldSetValues()
        {
            var expectedValues = new int[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<int>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int32ListMultiline_ShouldSetValues()
        {
            var expectedValues = new int[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<int>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int64List_ShouldSetValues()
        {
            var expectedValues = new long[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<long>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int64ListMultiline_ShouldSetValues()
        {
            var expectedValues = new long[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<long>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt8List_ShouldSetValues()
        {
            var expectedValues = new byte[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<byte>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt8ListMultiline_ShouldSetValues()
        {
            var expectedValues = new byte[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<byte>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt16List_ShouldSetValues()
        {
            var expectedValues = new ushort[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<ushort>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt16ListMultiline_ShouldSetValues()
        {
            var expectedValues = new ushort[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<ushort>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt32List_ShouldSetValues()
        {
            var expectedValues = new uint[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<uint>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt32ListMultiline_ShouldSetValues()
        {
            var expectedValues = new uint[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<uint>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }
    }
}
