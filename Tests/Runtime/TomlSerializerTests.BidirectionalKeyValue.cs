using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_NullValue_ShouldBeEqual()
        {
            var serializedValue = new WrappedValue<string>(null);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.IsNull(deserializedValue.Value);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SerializeDeserialize_BoolValue_ShouldBeEqual(bool boolValue)
        {
            var serializedValue = new WrappedValue<bool>(boolValue);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<bool>(!boolValue);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase('A')]
        [TestCase('0')]
        [TestCase('!')]
        public void SerializeDeserialize_CharValue_ShouldBeEqual(char charValue)
        {
            var serializedValue = new WrappedValue<char>(charValue);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<char>(' ');
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase("Hello, world!")]
        [TestCase("This is a \"quoted\" string")]
        [TestCase("C:\\Windows\\System32")]
        [TestCase("\\Network\\Share\\Drive")]
        public void SerializeDeserialize_StringValue_ShouldBeEqual(string stringValue)
        {
            var serializedValue = new WrappedValue<string>(stringValue);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<string>(string.Empty);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase("This is a quest.\nYou should complete it.\n")]
        [TestCase("  \tThis is a quest.\n    You should complete it.\n\nFor some loot.\n  ")]
        [TestCase("Even a quest with a \"quote\" in it.\nThis is supported by TOML.\nAlso 'single quotes'.\n")]
        public void SerializeDeserialize_MultilineString_ShouldSetEqual(string expectedValue)
        {
            var serializedQuest = new Quest
            {
                Description = expectedValue
            };

            var tomlString = TomlSerializer.Serialize(serializedQuest);

            var deserializedQuest = new Quest();
            TomlSerializer.DeserializeInto(tomlString, deserializedQuest);

            Assert.AreEqual(serializedQuest.Description, deserializedQuest.Description);
        }

        [TestCase(@"C:\Scripts\Quests\GatherWood.lua")]
        [TestCase(@"C:\John's Scripts\Quests\GatherWood.lua")]
        [TestCase(@"\\Network\\Share\\Scripts\\Quests\\GatherWood.lua")]
        [TestCase(@"You can use ""quotes"" in paths.")]
        public void SerializeDeserialize_LiteralString_ShouldSetEqual(string expectedValue)
        {
            var serializedQuest = new Quest
            {
                ScriptPath = expectedValue
            };

            var tomlString = TomlSerializer.Serialize(serializedQuest);

            var deserializedQuest = new Quest();
            TomlSerializer.DeserializeInto(tomlString, deserializedQuest);

            Assert.AreEqual(serializedQuest.Summary, deserializedQuest.Summary);
        }

        [TestCase("This is a quest.\nYou should complete it.\n")]
        [TestCase("  \tThis is a quest.\n    You should complete it.\n\nFor some loot.\n  ")]
        [TestCase("Even a quest with a \"quote\" in it.\nThis is supported by TOML.\nAlso 'single quotes'.\n")]
        public void SerializeDeserialize_MultilineLiteralString_ShouldSetEqual(string expectedValue)
        {
            var serializedQuest = new Quest
            {
                Summary = expectedValue
            };

            var tomlString = TomlSerializer.Serialize(serializedQuest);

            var deserializedQuest = new Quest();
            TomlSerializer.DeserializeInto(tomlString, deserializedQuest);

            Assert.AreEqual(serializedQuest.Summary, deserializedQuest.Summary);
        }
        
        [TestCase(Direction.Up)]
        [TestCase(Direction.Down)]
        [TestCase(Direction.Left)]
        [TestCase(Direction.Right)]
        public void SerializeDeserialize_EnumValue_ShouldBeEqual(Direction enumValue)
        {
            var serializedValue = new WrappedValue<Direction>(enumValue);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<Direction>(Direction.None);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase(StatusEffects.Poison)]
        [TestCase(StatusEffects.Blind)]
        [TestCase(StatusEffects.Frozen | StatusEffects.Sleep | StatusEffects.Stun)]
        [TestCase(StatusEffects.All)]
        public void SerializeDeserialize_EnumFlagsValue_ShouldBeEqual(StatusEffects flagsValue)
        {
            var serializedValue = new WrappedValue<StatusEffects>(flagsValue);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<StatusEffects>(StatusEffects.All);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase(sbyte.MinValue)]
        [TestCase(42)]
        [TestCase(sbyte.MaxValue)]
        public void SerializeDeserialize_Int8Value_ShouldBeEqual(sbyte int8Value)
        {
            var serializedValue = new WrappedValue<sbyte>(int8Value);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<sbyte>(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase(short.MinValue)]
        [TestCase((short)42)]
        [TestCase(short.MaxValue)]
        public void SerializeDeserialize_Int16Value_ShouldBeEqual(short int16Value)
        {
            var serializedValue = new WrappedValue<short>(int16Value);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<short>(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase(int.MinValue)]
        [TestCase(42)]
        [TestCase(int.MaxValue)]
        public void SerializeDeserialize_Int32Value_ShouldBeEqual(int int32Value)
        {
            var serializedValue = new WrappedValue<int>(int32Value);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<int>(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase(long.MinValue)]
        [TestCase(42L)]
        [TestCase(long.MaxValue)]
        public void SerializeDeserialize_Int64Value_ShouldBeEqual(long int64Value)
        {
            var serializedValue = new WrappedValue<long>(int64Value);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<long>(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase((byte)42)]
        [TestCase(byte.MaxValue)]
        public void SerializeDeserialize_UInt8Value_ShouldBeEqual(byte uint8Value)
        {
            var serializedValue = new WrappedValue<byte>(uint8Value);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<byte>(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase((ushort)42)]
        [TestCase(ushort.MaxValue)]
        public void SerializeDeserialize_UInt16Value_ShouldBeEqual(ushort uint16Value)
        {
            var serializedValue = new WrappedValue<ushort>(uint16Value);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<ushort>(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [TestCase(42u)]
        [TestCase(uint.MaxValue)]
        public void SerializeDeserialize_UInt32Value_ShouldBeEqual(uint uint32Value)
        {
            var serializedValue = new WrappedValue<uint>(uint32Value);
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<uint>(0);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
        
        [Test]
        public void SerializeDeserialize_DateTimeValue_ShouldBeEqual()
        {
            var serializedValue = new WrappedValue<DateTime>(new DateTime(1979, 5, 27));
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedValue<DateTime>(DateTime.MinValue);
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }
    }
}
