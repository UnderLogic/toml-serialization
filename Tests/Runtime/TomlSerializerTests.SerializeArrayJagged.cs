using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullArrayJagged_ShouldSerializeNull()
        {
            var wrappedArray = WrappedJaggedArray<string>.Null();
            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual("array = null\n", toml);
        }

        [Test]
        public void Serialize_EmptyArrayJagged_ShouldSerializeEmpty()
        {
            var wrappedArray = WrappedJaggedArray<string>.Empty();

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual("array = []\n", toml);
        }

        [Test]
        public void Serialize_ArrayOfNullsJagged_ShouldSerializeNulls()
        {
            var wrappedArray = new WrappedJaggedArray<string>(new[]
            {
                new string[] { null },
                new string[] { null, null },
                new string[] { null, null, null }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual("array = [ [ null ], [ null, null ], [ null, null, null ] ]\n", toml);
        }

        [Test]
        public void Serialize_BoolArrayJagged_ShouldSerializeLowercase()
        {
            var wrappedArray = new WrappedJaggedArray<bool>(new[]
            {
                new[] { true },
                new[] { false, true },
                new[] { true, false, true }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual("array = [ [ true ], [ false, true ], [ true, false, true ] ]\n", toml);
        }

        [Test]
        public void Serialize_CharArrayJagged_ShouldSerializeQuoted()
        {
            var wrappedArray = new WrappedJaggedArray<char>(new[]
            {
                new[] { 'A' },
                new[] { '0', '9' },
                new[] { '!', '@', '#' }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual("array = [ [ \"A\" ], [ \"0\", \"9\" ], [ \"!\", \"@\", \"#\" ] ]\n", toml);
        }

        [Test]
        public void Serialize_StringArrayJagged_ShouldSerializeQuoted()
        {
            var wrappedArray = new WrappedJaggedArray<string>(new[]
            {
                new[] { "test" },
                new[] { "Hello", "World" },
                new[] { "A", "\"Quoted\"", "String" }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                "array = [ [ \"test\" ], [ \"Hello\", \"World\" ], [ \"A\", \"\\\\\"Quoted\\\\\"\", \"String\" ] ]\n",
                toml);
        }

        [Test]
        public void Serialize_EnumArrayJagged_ShouldSerializeQuoted()
        {
            var wrappedArray = new WrappedJaggedArray<Direction>(new[]
            {
                new[] { Direction.None },
                new[] { Direction.Up, Direction.Down },
                new[] { Direction.Left, Direction.Right }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual("array = [ [ \"None\" ], [ \"Up\", \"Down\" ], [ \"Left\", \"Right\" ] ]\n", toml);
        }

        [Test]
        public void Serialize_EnumFlagsArrayJagged_ShouldSerializeQuoted()
        {
            var wrappedArray = new WrappedJaggedArray<StatusEffects>(new[]
            {
                new[] { StatusEffects.All },
                new[] { StatusEffects.Poison, StatusEffects.Blind },
                new[] { StatusEffects.Frozen | StatusEffects.Sleep, StatusEffects.Silence | StatusEffects.Burn }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                "array = [ [ \"All\" ], [ \"Poison\", \"Blind\" ], [ \"Frozen, Sleep\", \"Silence, Burn\" ] ]\n", toml);
        }

        [Test]
        public void Serialize_Int8ArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<sbyte>(new[]
            {
                new sbyte[] { 0 },
                new sbyte[] { sbyte.MinValue, sbyte.MaxValue },
                new sbyte[] { 10, 20, 30 }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                $"array = [ [ 0 ], [ {sbyte.MinValue}, {sbyte.MaxValue} ], [ 10, 20, 30 ] ]\n", toml);
        }

        [Test]
        public void Serialize_Int16ArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<short>(new[]
            {
                new short[] { 0 },
                new short[] { short.MinValue, short.MaxValue },
                new short[] { 10, 20, 30 }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                $"array = [ [ 0 ], [ {short.MinValue}, {short.MaxValue} ], [ 10, 20, 30 ] ]\n", toml);
        }

        [Test]
        public void Serialize_Int32ArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<int>(new[]
            {
                new int[] { 0 },
                new int[] { int.MinValue, int.MaxValue },
                new int[] { 10, 20, 30 }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                $"array = [ [ 0 ], [ {int.MinValue}, {int.MaxValue} ], [ 10, 20, 30 ] ]\n", toml);
        }

        [Test]
        public void Serialize_Int64ArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<long>(new[]
            {
                new long[] { 0 },
                new long[] { long.MinValue, long.MaxValue },
                new long[] { 10, 20, 30 }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                $"array = [ [ 0 ], [ {long.MinValue}, {long.MaxValue} ], [ 10, 20, 30 ] ]\n", toml);
        }

        [Test]
        public void Serialize_UInt8ArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<byte>(new[]
            {
                new byte[] { 0 },
                new byte[] { 128, byte.MaxValue },
                new byte[] { 10, 20, 30 }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                $"array = [ [ 0 ], [ 128, {byte.MaxValue} ], [ 10, 20, 30 ] ]\n", toml);
        }

        [Test]
        public void Serialize_UInt16ArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<ushort>(new[]
            {
                new ushort[] { 0 },
                new ushort[] { 128, ushort.MaxValue },
                new ushort[] { 10, 20, 30 }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                $"array = [ [ 0 ], [ 128, {ushort.MaxValue} ], [ 10, 20, 30 ] ]\n", toml);
        }

        [Test]
        public void Serialize_UInt32ArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<uint>(new[]
            {
                new uint[] { 0 },
                new uint[] { 128, uint.MaxValue },
                new uint[] { 10, 20, 30 }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                $"array = [ [ 0 ], [ 128, {uint.MaxValue} ], [ 10, 20, 30 ] ]\n", toml);
        }

        [Test]
        public void Serialize_FloatArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<float>(new[]
            {
                new float[] { 0 },
                new float[] { float.MinValue, float.MaxValue },
                new float[] { 1.412f, 3.14f, 9.86f }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                $"array = [ [ 0 ], [ {(double)float.MinValue}, {(double)float.MaxValue} ], [ {(double)1.412f}, {(double)3.14f}, {(double)9.86f} ] ]\n",
                toml);
        }

        [Test]
        public void Serialize_DoubleArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<double>(new[]
            {
                new double[] { 0 },
                new double[] { double.MinValue, double.MaxValue },
                new double[] { 1.412, 3.14, 9.86 }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual(
                $"array = [ [ 0 ], [ {double.MinValue}, {double.MaxValue} ], [ 1.412, 3.14, 9.86 ] ]\n", toml);
        }

        [Test]
        public void Serialize_DateTimeArrayJagged_ShouldSerializeLiteral()
        {
            var wrappedArray = new WrappedJaggedArray<DateTime>(new[]
            {
                new[] { new DateTime(1979, 5, 27) },
                new[] { DateTime.MinValue.Date, DateTime.MaxValue.Date },
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            var firstDateString = new DateTime(1979, 5, 27).ToString("yyyy-MM-dd HH:mm:ss.fffZ");
            var minDateString = DateTime.MinValue.Date.ToString("yyyy-MM-dd HH:mm:ss.fffZ");
            var maxDateString = DateTime.MaxValue.Date.ToString("yyyy-MM-dd HH:mm:ss.fffZ");

            Assert.AreEqual(
                $"array = [ [ {firstDateString} ], [ {minDateString}, {maxDateString} ] ]\n",
                toml);
        }

        [Test]
        public void Serialize_MixedArrayJagged_ShouldSerializeInline()
        {
            var wrappedArray = new WrappedJaggedArray<object>(new[]
            {
                new object[] { "John" },
                new object[] { true, 42 },
                new object[] { 3.14, 1.412, 9.86 },
                new object[] { Direction.Up, StatusEffects.Poison }
            });

            var toml = TomlSerializer.Serialize(wrappedArray);

            Assert.AreEqual("array = [ [ \"John\" ], [ true, 42 ], [ 3.14, 1.412, 9.86 ], [ \"Up\", \"Poison\" ] ]\n",
                toml);
        }
    }
}
