using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_Value_Null()
        {
            var wrappedNull = new WrappedValue<string>(null);
            var tomlString = TomlSerializer.Serialize(wrappedNull);

            var expectedTomlString = wrappedNull.ToTomlStringKeyValuePair("value", value => "null");
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Serialize_Value_Bool(bool testValue)
        {
            var wrapped = new WrappedValue<bool>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);

            var expectedTomlString =
                wrapped.ToTomlStringKeyValuePair("value", value => value.ToString().ToLowerInvariant());
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [TestCase('A')]
        [TestCase('Z')]
        [TestCase('0')]
        [TestCase('9')]
        [TestCase('_')]
        public void Serialize_Value_Char(char testValue)
        {
            var wrapped = new WrappedValue<char>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_Value_String(string testValue)
        {
            var wrapped = new WrappedValue<string>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [TestCase(sbyte.MinValue)]
        [TestCase(sbyte.MaxValue)]
        public void Serialize_Value_Int8(sbyte testValue)
        {
            var wrapped = new WrappedValue<sbyte>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);

            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(short.MinValue)]
        [TestCase(short.MaxValue)]
        public void Serialize_Value_Int16(short testValue)
        {
            var wrapped = new WrappedValue<short>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void Serialize_Value_Int32(int testValue)
        {
            var wrapped = new WrappedValue<int>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(long.MinValue)]
        [TestCase(long.MaxValue)]
        public void Serialize_Value_Int64(long testValue)
        {
            var wrapped = new WrappedValue<long>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(byte.MinValue)]
        [TestCase(byte.MaxValue)]
        public void Serialize_Value_UInt8(byte testValue)
        {
            var wrapped = new WrappedValue<byte>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(ushort.MinValue)]
        [TestCase(ushort.MaxValue)]
        public void Serialize_Value_UInt16(ushort testValue)
        {
            var wrapped = new WrappedValue<ushort>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(uint.MinValue)]
        [TestCase(uint.MaxValue)]
        public void Serialize_Value_UInt32(uint testValue)
        {
            var wrapped = new WrappedValue<uint>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [TestCase(float.MinValue)]
        [TestCase(float.MaxValue)]
        public void Serialize_Value_Float(float testValue)
        {
            var wrapped = new WrappedValue<float>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);

            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => ((double)value).ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        public void Serialize_Value_Double(double testValue)
        {
            var wrapped = new WrappedValue<double>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Value_DateTime()
        {
            var now = DateTime.Now;
            var wrapped = new WrappedValue<DateTime>(now);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => $"{value:yyyy-MM-dd HH:mm:ss.fffZ}");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(MockEnum.North)]
        [TestCase(MockEnum.South)]
        [TestCase(MockEnum.East)]
        [TestCase(MockEnum.West)]
        public void Serialize_Value_Enum(MockEnum testValue)
        {
            var wrapped = new WrappedValue<MockEnum>(testValue);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(MockStateFlags.None)]
        [TestCase(MockStateFlags.Pending | MockStateFlags.InProgress)]
        [TestCase(MockStateFlags.All)]
        public void Serialize_Value_Enum_Flags(MockStateFlags testFlags)
        {
            var wrapped = new WrappedValue<MockStateFlags>(testFlags);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var expectedTomlString = wrapped.ToTomlStringKeyValuePair("value", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
    }
}
