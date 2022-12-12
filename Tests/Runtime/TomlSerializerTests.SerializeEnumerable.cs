using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_Enumerable_Bool()
        {
            var collection = new List<bool>(new[] { true, false });
            var wrappedList = new WrappedEnumerable<bool>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);

            var expectedTomlString =
                wrappedList.ToTomlStringArray("values", value => value.ToString().ToLowerInvariant());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }

        [Test]
        public void Serialize_Enumerable_Char()
        {
            var collection = new List<char>(new[] { 'A', 'Z', '0', '9', '_' });
            var wrappedList = new WrappedEnumerable<char>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }
        
        [Test]
        public void Serialize_Enumerable_Int8()
        {
            var collection = new List<sbyte>(new[] { sbyte.MinValue, sbyte.MaxValue });
            var wrappedList = new WrappedEnumerable<sbyte>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }
        
        [Test]
        public void Serialize_Enumerable_Int16()
        {
            var collection = new List<short>(new[] { short.MinValue, short.MaxValue });
            var wrappedList = new WrappedEnumerable<short>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }
        
        [Test]
        public void Serialize_Enumerable_Int32()
        {
            var collection = new List<int>(new[] { int.MinValue, int.MaxValue });
            var wrappedList = new WrappedEnumerable<int>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }
        
        [Test]
        public void Serialize_Enumerable_Int64()
        {
            var collection = new List<long>(new[] { long.MinValue, long.MaxValue });
            var wrappedList = new WrappedEnumerable<long>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }
        
        [Test]
        public void Serialize_Enumerable_UInt8()
        {
            var collection = new List<byte>(new[] { byte.MinValue, byte.MaxValue });
            var wrappedList = new WrappedEnumerable<byte>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }
        
        [Test]
        public void Serialize_Enumerable_UInt16()
        {
            var collection = new List<ushort>(new[] { ushort.MinValue, ushort.MaxValue });
            var wrappedList = new WrappedEnumerable<ushort>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }
        
        [Test]
        public void Serialize_Enumerable_UInt32()
        {
            var collection = new List<uint>(new[] { uint.MinValue, uint.MaxValue });
            var wrappedList = new WrappedEnumerable<uint>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }

        [Test]
        public void Serialize_Enumerable_Float()
        {
            var collection = new List<float>(new[] { float.MinValue, float.MaxValue });
            var wrappedList = new WrappedEnumerable<float>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);

            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => ((double)value).ToString());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }

        [Test]
        public void Serialize_Enumerable_Double()
        {
            var collection = new List<double>(new[] { double.MinValue, double.MaxValue });
            var wrappedList = new WrappedEnumerable<double>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }

        [Test]
        public void Serialize_Enumerable_String()
        {
            var collection = new List<string>(new[] { "hello", "world" });
            var wrappedList = new WrappedEnumerable<string>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);
            
            var expectedTomlString = wrappedList.ToTomlStringArray("values", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }

        [Test]
        public void Serialize_Enumerable_DateTime()
        {
            var collection = new List<DateTime>(new[] { DateTime.MinValue, DateTime.Now, DateTime.MaxValue });
            var wrappedList = new WrappedEnumerable<DateTime>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedList);

            var expectedTomlString =
                wrappedList.ToTomlStringArray("values", value => $"{value:yyyy-MM-dd HH:mm:ss.fffZ}");
            Assert.AreEqual(expectedTomlString, tomlString.Trim());
        }
    }
}
