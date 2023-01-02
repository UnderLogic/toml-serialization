using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase("inf")]
        [TestCase("+inf")]
        public void Deserialize_FloatPositiveInfinity_ShouldSetToPositiveInfinity(string valueString)
        {
            var wrappedValue = new WrappedValue<float>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);

            Assert.IsTrue(float.IsPositiveInfinity(wrappedValue.Value), "Value should be positive infinity");
        }
        
        [TestCase("-inf")]
        public void Deserialize_FloatNegativeInfinity_ShouldSetToNegativeInfinity(string valueString)
        {
            var wrappedValue = new WrappedValue<float>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);

            Assert.IsTrue(float.IsNegativeInfinity(wrappedValue.Value), "Value should be negative infinity");
        }
        
        [TestCase("nan")]
        [TestCase("+nan")]
        [TestCase("-nan")]
        public void Deserialize_FloatNaN_ShouldSetToNaN(string valueString)
        {
            var wrappedValue = new WrappedValue<float>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);

            Assert.IsTrue(float.IsNaN(wrappedValue.Value), "Value should be not a number (NaN)");
        }
        
        [TestCase("inf")]
        [TestCase("+inf")]
        public void Deserialize_DoublePositiveInfinity_ShouldSetToPositiveInfinity(string valueString)
        {
            var wrappedValue = new WrappedValue<double>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);

            Assert.IsTrue(double.IsPositiveInfinity(wrappedValue.Value), "Value should be positive infinity");
        }
        
        [TestCase("-inf")]
        public void Deserialize_DoubleNegativeInfinity_ShouldSetToNegativeInfinity(string valueString)
        {
            var wrappedValue = new WrappedValue<double>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);

            Assert.IsTrue(double.IsNegativeInfinity(wrappedValue.Value), "Value should be negative infinity");
        }
        
        [TestCase("nan")]
        [TestCase("+nan")]
        [TestCase("-nan")]
        public void Deserialize_DoubleNaN_ShouldSetToNaN(string valueString)
        {
            var wrappedValue = new WrappedValue<double>();
            TomlSerializer.DeserializeInto($"value = {valueString}\n", wrappedValue);

            Assert.IsTrue(double.IsNaN(wrappedValue.Value), "Value should be not a number (NaN)");
        }
    }
}
