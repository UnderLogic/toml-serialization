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
        public void SerializeDeserialize_NullDict_ShouldBeNull()
        {
            var serializedDict = WrappedDictionary<string>.Null();
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<string>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);
            
            Assert.IsTrue(deserializeDict.IsNull);
        }
        
        [Test]
        public void SerializeDeserialize_EmptyDict_ShouldBeEmpty()
        {
            var serializedDict = new WrappedDictionary<string>();
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<string> { { "test", "world" } };
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);
            
            Assert.IsTrue(deserializeDict.IsEmpty);
        }

        [Test]
        public void SerializeDeserialize_DictOfNulls_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<string>
                { { "null_1", null }, { "null_2", null }, { "null_3", null } };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<string>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.IsNull(deserializeDict["null_1"]);
            Assert.IsNull(deserializeDict["null_2"]);
            Assert.IsNull(deserializeDict["null_3"]);
        }

        [Test]
        public void SerializeDeserialize_CharDict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<char>
                { { "letter_a", 'A' }, { "digit_0", '0' }, { "symbol", '!' } };
            var tomlString = TomlSerializer.Serialize(serializedDict);
            
            var deserializeDict = new WrappedDictionary<char>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);
            
            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual('A', deserializeDict["letter_a"]);
            Assert.AreEqual('0', deserializeDict["digit_0"]);
            Assert.AreEqual('!', deserializeDict["symbol"]);
        }

        [Test]
        public void SerializeDeserialize_StringDict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<string>
                { { "hello", "world" }, { "foo", "bar" }, { "quoted", "this is a \"quoted\" string" } };
            var tomlString = TomlSerializer.Serialize(serializedDict);
            
            var deserializeDict = new WrappedDictionary<string>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);
            
            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual("world", deserializeDict["hello"]);
            Assert.AreEqual("bar", deserializeDict["foo"]);
            Assert.AreEqual("this is a \"quoted\" string", deserializeDict["quoted"]);
        }

        [Test]
        public void SerializeDeserialize_EnumDict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<Direction>
                { { "up", Direction.Up }, { "down", Direction.Down }, { "left", Direction.Left }, { "right", Direction.Right } };
            var tomlString = TomlSerializer.Serialize(serializedDict);
            
            var deserializeDict = new WrappedDictionary<Direction>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);
            
            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(Direction.Up, deserializeDict["up"]);
            Assert.AreEqual(Direction.Down, deserializeDict["down"]);
            Assert.AreEqual(Direction.Left, deserializeDict["left"]);
            Assert.AreEqual(Direction.Right, deserializeDict["right"]);
        }

        [Test]
        public void SerializeDeserialize_EnumFlagsDict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<StatusEffects>
            {
                { "poison", StatusEffects.Poison },
                { "blind", StatusEffects.Blind },
                { "immobile", StatusEffects.Frozen | StatusEffects.Sleep | StatusEffects.Stun },
                { "all", StatusEffects.All }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<StatusEffects>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(StatusEffects.Poison, deserializeDict["poison"]);
            Assert.AreEqual(StatusEffects.Blind, deserializeDict["blind"]);
            Assert.AreEqual(StatusEffects.Frozen | StatusEffects.Sleep | StatusEffects.Stun,
                deserializeDict["immobile"]);
            Assert.AreEqual(StatusEffects.All, deserializeDict["all"]);
        }

        [Test]
        public void SerializeDeserialize_Int8Dict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<sbyte>
            {
                { "min_value", sbyte.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", sbyte.MaxValue }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<sbyte>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(sbyte.MinValue, deserializeDict["min_value"]);
            Assert.AreEqual(-1, deserializeDict["minus_one"]);
            Assert.AreEqual(0, deserializeDict["zero"]);
            Assert.AreEqual(1, deserializeDict["plus_one"]);
            Assert.AreEqual(sbyte.MaxValue, deserializeDict["max_value"]);
        }
        
        [Test]
        public void SerializeDeserialize_Int16Dict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<short>
            {
                { "min_value", short.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", short.MaxValue }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<short>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(short.MinValue, deserializeDict["min_value"]);
            Assert.AreEqual(-1, deserializeDict["minus_one"]);
            Assert.AreEqual(0, deserializeDict["zero"]);
            Assert.AreEqual(1, deserializeDict["plus_one"]);
            Assert.AreEqual(short.MaxValue, deserializeDict["max_value"]);
        }
        
        [Test]
        public void SerializeDeserialize_Int32Dict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<int>
            {
                { "min_value", int.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", int.MaxValue }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<int>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(int.MinValue, deserializeDict["min_value"]);
            Assert.AreEqual(-1, deserializeDict["minus_one"]);
            Assert.AreEqual(0, deserializeDict["zero"]);
            Assert.AreEqual(1, deserializeDict["plus_one"]);
            Assert.AreEqual(int.MaxValue, deserializeDict["max_value"]);
        }
        
        [Test]
        public void SerializeDeserialize_Int64Dict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<long>
            {
                { "min_value", long.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", long.MaxValue }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<long>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(long.MinValue, deserializeDict["min_value"]);
            Assert.AreEqual(-1, deserializeDict["minus_one"]);
            Assert.AreEqual(0, deserializeDict["zero"]);
            Assert.AreEqual(1, deserializeDict["plus_one"]);
            Assert.AreEqual(long.MaxValue, deserializeDict["max_value"]);
        }
        
        [Test]
        public void SerializeDeserialize_UInt8Dict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<byte>
            {
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", byte.MaxValue }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<byte>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(0, deserializeDict["zero"]);
            Assert.AreEqual(1, deserializeDict["plus_one"]);
            Assert.AreEqual(byte.MaxValue, deserializeDict["max_value"]);
        }
        
        [Test]
        public void SerializeDeserialize_UInt16Dict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<ushort>
            {
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", ushort.MaxValue }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<ushort>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(0, deserializeDict["zero"]);
            Assert.AreEqual(1, deserializeDict["plus_one"]);
            Assert.AreEqual(ushort.MaxValue, deserializeDict["max_value"]);
        }
        
        [Test]
        public void SerializeDeserialize_UInt32Dict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<uint>
            {
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", uint.MaxValue }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<uint>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(0, deserializeDict["zero"]);
            Assert.AreEqual(1, deserializeDict["plus_one"]);
            Assert.AreEqual(uint.MaxValue, deserializeDict["max_value"]);
        }
        
        [Test]
        public void SerializeDeserialize_DateTimeDict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<DateTime>
            {
                { "min_value", DateTime.MinValue.Date },
                { "some_date", new DateTime(1979, 5, 27) },
                { "max_value", DateTime.MaxValue.Date }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<DateTime>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual(serializedDict["min_value"], deserializeDict["min_value"]);
            Assert.AreEqual(serializedDict["some_date"], deserializeDict["some_date"]);
            Assert.AreEqual(serializedDict["max_value"], deserializeDict["max_value"]);
        }

        [Test]
        public void SerializeDeserialize_MixedDict_ShouldBeEqual()
        {
            var serializedDict = new WrappedDictionary<object>
            {
                { "name", "John" },
                { "level", 42 },
                { "weight", 3.14 },
                { "stats", new long[] { 12, 45, 67, 82, 21 } },
                { "is_banned", true },
                { "createdAt", new DateTime(1979, 5, 27) }
            };
            var tomlString = TomlSerializer.Serialize(serializedDict);

            var deserializeDict = new WrappedDictionary<object>();
            TomlSerializer.DeserializeInto(tomlString, deserializeDict);

            Assert.IsFalse(deserializeDict.IsEmpty, "Dictionary should not be empty");
            Assert.AreEqual("John", deserializeDict["name"]);
            Assert.AreEqual(42, deserializeDict["level"]);
            Assert.AreEqual(3.14, deserializeDict["weight"]);
            Assert.AreEqual(true, deserializeDict["is_banned"]);
            Assert.AreEqual(new DateTime(1979, 5, 27), deserializeDict["createdAt"]);

            var serializedStats = serializedDict["stats"] as long[];
            var deserializedStats = (deserializeDict["stats"] as List<object>).Cast<long>();
            
            Assert.IsNotNull(serializedStats);
            Assert.IsTrue(deserializedStats.SequenceEqual(serializedStats));
        }
    }
}
