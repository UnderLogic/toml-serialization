using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullKeyValue_ShouldSerializeNull()
        {
            var wrappedValue = new WrappedValue<string>(null);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual("value = null\n", toml);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Serialize_BoolKeyValue_ShouldSerializeLowerCase(bool boolValue)
        {
            var wrappedValue = new WrappedValue<bool>(boolValue);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = {boolValue.ToString().ToLowerInvariant()}\n", toml);
        }

        [TestCase('A')]
        [TestCase('0')]
        [TestCase('$')]
        public void Serialize_CharKeyValue_ShouldSerializeQuoted(char charValue)
        {
            var wrappedValue = new WrappedValue<char>(charValue);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = \"{charValue}\"\n", toml);
        }

        [TestCase("Hello World!")]
        [TestCase("The quick brown fox jumps over the lazy dog.")]
        public void Serialize_StringKeyValue_ShouldSerializeQuoted(string stringValue)
        {
            var wrappedValue = new WrappedValue<string>(stringValue);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = \"{stringValue}\"\n", toml);
        }
        
        [TestCase("I'm a string with a \"quote\" in it.")]
        [TestCase("C:\\Windows\\System32")]
        [TestCase("This is a quoted path: \"C:\\Windows\\System32\"")]
        public void Serialize_StringKeyValue_ShouldSerializeEscaped(string stringValue)
        {
            var wrappedValue = new WrappedValue<string>(stringValue);
            var toml = TomlSerializer.Serialize(wrappedValue);

            var escapedValue = stringValue.Replace("\"", "\\\"");
            Assert.AreEqual($"value = \"{escapedValue}\"\n", toml);
        }

        [TestCase(Direction.None)]
        [TestCase(Direction.Up)]
        [TestCase(Direction.Down)]
        [TestCase(Direction.Left)]
        [TestCase(Direction.Right)]
        public void Serialize_EnumKeyValue_ShouldSerializeQuoted(Direction enumValue)
        {
            var wrappedValue = new WrappedValue<Direction>(enumValue);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = \"{enumValue:F}\"\n", toml);
        }

        [TestCase(StatusEffects.None)]
        [TestCase(StatusEffects.Poison)]
        [TestCase(StatusEffects.Blind)]
        [TestCase(StatusEffects.Frozen | StatusEffects.Sleep)]
        [TestCase(StatusEffects.All)]
        public void Serialize_EnumFlagsKeyValue_ShouldSerializeQuoted(StatusEffects flagsValue)
        {
            var wrappedValue = new WrappedValue<StatusEffects>(flagsValue);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = \"{flagsValue:F}\"\n", toml);
        }

        [TestCase(sbyte.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(sbyte.MaxValue)]
        public void Serialize_Int8KeyValue_ShouldSerializeLiteral(sbyte int8Value)
        {
            var wrappedValue = new WrappedValue<sbyte>(int8Value);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = {int8Value}\n", toml);
        }

        [TestCase(short.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(short.MaxValue)]
        public void Serialize_Int16KeyValue_ShouldSerializeLiteral(short int16Value)
        {
            var wrappedValue = new WrappedValue<short>(int16Value);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = {int16Value}\n", toml);
        }

        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(int.MaxValue)]
        public void Serialize_Int32KeyValue_ShouldSerializeLiteral(int int32Value)
        {
            var wrappedValue = new WrappedValue<int>(int32Value);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = {int32Value}\n", toml);
        }

        [TestCase(long.MinValue)]
        [TestCase(-1L)]
        [TestCase(0L)]
        [TestCase(1L)]
        [TestCase(long.MaxValue)]
        public void Serialize_Int64KeyValue_ShouldSerializeLiteral(long int64Value)
        {
            var wrappedValue = new WrappedValue<long>(int64Value);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = {int64Value}\n", toml);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(byte.MaxValue)]
        public void Serialize_UInt8KeyValue_ShouldSerializeLiteral(byte uint8Value)
        {
            var wrappedValue = new WrappedValue<byte>(uint8Value);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = {uint8Value}\n", toml);
        }

        [TestCase((ushort)0)]
        [TestCase((ushort)1)]
        [TestCase(ushort.MaxValue)]
        public void Serialize_UInt16KeyValue_ShouldSerializeLiteral(ushort uint16Value)
        {
            var wrappedValue = new WrappedValue<ushort>(uint16Value);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = {uint16Value}\n", toml);
        }

        [TestCase(0u)]
        [TestCase(1u)]
        [TestCase(uint.MaxValue)]
        public void Serialize_UInt32KeyValue_ShouldSerializeLiteral(uint uint32Value)
        {
            var wrappedValue = new WrappedValue<uint>(uint32Value);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = {uint32Value}\n", toml);
        }

        [Test]
        public void Serialize_DateTimeKeyValue_ShouldSerializeIsoFormat()
        {
            var now = DateTime.UtcNow;
            var wrappedValue = new WrappedValue<DateTime>(now);
            var toml = TomlSerializer.Serialize(wrappedValue);

            Assert.AreEqual($"value = {now:yyyy-MM-dd HH:mm:ss.fffZ}\n", toml);
        }
    }
}
