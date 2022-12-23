using System;
using System.Linq;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullList_ShouldSerializeNull()
        {
            var wrappedList = WrappedList<string>.Null();
            var toml = TomlSerializer.Serialize(wrappedList);

            Assert.AreEqual("list = null\n", toml);
        }

        [Test]
        public void Serialize_EmptyList_ShouldSerializeEmpty()
        {
            var wrappedList = WrappedList<string>.Empty();
            var toml = TomlSerializer.Serialize(wrappedList);

            Assert.AreEqual("list = []\n", toml);
        }

        [Test]
        public void Serialize_BoolList_ShouldSerializeLowerCase()
        {
            var wrappedList = WrappedList<bool>.FromValues(true, false);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => value.ToString().ToLowerInvariant());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_CharList_ShouldSerializeQuoted()
        {
            var wrappedList = WrappedList<char>.FromValues('A', 'Z', '0', '9', '_');
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => $"\"{value}\"");
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_StringList_ShouldSerializeQuoted()
        {
            var wrappedList = WrappedList<string>.FromValues("hello", "world");
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => $"\"{value}\"");
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_EnumList_ShouldSerializeQuoted()
        {
            var wrappedList =
                WrappedList<MockEnum>.FromValues(MockEnum.North, MockEnum.South, MockEnum.East, MockEnum.West);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => $"\"{value}\"");
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_EnumFlagsList_ShouldSerializeQuoted()
        {
            var wrappedList =
                WrappedList<MockFlags>.FromValues(MockFlags.None, MockFlags.Available | MockFlags.InProgress,
                    MockFlags.All);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => $"\"{value}\"");
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_Int8List_ShouldSerializeLiteral()
        {
            var wrappedList = WrappedList<sbyte>.FromValues(sbyte.MinValue, -1, 0, 1, sbyte.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => value.ToString());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_Int16List_ShouldSerializeLiteral()
        {
            var wrappedList = WrappedList<short>.FromValues(short.MinValue, -1, 0, 1, short.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => value.ToString());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_Int32List_ShouldSerializeLiteral()
        {
            var wrappedList = WrappedList<int>.FromValues(int.MinValue, -1, 0, 1, int.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => value.ToString());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_Int64List_ShouldSerializeLiteral()
        {
            var wrappedList = WrappedList<long>.FromValues(long.MinValue, -1, 0, 1, long.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => value.ToString());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_UInt8List_ShouldSerializeLiteral()
        {
            var wrappedList = WrappedList<byte>.FromValues(0, 1, byte.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => value.ToString());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_UInt16List_ShouldSerializeLiteral()
        {
            var wrappedList = WrappedList<ushort>.FromValues(0, 1, ushort.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => value.ToString());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_UInt32List_ShouldSerializeLiteral()
        {
            var wrappedList = WrappedList<int>.FromValues(0, 1, int.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => value.ToString());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_FloatList_ShouldSerializeLiteral()
        {
            var wrappedList = WrappedList<float>.FromValues(float.MinValue, -1, 0, 1, float.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => ((double)value).ToString());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_DoubleList_ShouldSerializeLiteral()
        {
            var wrappedList = WrappedList<double>.FromValues(double.MinValue, -1, 0, 1, double.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => value.ToString());
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_DateTimeList_ShouldSerializeIsoFormat()
        {
            var wrappedList = WrappedList<DateTime>.FromValues(DateTime.MinValue, DateTime.UtcNow, DateTime.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedList);

            var valueStrings = wrappedList.Select(value => $"{value:yyyy-MM-dd HH:mm:ss.fffZ}");
            var listString = string.Join(", ", valueStrings);

            Assert.AreEqual($"list = [ {listString} ]\n", toml);
        }

        [Test]
        public void Serialize_MixedList_ShouldSerializeInline()
        {
            var classObject = new MockSimpleClass
            {
                Id = 99,
                Name = "Hidden Item",
                Weight = 0.5f,
                Hidden = true,
                CreatedAt = new DateTime(2022, 10, 1)
            };

            var structObject = new MockSimpleStruct()
            {
                Index = 31,
                X = 0.5f,
                Y = 1.5f,
                Z = 5f
            };

            var wrappedList =
                WrappedList<object>.FromValues(true, 42, new[] { 3.14, 1.412 }, classObject, structObject,
                    MockEnum.West);

            var toml = TomlSerializer.Serialize(wrappedList);

            Assert.AreEqual(
                "list = [ true, 42, [ 3.14, 1.412 ], { id = 99, name = \"Hidden Item\", weight = 0.5, hidden = true, createdAt = 2022-10-01 00:00:00.000Z }, { index = 31, x = 0.5, y = 1.5, z = 5 }, \"West\" ]\n",
                toml);
        }
    }
}
