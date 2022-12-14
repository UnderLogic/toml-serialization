using System.Globalization;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullIntegerArray_ShouldSetNull()
        {
            var toml = "array = null\n";
            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<long>>(toml);
            Assert.That(deserializedArray.IsNull, Is.EqualTo(true), "Array should be null");
        }
        
        [Test]
        public void Deserialize_EmptyIntegerArray_ShouldSetEmpty()
        {
            var toml = "array = []\n";
            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<long>>(toml);
            Assert.That(deserializedArray.Count, Is.EqualTo(0), "Array should be null");
        }
        
        [Test]
        public void Deserialize_Int8Array_ShouldSetValues()
        {
            var expectedValues = new sbyte[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<sbyte>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int8ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new sbyte[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<sbyte>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int16Array_ShouldSetValues()
        {
            var expectedValues = new short[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<short>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int16ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new short[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<short>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int32Array_ShouldSetValues()
        {
            var expectedValues = new int[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<int>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int32ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new int[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<int>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int64Array_ShouldSetValues()
        {
            var expectedValues = new long[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<long>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int64ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new long[] { -42, 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<long>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt8Array_ShouldSetValues()
        {
            var expectedValues = new byte[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<byte>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt8ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new byte[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<byte>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt16Array_ShouldSetValues()
        {
            var expectedValues = new ushort[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<ushort>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt16ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new ushort[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<ushort>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt32Array_ShouldSetValues()
        {
            var expectedValues = new uint[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<uint>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt32ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new uint[] { 0, 42 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<uint>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
