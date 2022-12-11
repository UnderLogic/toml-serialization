using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        private const string ArrayDelimiter = ", ";

        [TestCase(new[] { true, false })]
        public void Serialize_ScalarArray_Bool(IEnumerable<bool> collection)
        {
            var wrappedCollection = new WrappedEnumerable<bool>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}".ToLowerInvariant()));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { 'A', 'Z', '0', '9', '_' })]
        public void Serialize_ScalarArray_Char(IEnumerable<char> collection)
        {
            var wrappedCollection = new WrappedEnumerable<char>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"'{value}'"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { sbyte.MinValue, sbyte.MaxValue })]
        public void Serialize_ScalarArray_Int8(IEnumerable<sbyte> collection)
        {
            var wrappedCollection = new WrappedEnumerable<sbyte>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { short.MinValue, short.MaxValue })]
        public void Serialize_ScalarArray_Int16(IEnumerable<short> collection)
        {
            var wrappedCollection = new WrappedEnumerable<short>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { int.MinValue, int.MaxValue })]
        public void Serialize_ScalarArray_Int32(IEnumerable<int> collection)
        {
            var wrappedCollection = new WrappedEnumerable<int>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { long.MinValue, long.MaxValue })]
        public void Serialize_ScalarArray_Int64(IEnumerable<long> collection)
        {
            var wrappedCollection = new WrappedEnumerable<long>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { byte.MinValue, byte.MaxValue })]
        public void Serialize_ScalarArray_UInt8(IEnumerable<byte> collection)
        {
            var wrappedCollection = new WrappedEnumerable<byte>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { ushort.MinValue, ushort.MaxValue })]
        public void Serialize_ScalarArray_UInt16(IEnumerable<ushort> collection)
        {
            var wrappedCollection = new WrappedEnumerable<ushort>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { uint.MinValue, uint.MaxValue })]
        public void Serialize_ScalarArray_UInt32(IEnumerable<uint> collection)
        {
            var wrappedCollection = new WrappedEnumerable<uint>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { ulong.MinValue, ulong.MaxValue })]
        public void Serialize_ScalarArray_UInt64(IEnumerable<ulong> collection)
        {
            var wrappedCollection = new WrappedEnumerable<ulong>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { float.MinValue, float.MaxValue })]
        public void Serialize_ScalarArray_Float(IEnumerable<float> collection)
        {
            var wrappedCollection = new WrappedEnumerable<float>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [TestCase(new[] { double.MinValue, double.MaxValue })]
        public void Serialize_ScalarArray_Double(IEnumerable<double> collection)
        {
            var wrappedCollection = new WrappedEnumerable<double>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }

        [Test]
        public void Serialize_ScalarArray_Decimal()
        {
            var collection = new[] { decimal.MinValue, decimal.MinusOne, decimal.One, decimal.MaxValue };

            var wrappedCollection = new WrappedEnumerable<decimal>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }

        [TestCase("hello")]
        [TestCase("world")]
        public void Serialize_ScalarArray_String(string stringValue)
        {
            var collection = new[] { stringValue, stringValue, stringValue };
            
            var wrappedCollection = new WrappedEnumerable<string>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"\"{value}\""));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarArray_DateTime()
        {
            var collection = new[] { DateTime.MinValue, DateTime.Now, DateTime.MaxValue };
            
            var wrappedCollection = new WrappedEnumerable<DateTime>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(value => $"{value:yyyy-MM-ddTHH:mm:ss.fffZ}"));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
    }
}
