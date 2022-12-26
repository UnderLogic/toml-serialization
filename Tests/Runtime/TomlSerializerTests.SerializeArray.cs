using System;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullArray_ShouldSerializeNull()
        {
            var wrappedArray = WrappedArray<string>.Null();
            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual("array = null\n", toml);
        }

        [Test]
        public void Serialize_EmptyArray_ShouldSerializeEmpty()
        {
            var wrappedArray = WrappedArray<string>.Empty();
            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual("array = []\n", toml);
        }

        [Test]
        public void Serialize_BoolArray_ShouldSerializeLowerCase()
        {
            var wrappedArray = WrappedArray<bool>.FromValues(true, false);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => value.ToString().ToLowerInvariant());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_CharArray_ShouldSerializeQuoted()
        {
            var wrappedArray = WrappedArray<char>.FromValues('A', 'Z', '0', '9', '_');
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => $"\"{value}\"");
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_StringArray_ShouldSerializeQuoted()
        {
            var wrappedArray = WrappedArray<string>.FromValues("hello", "world");
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => $"\"{value}\"");
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_EnumArray_ShouldSerializeQuoted()
        {
            var wrappedArray =
                WrappedArray<Direction>.FromValues(Direction.Up, Direction.Down, Direction.Left, Direction.Right);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => $"\"{value}\"");
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_EnumFlagsArray_ShouldSerializeQuoted()
        {
            var wrappedArray =
                WrappedArray<StatusEffects>.FromValues(StatusEffects.None, StatusEffects.Poison, StatusEffects.Blind,
                    StatusEffects.Frozen | StatusEffects.Sleep,
                    StatusEffects.All);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => $"\"{value}\"");
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_Int8Array_ShouldSerializeLiteral()
        {
            var wrappedArray = WrappedArray<sbyte>.FromValues(sbyte.MinValue, -1, 0, 1, sbyte.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => value.ToString());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_Int16Array_ShouldSerializeLiteral()
        {
            var wrappedArray = WrappedArray<short>.FromValues(short.MinValue, -1, 0, 1, short.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => value.ToString());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_Int32Array_ShouldSerializeLiteral()
        {
            var wrappedArray = WrappedArray<int>.FromValues(int.MinValue, -1, 0, 1, int.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => value.ToString());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_Int64Array_ShouldSerializeLiteral()
        {
            var wrappedArray = WrappedArray<long>.FromValues(long.MinValue, -1, 0, 1, long.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => value.ToString());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_UInt8Array_ShouldSerializeLiteral()
        {
            var wrappedArray = WrappedArray<byte>.FromValues(0, 1, byte.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => value.ToString());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_UInt16Array_ShouldSerializeLiteral()
        {
            var wrappedArray = WrappedArray<ushort>.FromValues(0, 1, ushort.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => value.ToString());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_UInt32Array_ShouldSerializeLiteral()
        {
            var wrappedArray = WrappedArray<int>.FromValues(0, 1, int.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => value.ToString());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_FloatArray_ShouldSerializeLiteral()
        {
            var wrappedArray = WrappedArray<float>.FromValues(float.MinValue, -1, 0, 1, float.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => ((double)value).ToString());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_DoubleArray_ShouldSerializeLiteral()
        {
            var wrappedArray = WrappedArray<double>.FromValues(double.MinValue, -1, 0, 1, double.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => value.ToString());
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_DateTimeArray_ShouldSerializeIsoFormat()
        {
            var wrappedArray = WrappedArray<DateTime>.FromValues(DateTime.MinValue, DateTime.UtcNow, DateTime.MaxValue);
            var toml = TomlSerializer.Serialize(wrappedArray);

            var valueStrings = wrappedArray.Select(value => $"{value:yyyy-MM-dd HH:mm:ss.fffZ}");
            var arrayString = string.Join(", ", valueStrings);

            Assert.AreEqual($"array = [ {arrayString} ]\n", toml);
        }

        [Test]
        public void Serialize_MixedArray_ShouldSerializeInline()
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

            var wrappedArray =
                WrappedArray<object>.FromValues(true, 42, new[] { 3.14, 1.412 }, classObject, structObject,
                    Direction.Left);

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                "array = [ true, 42, [ 3.14, 1.412 ], { id = 99, name = \"Hidden Item\", weight = 0.5, hidden = true, createdAt = 2022-10-01 00:00:00.000Z }, { index = 31, x = 0.5, y = 1.5, z = 5 }, \"Left\" ]\n",
                toml);
        }
    }
}
