using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;
using UnderLogic.Serialization.Toml.Tests.Fixtures.Builders;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(-42)]
        [TestCase(0)]
        [TestCase(42)]
        public void Serialize_Int8Value_ShouldBeLiteral(sbyte integerValue)
        {
            var value = new SerializableValue<sbyte>(integerValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", integerValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [TestCase(-42)]
        [TestCase(0)]
        [TestCase(42)]
        public void Serialize_Int16Value_ShouldBeLiteral(short integerValue)
        {
            var value = new SerializableValue<short>(integerValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", integerValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [TestCase(-42)]
        [TestCase(0)]
        [TestCase(42)]
        public void Serialize_Int32Value_ShouldBeLiteral(int integerValue)
        {
            var value = new SerializableValue<int>(integerValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", integerValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [TestCase(-42L)]
        [TestCase(0L)]
        [TestCase(42L)]
        public void Serialize_Int64Value_ShouldBeLiteral(long integerValue)
        {
            var value = new SerializableValue<long>(integerValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", integerValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [TestCase(0)]
        [TestCase(42)]
        public void Serialize_UInt8Value_ShouldBeLiteral(byte integerValue)
        {
            var value = new SerializableValue<byte>(integerValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", integerValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [TestCase((ushort)0)]
        [TestCase((ushort)42)]
        public void Serialize_UInt16Value_ShouldBeLiteral(ushort integerValue)
        {
            var value = new SerializableValue<ushort>(integerValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", integerValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [TestCase((uint)0)]
        [TestCase((uint)42)]
        public void Serialize_UInt32Value_ShouldBeLiteral(uint integerValue)
        {
            var value = new SerializableValue<uint>(integerValue);
            var toml = TomlSerializer.Serialize(value);

            var expectedToml = new TomlStringBuilder().AppendKeyValue("value", integerValue).AppendLine().ToString();
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
