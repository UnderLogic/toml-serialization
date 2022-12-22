using System;
using System.Globalization;
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

            Assert.IsNull(existingValue.Value, "Value should be null");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void Deserialize_BoolKeyValue_ShouldSetValue(bool boolValue)
        {
            var existingValue = new WrappedValue<bool>(!boolValue);

            var toml = $"value = {boolValue.ToString().ToLowerInvariant()}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(boolValue, existingValue.Value);
        }

        [TestCase('A')]
        [TestCase('0')]
        [TestCase('$')]
        public void Deserialize_CharKeyValue_ShouldSetValue(char charValue)
        {
            var existingValue = new WrappedValue<char>('?');

            var toml = $"value = \"{charValue}\"\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(charValue, existingValue.Value);
        }

        [TestCase("Hello World!")]
        [TestCase("The quick brown fox jumps over the lazy dog.")]
        public void Deserialize_StringKeyValue_ShouldSetValue(string stringValue)
        {
            var existingValue = new WrappedValue<string>("???");

            var toml = $"value = \"{stringValue}\"\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(stringValue, existingValue.Value);
        }

        [TestCase("I'm a string with a \"quote\" in it.")]
        [TestCase("C:\\Windows\\System32")]
        [TestCase("This is a quoted path: \"C:\\Windows\\System32\"")]
        public void Deserialize_StringKeyValue_ShouldUnescapeValue(string stringValue)
        {
            var escapedValue = stringValue.Replace("\"", "\\\"").Replace("\\", "\\\\");
            var existingValue = new WrappedValue<string>("???");

            var toml = $"value = \"{escapedValue}\"\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(stringValue, existingValue.Value);
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

            Assert.AreEqual(enumValue, existingValue.Value);
        }

        [TestCase(MockFlags.Available)]
        [TestCase(MockFlags.InProgress)]
        [TestCase(MockFlags.Completed)]
        [TestCase(MockFlags.Cancelled)]
        [TestCase(MockFlags.Available | MockFlags.InProgress)]
        [TestCase(MockFlags.All)]
        public void Deserialize_EnumKeyValue_ShouldSetValue(MockFlags flagsValue)
        {
            var existingValue = new WrappedValue<MockFlags>(MockFlags.None);

            var toml = $"value = \"{flagsValue:F}\"\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(flagsValue, existingValue.Value);
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

            Assert.AreEqual(int8Value, existingValue.Value);
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

            Assert.AreEqual(int16Value, existingValue.Value);
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

            Assert.AreEqual(int32Value, existingValue.Value);
        }

        [TestCase(long.MinValue)]
        [TestCase(-1L)]
        [TestCase(0)]
        [TestCase(1L)]
        [TestCase(long.MaxValue)]
        public void Deserialize_Int64KeyValue_ShouldSetValue(long int64Value)
        {
            var existingValue = new WrappedValue<long>(42);

            var toml = $"value = {int64Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(int64Value, existingValue.Value);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(byte.MaxValue)]
        public void Deserialize_UInt8KeyValue_ShouldSetValue(byte uint8Value)
        {
            var existingValue = new WrappedValue<byte>(42);

            var toml = $"value = {uint8Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(uint8Value, existingValue.Value);
        }

        [TestCase((ushort)0)]
        [TestCase((ushort)1)]
        [TestCase(ushort.MaxValue)]
        public void Deserialize_UInt16KeyValue_ShouldSetValue(ushort uint16Value)
        {
            var existingValue = new WrappedValue<ushort>(42);

            var toml = $"value = {uint16Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(uint16Value, existingValue.Value);
        }


        [TestCase(0u)]
        [TestCase(1u)]
        [TestCase(uint.MaxValue)]
        public void Deserialize_UInt32KeyValue_ShouldSetValue(uint uint32Value)
        {
            var existingValue = new WrappedValue<uint>(42);

            var toml = $"value = {uint32Value}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            Assert.AreEqual(uint32Value, existingValue.Value);
        }

        [TestCase("1979-05-27T07:32:00Z")]
        [TestCase("1979-05-27T00:32:00-07:00")]
        [TestCase("1979-05-27T00:32:00.999999-07:00")]
        [TestCase("1979-05-27 07:32:00Z")]
        [TestCase("1979-05-27 00:32:00-07:00")]
        [TestCase("1979-05-27 00:32:00.999999-07:00")]
        [TestCase("1979-05-27T07:32:00")]
        [TestCase("1979-05-27 07:32:00")]
        [TestCase("1979-05-27")]
        [TestCase("07:32:00")]
        [TestCase("00:32:00.999")]
        public void Deserialize_DateTimeKeyValue_ShouldSetValue(string dateString)
        {
            var existingValue = new WrappedValue<DateTime>(DateTime.MinValue);

            var toml = $"value = {dateString}\n";
            TomlSerializer.DeserializeInto(toml, existingValue);

            var expectedDate = DateTime.Parse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            Assert.AreEqual(expectedDate, existingValue.Value);
        }
    }
}
