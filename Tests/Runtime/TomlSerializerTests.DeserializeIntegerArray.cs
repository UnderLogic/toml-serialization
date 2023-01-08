using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_Int8Array_ShouldSetValues()
        {
            var expectedValues = new sbyte[] { -42, 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<sbyte>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int8ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new sbyte[] { -42, 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<sbyte>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int16Array_ShouldSetValues()
        {
            var expectedValues = new short[] { -42, 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<short>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int16ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new short[] { -42, 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<short>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int32Array_ShouldSetValues()
        {
            var expectedValues = new int[] { -42, 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<int>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int32ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new int[] { -42, 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<int>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int64Array_ShouldSetValues()
        {
            var expectedValues = new long[] { -42, 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<long>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_Int64ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new long[] { -42, 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<long>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt8Array_ShouldSetValues()
        {
            var expectedValues = new byte[] { 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<byte>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt8ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new byte[] { 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<byte>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt16Array_ShouldSetValues()
        {
            var expectedValues = new ushort[] { 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<ushort>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt16ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new ushort[] { 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<ushort>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt32Array_ShouldSetValues()
        {
            var expectedValues = new uint[] { 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<uint>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_UInt32ArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new uint[] { 0, 42 };
            var toml = new TomlStringBuilder().AppendArray("array", expectedValues, true).ToString();

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<uint>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
