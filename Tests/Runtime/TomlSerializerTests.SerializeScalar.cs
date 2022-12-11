using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Serialize_Scalar_Bool(bool value)
        {
            var wrapped = new WrappedValue<bool>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);

            var boolString = value.ToString().ToLowerInvariant();
            Assert.AreEqual($"value = {boolString}", tomlString.Trim());
        }
        
        [TestCase('A')]
        [TestCase('Z')]
        [TestCase('0')]
        [TestCase('9')]
        [TestCase('_')]
        public void Serialize_Scalar_Char(char value)
        {
            var wrapped = new WrappedValue<char>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = \"{value}\"", tomlString.Trim());
        }
        
        [TestCase(sbyte.MinValue)]
        [TestCase(sbyte.MaxValue)]
        public void Serialize_Scalar_Int8(sbyte value)
        {
            var wrapped = new WrappedValue<sbyte>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = {value}", tomlString.Trim());
        }
        
        [TestCase(short.MinValue)]
        [TestCase(short.MaxValue)]
        public void Serialize_Scalar_Int16(short value)
        {
            var wrapped = new WrappedValue<short>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = {value}", tomlString.Trim());
        }
        
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void Serialize_Scalar_Int32(int value)
        {
            var wrapped = new WrappedValue<int>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = {value}", tomlString.Trim());
        }
        
        [TestCase(long.MinValue)]
        [TestCase(long.MaxValue)]
        public void Serialize_Scalar_Int64(long value)
        {
            var wrapped = new WrappedValue<long>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = {value}", tomlString.Trim());
        }
        
        [TestCase(byte.MinValue)]
        [TestCase(byte.MaxValue)]
        public void Serialize_Scalar_UInt8(byte value)
        {
            var wrapped = new WrappedValue<byte>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = {value}", tomlString.Trim());
        }
        
        [TestCase(ushort.MinValue)]
        [TestCase(ushort.MaxValue)]
        public void Serialize_Scalar_UInt16(ushort value)
        {
            var wrapped = new WrappedValue<ushort>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = {value}", tomlString.Trim());
        }
        
        [TestCase(uint.MinValue)]
        [TestCase(uint.MaxValue)]
        public void Serialize_Scalar_UInt32(uint value)
        {
            var wrapped = new WrappedValue<uint>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = {value}", tomlString.Trim());
        }

        [TestCase(float.MinValue)]
        [TestCase(float.MaxValue)]
        public void Serialize_Scalar_Float(float value)
        {
            var wrapped = new WrappedValue<float>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = {(double)value}", tomlString.Trim());
        }
        
        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        public void Serialize_Scalar_Double(double value)
        {
            var wrapped = new WrappedValue<double>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = {value}", tomlString.Trim());
        }

        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_Scalar_String(string value)
        {
            var wrapped = new WrappedValue<string>(value);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            Assert.AreEqual($"value = \"{value}\"", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_Scalar_DateTime()
        {
            var now = DateTime.Now;
            var wrapped = new WrappedValue<DateTime>(now);
            var tomlString = TomlSerializer.Serialize(wrapped);
            
            var isoDate = $"{now:yyyy-MM-dd HH:mm:ss.fffZ}";
            Assert.AreEqual($"value = {isoDate}", tomlString.Trim());
        }
    }
}
