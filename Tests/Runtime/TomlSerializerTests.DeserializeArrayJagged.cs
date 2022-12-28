using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullArrayJagged_ShouldSetNull()
        {
            var wrappedArray = new WrappedJaggedArray<string>(new[]
            {
                new[] { "hello", "world" }
            });
            const string toml = "array = null\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsTrue(wrappedArray.IsNull, "Array should be null");
        }

        [Test]
        public void Deserialize_EmptyArrayJagged_ShouldSetEmpty()
        {
            var wrappedArray = new WrappedJaggedArray<string>(new[]
            {
                new[] { "hello", "world" }
            });
            const string toml = "array = []\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsTrue(wrappedArray.IsEmpty, "Array should be empty");
        }

        [Test]
        public void Deserialize_ArrayOfNullsJagged_ShouldSetNulls()
        {
            var wrappedArray = new WrappedJaggedArray<string>(new[]
            {
                new[] { "hello", "world" }
            });
            const string toml = "array = [ [ null ], [ null, null ], [ null, null, null ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.AreEqual(3, wrappedArray.Count, "Array should have 3 elements");

            var expectedValues = new[]
            {
                new string[] { null },
                new string[] { null, null },
                new string[] { null, null, null }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_BoolArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<bool>.Empty();
            const string toml = "array = [ [ true, false ], [ false, true ], [ true, false, true ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { true, false },
                new[] { false, true },
                new[] { true, false, true }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_CharArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<char>.Empty();
            const string toml = "array = [ [ \"A\" ], [ \"0\", \"9\" ], [ \"!\", \"@\", \"#\" ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { 'A' },
                new[] { '0', '9' },
                new[] { '!', '@', '#' }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_StringArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<string>.Empty();
            const string toml =
                "array = [ [ \"test\" ], [ \"Hello\", \"World\" ], [ \"The\", \"quick\", \"brown\", \"fox\" ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { "test" },
                new[] { "Hello", "World" },
                new[] { "The", "quick", "brown", "fox" }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_EnumArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<Direction>.Empty();
            const string toml = "array = [ [ \"None\" ], [ \"Up\", \"Down\" ], [ \"Left\", \"Right\" ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { Direction.None },
                new[] { Direction.Up, Direction.Down },
                new[] { Direction.Left, Direction.Right }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_EnumFlagsArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<StatusEffects>.Empty();
            const string toml =
                "array = [ [ \"All\" ], [ \"Poison\", \"Blind\" ], [ \"Frozen, Stun\", \"Silence, Burn\" ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { StatusEffects.All },
                new[] { StatusEffects.Poison, StatusEffects.Blind },
                new[] { StatusEffects.Frozen | StatusEffects.Stun, StatusEffects.Silence | StatusEffects.Burn }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_Int8ArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<sbyte>.Empty();
            var toml = $"array = [ [ 0 ], [ {sbyte.MinValue}, {sbyte.MaxValue} ], [ 10, 20, 30 ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new sbyte[] { 0 },
                new sbyte[] { sbyte.MinValue, sbyte.MaxValue },
                new sbyte[] { 10, 20, 30 }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_Int16ArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<short>.Empty();
            var toml = $"array = [ [ 0 ], [ {short.MinValue}, {short.MaxValue} ], [ 10, 20, 30 ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new short[] { 0 },
                new short[] { short.MinValue, short.MaxValue },
                new short[] { 10, 20, 30 }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_Int32ArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<int>.Empty();
            var toml = $"array = [ [ 0 ], [ {int.MinValue}, {int.MaxValue} ], [ 10, 20, 30 ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { 0 },
                new[] { int.MinValue, int.MaxValue },
                new[] { 10, 20, 30 }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_Int64ArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<long>.Empty();
            var toml = $"array = [ [ 0 ], [ {long.MinValue}, {long.MaxValue} ], [ 10, 20, 30 ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { 0L },
                new[] { long.MinValue, long.MaxValue },
                new[] { 10L, 20L, 30L }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_UInt8ArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<byte>.Empty();
            var toml = $"array = [ [ 0 ], [ 128, {byte.MaxValue} ], [ 10, 20, 30 ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new byte[] { 0 },
                new byte[] { 128, byte.MaxValue },
                new byte[] { 10, 20, 30 }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_UInt16ArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<ushort>.Empty();
            var toml = $"array = [ [ 0 ], [ 128, {ushort.MaxValue} ], [ 10, 20, 30 ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new ushort[] { 0 },
                new ushort[] { 128, ushort.MaxValue },
                new ushort[] { 10, 20, 30 }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_UInt32ArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<uint>.Empty();
            var toml = $"array = [ [ 0 ], [ 128, {uint.MaxValue} ], [ 10, 20, 30 ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { 0u },
                new[] { 128u, uint.MaxValue },
                new[] { 10u, 20u, 30u }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_FloatArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<float>.Empty();
            var toml =
                $"array = [ [ 0 ], [ {(double)float.MinValue}, {(double)float.MaxValue} ], [ {(double)1.412f}, {(double)3.14f}, {(double)9.86f} ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { 0f },
                new[] { float.MinValue, float.MaxValue },
                new[] { 1.412f, 3.14f, 9.86f }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_DoubleArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<double>.Empty();
            var toml = $"array = [ [ 0 ], [ 3.14e-10, 3.14e+10 ], [ {1.412}, {3.14}, {9.86} ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new[] { 0d },
                new[] { 3.14e-10, 3.14e+10 },
                new[] { 1.412, 3.14, 9.86 }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_DateTimeArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<DateTime>.Empty();
            var toml = $"array = [ [ 1979-05-27 ], [ 1999-08-02, 2000-08-02 ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new DateTime[] { new(1979, 5, 27) },
                new DateTime[] { new(1999, 8, 2), new(2000, 8, 2) }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        [Test]
        public void Deserialize_MixedArrayJagged_ShouldSetElements()
        {
            var wrappedArray = WrappedJaggedArray<object>.Empty();
            var toml = $"array = [ [ \"John \" ], [ true, 42 ], [ 3.14, 1.412, 9.86], [ \"Up\", \"Poison\" ] ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);

            var expectedValues = new[]
            {
                new object[] { "John " },
                new object[] { true, 42 },
                new object[] { 3.14, 1.412, 9.86 },
                new object[] { $"{Direction.Up:F}", $"{StatusEffects.Poison:F}" }
            };
            AssertArrayJagged(wrappedArray, expectedValues);
        }

        // Helper method to assert the values of a jagged array
        private static void AssertArrayJagged<T>(WrappedJaggedArray<T> wrappedArray, T[][] expectedValues)
        {
            Assert.AreEqual(expectedValues.Length, wrappedArray.Count, "Array should have correct length");
            for (var i = 0; i < wrappedArray.Count; i++)
            {
                var expected = expectedValues[i];
                var actual = wrappedArray[i];
                Assert.AreEqual(expected.Length, actual.Length, "Array element should have correct length");

                for (var j = 0; j < expected.Length; j++)
                    Assert.AreEqual(expected[j], actual[j], "Array element should have correct value");
            }
        }
    }
}
