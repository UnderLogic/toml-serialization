using System;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_NullArray_ShouldBeNull()
        {
            var serializedArray = WrappedArray<string>.Null();
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<string>.FromValues("Hello", "World!");
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.IsNull);
        }
        
        [Test]
        public void SerializeDeserialize_EmptyArray_ShouldBeEmpty()
        {
            var serializedArray = WrappedArray<string>.Empty();
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<string>.FromValues("Hello", "World!");
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.IsEmpty);
        }

        [Test]
        public void SerializeDeserialize_ArrayOfNulls_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<string>.FromValues(null, null, null);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<string>.FromValues("Hello", "World!");
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_CharArray_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<char>.FromValues('A', '0', '#');
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<char>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_StringArray_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<string>.FromValues("Hello", "\"Quoted\"", "World!");
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<string>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }

        [Test]
        public void SerializeDeserialize_EnumArray_ShouldBeEqual()
        {
            var serializedArray =
                WrappedArray<Direction>.FromValues(Direction.Up, Direction.Down, Direction.Left, Direction.Right);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<Direction>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }

        [Test]
        public void SerializeDeserialize_EnumFlagsArray_ShouldBeEqual()
        {
            var serializedArray =
                WrappedArray<StatusEffects>.FromValues(StatusEffects.Poison, StatusEffects.Blind,
                    StatusEffects.Frozen | StatusEffects.Sleep | StatusEffects.Stun, StatusEffects.All);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<StatusEffects>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }

        [Test]
        public void SerializeDeserialize_Int8Array_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<sbyte>.FromValues(sbyte.MinValue, -1, 0, 1, sbyte.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<sbyte>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_Int16Array_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<short>.FromValues(short.MinValue, -1, 0, 1, short.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<short>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_Int32Array_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<int>.FromValues(int.MinValue, -1, 0, 1, int.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<int>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_Int64Array_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<long>.FromValues(long.MinValue, -1, 0, 1, long.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<long>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_UInt8Array_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<byte>.FromValues(0, 1, byte.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<byte>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_UInt16Array_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<ushort>.FromValues(0, 1, ushort.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<ushort>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_UInt32Array_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<uint>.FromValues(0, 1, uint.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<uint>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_DateTimeArray_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<DateTime>.FromValues(DateTime.MinValue, DateTime.MinValue);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<DateTime>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.IsTrue(deserializedArray.SequenceEqual(serializedArray), "Array contents are not equal");
        }

        [Test]
        public void SerializeDeserialize_MixedArray_ShouldBeEqual()
        {
            var serializedArray =
                WrappedArray<object>.FromValues("John", 3.14, true, Direction.Left, new long[] { 10, 42 });
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializedArray = WrappedArray<object>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedArray);

            Assert.AreEqual(deserializedArray[0], serializedArray[0]);
            Assert.AreEqual(deserializedArray[1], serializedArray[1]);
            Assert.AreEqual(deserializedArray[2], serializedArray[2]);
            Assert.AreEqual(deserializedArray[3].ToString(), serializedArray[3].ToString());

            var serializedInnerArray = serializedArray[4] as long[];
            var deserializedInnerArray = (deserializedArray[4] as object[]).Cast<long>();

            Assert.IsNotNull(serializedInnerArray);
            Assert.IsTrue(deserializedInnerArray.SequenceEqual(serializedInnerArray), "Array contents are not equal");
        }
    }
}
