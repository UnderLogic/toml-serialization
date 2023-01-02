using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_FloatPositiveInfinity_ShouldSerializeAsInfinity()
        {
            var wrappedValue = new WrappedValue<float>(float.PositiveInfinity);
           
            var actual = TomlSerializer.Serialize(wrappedValue);
            Assert.AreEqual("value = +inf\n", actual);
        }
        
        [Test]
        public void Serialize_FloatNegativeInfinity_ShouldSerializeAsInfinity()
        {
            var wrappedValue = new WrappedValue<float>(float.NegativeInfinity);
           
            var actual = TomlSerializer.Serialize(wrappedValue);
            Assert.AreEqual("value = -inf\n", actual);
        }
        
        [Test]
        public void Serialize_FloatNaN_ShouldSerializeAsNaN()
        {
            var wrappedValue = new WrappedValue<float>(float.NaN);
           
            var actual = TomlSerializer.Serialize(wrappedValue);
            Assert.AreEqual("value = nan\n", actual);
        }
        
        [Test]
        public void Serialize_DoublePositiveInfinity_ShouldSerializeAsInfinity()
        {
            var wrappedValue = new WrappedValue<double>(double.PositiveInfinity);
           
            var actual = TomlSerializer.Serialize(wrappedValue);
            Assert.AreEqual("value = +inf\n", actual);
        }
        
        [Test]
        public void Serialize_DoubleNegativeInfinity_ShouldSerializeAsInfinity()
        {
            var wrappedValue = new WrappedValue<double>(double.NegativeInfinity);
           
            var actual = TomlSerializer.Serialize(wrappedValue);
            Assert.AreEqual("value = -inf\n", actual);
        }
        
        [Test]
        public void Serialize_DoubleNaN_ShouldSerializeAsNaN()
        {
            var wrappedValue = new WrappedValue<double>(double.NaN);
           
            var actual = TomlSerializer.Serialize(wrappedValue);
            Assert.AreEqual("value = nan\n", actual);
        }
    }
}
