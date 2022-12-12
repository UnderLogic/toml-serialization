using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [TestCase(new[] { true, false })]
        public void Serialize_Array_Bool(IEnumerable<bool> collection)
        {
            var wrappedArray = new WrappedEnumerable<bool>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString =
                wrappedArray.ToTomlStringArray("values", value => value.ToString().ToLowerInvariant());
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [TestCase(new[] { 'A', 'Z', '0', '9', '_' })]
        public void Serialize_Array_Char(IEnumerable<char> collection)
        {
            var wrappedArray = new WrappedEnumerable<char>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_Array_String(string stringValue)
        {
            var collection = new[] { stringValue, stringValue, stringValue };
            
            var wrappedArray = new WrappedEnumerable<string>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [TestCase(new[] { sbyte.MinValue, sbyte.MaxValue })]
        public void Serialize_Array_Int8(IEnumerable<sbyte> collection)
        {
            var wrappedArray = new WrappedEnumerable<sbyte>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(new[] { short.MinValue, short.MaxValue })]
        public void Serialize_Array_Int16(IEnumerable<short> collection)
        {
            var wrappedArray = new WrappedEnumerable<short>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(new[] { int.MinValue, int.MaxValue })]
        public void Serialize_Array_Int32(IEnumerable<int> collection)
        {
            var wrappedArray = new WrappedEnumerable<int>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(new[] { long.MinValue, long.MaxValue })]
        public void Serialize_Array_Int64(IEnumerable<long> collection)
        {
            var wrappedArray = new WrappedEnumerable<long>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(new[] { byte.MinValue, byte.MaxValue })]
        public void Serialize_Array_UInt8(IEnumerable<byte> collection)
        {
            var wrappedArray = new WrappedEnumerable<byte>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(new[] { ushort.MinValue, ushort.MaxValue })]
        public void Serialize_Array_UInt16(IEnumerable<ushort> collection)
        {
            var wrappedArray = new WrappedEnumerable<ushort>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [TestCase(new[] { uint.MinValue, uint.MaxValue })]
        public void Serialize_Array_UInt32(IEnumerable<uint> collection)
        {
            var wrappedArray = new WrappedEnumerable<uint>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [TestCase(new[] { float.MinValue, float.MaxValue })]
        public void Serialize_Array_Float(IEnumerable<float> collection)
        {
            var wrappedArray = new WrappedEnumerable<float>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => ((double)value).ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [TestCase(new[] { double.MinValue, double.MaxValue })]
        public void Serialize_Array_Double(IEnumerable<double> collection)
        {
            var wrappedArray = new WrappedEnumerable<double>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Array_DateTime()
        {
            var collection = new[] { DateTime.MinValue, DateTime.Now, DateTime.MaxValue };
            
            var wrappedArray = new WrappedEnumerable<DateTime>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedArray);

            var expectedTomlString = wrappedArray.ToTomlStringArray("values", value => $"{value:yyyy-MM-dd HH:mm:ss.fffZ}");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
    }
}
