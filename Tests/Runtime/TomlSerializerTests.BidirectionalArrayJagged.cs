using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_NullArrayJagged_ShouldBeNull()
        {
            var serializedArray = WrappedJaggedArray<string>.Null();
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = new WrappedJaggedArray<string>(new[]
            {
                new[] { "Hello", "World" }
            });
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.IsNull, "Array should be null");
        }

        [Test]
        public void SerializeDeserialize_EmptyArrayJagged_ShouldBeEmpty()
        {
            var serializedArray = WrappedJaggedArray<string>.Empty();
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = new WrappedJaggedArray<string>(new[]
            {
                new[] { "Hello", "World" }
            });
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.IsEmpty, "Array should be empty");
        }

        [Test]
        public void SerializeDeserialize_ArrayOfNullsJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new string[] { null },
                new string[] { null, null },
                new string[] { null, null, null }
            };

            var serializedArray = new WrappedJaggedArray<string>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<string>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_BoolArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { true, false },
                new[] { true, false, true },
                new[] { false, true, false }
            };

            var serializedArray = new WrappedJaggedArray<bool>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<bool>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_CharArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { 'A', 'Z' },
                new[] { '0', '9', '_' },
                new[] { '!', '@', '#', '%' }
            };

            var serializedArray = new WrappedJaggedArray<char>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<char>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_StringArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { "Hello", "World" },
                new[] { "foo", "bar", "baz" },
                new[] { "The", "quick", "brown", "fox" }
            };

            var serializedArray = new WrappedJaggedArray<string>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<string>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_EnumArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { Direction.None },
                new[] { Direction.Up, Direction.Down },
                new[] { Direction.Left, Direction.Right }
            };

            var serializedArray = new WrappedJaggedArray<Direction>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<Direction>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_EnumFlagsArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { StatusEffects.All },
                new[] { StatusEffects.Poison, StatusEffects.Blind },
                new[] { StatusEffects.Frozen | StatusEffects.Sleep, StatusEffects.Silence | StatusEffects.Burn }
            };

            var serializedArray = new WrappedJaggedArray<StatusEffects>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<StatusEffects>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_Int8ArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new sbyte[] { 0 },
                new sbyte[] { sbyte.MinValue, sbyte.MaxValue },
                new sbyte[] { 10, 20, 30 }
            };

            var serializedArray = new WrappedJaggedArray<sbyte>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<sbyte>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_Int16ArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new short[] { 0 },
                new short[] { short.MinValue, short.MaxValue },
                new short[] { 10, 20, 30 }
            };

            var serializedArray = new WrappedJaggedArray<short>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<short>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_Int32ArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { 0 },
                new[] { int.MinValue, int.MaxValue },
                new[] { 10, 20, 30 }
            };

            var serializedArray = new WrappedJaggedArray<int>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<int>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_Int64ArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { 0L },
                new[] { long.MinValue, long.MaxValue },
                new[] { 10L, 20L, 30L }
            };

            var serializedArray = new WrappedJaggedArray<long>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<long>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_UInt8ArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new byte[] { 0 },
                new byte[] { 128, byte.MaxValue },
                new byte[] { 10, 20, 30 }
            };

            var serializedArray = new WrappedJaggedArray<byte>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<byte>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_UInt16ArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new ushort[] { 0 },
                new ushort[] { 128, ushort.MaxValue },
                new ushort[] { 10, 20, 30 }
            };

            var serializedArray = new WrappedJaggedArray<ushort>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<ushort>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_UInt32ArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { 0u },
                new[] { uint.MinValue, uint.MaxValue },
                new[] { 10u, 20u, 30u }
            };

            var serializedArray = new WrappedJaggedArray<uint>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<uint>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_FloatArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { 0f },
                new[] { float.MinValue, float.MaxValue },
                new[] { 10f, 20f, 30f }
            };

            var serializedArray = new WrappedJaggedArray<float>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<float>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_DoubleArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { 0d },
                new[] { 3.14e-10, 3.14e+10 },
                new[] { 10d, 20d, 30d }
            };

            var serializedArray = new WrappedJaggedArray<double>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<double>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_DateTimeArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new[] { new DateTime(2015, 1, 1) },
                new[]
                {
                    new DateTime(2015, 1, 1, 1, 2, 3),
                    new DateTime(2015, 1, 1, 4, 5, 6)
                },
                new[]
                {
                    new DateTime(2015, 1, 1, 7, 8, 9),
                    new DateTime(2015, 1, 1, 10, 11, 12),
                    new DateTime(2015, 1, 1, 13, 14, 15)
                }
            };

            var serializedArray = new WrappedJaggedArray<DateTime>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<DateTime>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }

        [Test]
        public void SerializeDeserialize_MixedArrayJagged_ShouldBeEqual()
        {
            var expectedValues = new[]
            {
                new object[] { 0, 1, 2 },
                new object[] { 3.14, 6.28, 9.42 },
                new object[] { "Hello", "World" },
                new object[] { true, false }
            };

            var serializedArray = new WrappedJaggedArray<object>(expectedValues);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedJaggedArray<object>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            AssertArrayJagged(deserializedArray, expectedValues);
        }
    }
}
