using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullKeyValue_ShouldSetNull()
        {
            var existingValue = new WrappedValue<string>("Hello World!");

            const string toml = "value = null\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, null);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Deserialize_BoolKeyValue_ShouldSetValue(bool boolValue)
        {
            var existingValue = new WrappedValue<bool>(!boolValue);

            var toml = $"value = {boolValue.ToString().ToLowerInvariant()}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, boolValue);
        }

        [TestCase('A')]
        [TestCase('0')]
        [TestCase('$')]
        public void Deserialize_CharKeyValue_ShouldSetValue(char charValue)
        {
            var existingValue = new WrappedValue<char>('?');

            var toml = $"value = \"{charValue}\"\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, charValue);
        }

        [TestCase("Hello World!")]
        [TestCase("The quick brown fox jumps over the lazy dog.")]
        public void Deserialize_StringKeyValue_ShouldSetValue(string stringValue)
        {
            var existingValue = new WrappedValue<string>("???");

            var toml = $"value = \"{stringValue}\"\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, stringValue);
        }

        [TestCase(MockEnum.North)]
        [TestCase(MockEnum.South)]
        [TestCase(MockEnum.East)]
        [TestCase(MockEnum.West)]
        public void Deserialize_EnumKeyValue_ShouldSetValue(MockEnum enumValue)
        {
            var existingValue = new WrappedValue<MockEnum>(MockEnum.None);

            var toml = $"value = \"{enumValue:F}\"\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, enumValue);
        }

        [TestCase(MockFlags.Available)]
        [TestCase(MockFlags.InProgress)]
        [TestCase(MockFlags.Completed)]
        [TestCase(MockFlags.Cancelled)]
        [TestCase(MockFlags.Available | MockFlags.InProgress)]
        [TestCase(MockFlags.All)]
        public void Deserialize_EnumKeyValue_ShouldSetValue(MockFlags flagsValue)
        {
            var existingValue = new WrappedValue<MockEnum>(MockEnum.None);

            var toml = $"value = \"{flagsValue:F}\"\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, flagsValue);
        }

        [TestCase(sbyte.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(sbyte.MaxValue)]
        public void Deserialize_Int8KeyValue_ShouldSetValue(sbyte int8Value)
        {
            var existingValue = new WrappedValue<sbyte>(42);

            var toml = $"value = {int8Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, int8Value);
        }

        [TestCase(short.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(short.MaxValue)]
        public void Deserialize_Int16KeyValue_ShouldSetValue(short int16Value)
        {
            var existingValue = new WrappedValue<short>(42);

            var toml = $"value = {int16Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, int16Value);
        }

        [TestCase(int.MinValue)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(int.MaxValue)]
        public void Deserialize_Int32KeyValue_ShouldSetValue(int int32Value)
        {
            var existingValue = new WrappedValue<int>(42);

            var toml = $"value = {int32Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, int32Value);
        }

        [TestCase(long.MinValue)]
        [TestCase(-1L)]
        [TestCase(0)]
        [TestCase(1L)]
        [TestCase(long.MaxValue)]
        public void Deserialize_Int64KeyValue_ShouldSetValue(long int64Value)
        {
            var existingValue = new WrappedValue<int>(42);

            var toml = $"value = {int64Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, int64Value);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(byte.MaxValue)]
        public void Deserialize_UInt8KeyValue_ShouldSetValue(byte uint8Value)
        {
            var existingValue = new WrappedValue<byte>(42);

            var toml = $"value = {uint8Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, uint8Value);
        }

        [TestCase((ushort)0)]
        [TestCase((ushort)1)]
        [TestCase(ushort.MaxValue)]
        public void Deserialize_UInt16KeyValue_ShouldSetValue(ushort uint16Value)
        {
            var existingValue = new WrappedValue<short>(42);

            var toml = $"value = {uint16Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, uint16Value);
        }


        [TestCase(0u)]
        [TestCase(1u)]
        [TestCase(uint.MaxValue)]
        public void Deserialize_UInt32KeyValue_ShouldSetValue(uint uint32Value)
        {
            var existingValue = new WrappedValue<int>(42);

            var toml = $"value = {uint32Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(existingValue.Value, uint32Value);
        }

        [Test]
        public void Deserialize_DateTimeKeyValue_ShouldSetValue()
        {
            var existingValue = new WrappedValue<DateTime>(DateTime.MinValue);

            var toml = $"value = 1979-05-27 07:32:01.999Z\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            var expectedDate = new DateTime(1979, 5, 27, 7, 32, 1, 999, DateTimeKind.Utc);
            Assert.AreEqual(existingValue.Value, expectedDate);
        }
    }
}
