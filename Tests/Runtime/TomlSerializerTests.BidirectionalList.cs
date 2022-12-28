using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_NullList_ShouldBeEqual()
        {
            var serializedList = WrappedList<string>.Null();
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<string>.FromValues("Hello", "World!");
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.IsNull);
        }
        
        [Test]
        public void SerializeDeserialize_EmptyList_ShouldBeEqual()
        {
            var serializedList = WrappedList<string>.Empty();
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<string>.FromValues("Hello", "World!");
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.IsEmpty);
        }

        [Test]
        public void SerializeDeserialize_ListOfNulls_ShouldBeEqual()
        {
            var serializedList = WrappedList<string>.FromValues(null, null, null);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<string>.FromValues("Hello", "World!");
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_CharList_ShouldBeEqual()
        {
            var serializedList = WrappedList<char>.FromValues('A', '0', '#');
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<char>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_StringList_ShouldBeEqual()
        {
            var serializedList = WrappedList<string>.FromValues("Hello", "\"Quoted\"", "World!");
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<string>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }

        [Test]
        public void SerializeDeserialize_EnumList_ShouldBeEqual()
        {
            var serializedList =
                WrappedList<Direction>.FromValues(Direction.Up, Direction.Down, Direction.Left, Direction.Right);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<Direction>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }

        [Test]
        public void SerializeDeserialize_EnumFlagsList_ShouldBeEqual()
        {
            var serializedList =
                WrappedList<StatusEffects>.FromValues(StatusEffects.Poison, StatusEffects.Blind,
                    StatusEffects.Frozen | StatusEffects.Sleep | StatusEffects.Stun, StatusEffects.All);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<StatusEffects>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }

        [Test]
        public void SerializeDeserialize_Int8List_ShouldBeEqual()
        {
            var serializedList = WrappedList<sbyte>.FromValues(sbyte.MinValue, -1, 0, 1, sbyte.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<sbyte>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_Int16List_ShouldBeEqual()
        {
            var serializedList = WrappedList<short>.FromValues(short.MinValue, -1, 0, 1, short.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<short>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_Int32List_ShouldBeEqual()
        {
            var serializedList = WrappedList<int>.FromValues(int.MinValue, -1, 0, 1, int.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<int>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_Int64List_ShouldBeEqual()
        {
            var serializedList = WrappedList<long>.FromValues(long.MinValue, -1, 0, 1, long.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<long>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_UInt8List_ShouldBeEqual()
        {
            var serializedList = WrappedList<byte>.FromValues(0, 1, byte.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<byte>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_UInt16List_ShouldBeEqual()
        {
            var serializedList = WrappedList<ushort>.FromValues(0, 1, ushort.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<ushort>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_UInt32List_ShouldBeEqual()
        {
            var serializedList = WrappedList<uint>.FromValues(0, 1, uint.MaxValue);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<uint>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }
        
        [Test]
        public void SerializeDeserialize_DateTimeList_ShouldBeEqual()
        {
            var serializedList = WrappedList<DateTime>.FromValues(DateTime.MinValue, DateTime.MinValue);
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<DateTime>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.IsTrue(deserializedList.SequenceEqual(serializedList), "List contents are not equal");
        }

        [Test]
        public void SerializeDeserialize_MixedList_ShouldBeEqual()
        {
            var serializedList =
                WrappedList<object>.FromValues("John", 3.14, true, Direction.Left, new long[] { 10, 42 });
            var tomlString = TomlSerializer.Serialize(serializedList);

            var deserializedList = WrappedList<object>.Empty();
            TomlSerializer.DeserializeInto(tomlString, deserializedList);

            Assert.AreEqual(deserializedList[0], serializedList[0]);
            Assert.AreEqual(deserializedList[1], serializedList[1]);
            Assert.AreEqual(deserializedList[2], serializedList[2]);
            Assert.AreEqual(deserializedList[3].ToString(), serializedList[3].ToString());

            var serializedInnerList = serializedList[4] as long[];
            var deserializedInnerList = (deserializedList[4] as List<object>).Cast<long>();

            Assert.IsNotNull(serializedInnerList);
            Assert.IsTrue(deserializedInnerList.SequenceEqual(serializedInnerList), "List contents are not equal");
        }
    }
}
