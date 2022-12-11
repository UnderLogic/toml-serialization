using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_ScalarEnumerable_Bool()
        {
            var collection = new List<bool>(new[] { true, false });
            var wrappedEnumerable = new WrappedEnumerable<bool>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value}".ToLowerInvariant()));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_Char()
        {
            var collection = new List<char>(new[] { 'A', 'Z', '0', '9', '_' });
            var wrappedEnumerable = new WrappedEnumerable<char>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"\"{value}\""));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_Int8()
        {
            var collection = new List<sbyte>(new[] { sbyte.MinValue, sbyte.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<sbyte>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_Int16()
        {
            var collection = new List<short>(new[] { short.MinValue, short.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<short>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_Int32()
        {
            var collection = new List<int>(new[] { int.MinValue, int.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<int>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_Int64()
        {
            var collection = new List<long>(new[] { long.MinValue, long.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<long>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_UInt8()
        {
            var collection = new List<byte>(new[] { byte.MinValue, byte.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<byte>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_UInt16()
        {
            var collection = new List<ushort>(new[] { ushort.MinValue, ushort.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<ushort>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_UInt32()
        {
            var collection = new List<uint>(new[] { uint.MinValue, uint.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<uint>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_Float()
        {
            var collection = new List<float>(new[] { float.MinValue, float.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<float>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{(double)value}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_Double()
        {
            var collection = new List<double>(new[] { double.MinValue, double.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<double>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }

        [Test]
        public void Serialize_ScalarEnumerable_String()
        {
            var collection = new List<string>(new[] { "hello", "world" });
            var wrappedEnumerable = new WrappedEnumerable<string>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"\"{value}\""));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
        
        [Test]
        public void Serialize_ScalarEnumerable_DateTime()
        {
            var collection = new List<DateTime>(new[] { DateTime.MinValue, DateTime.Now, DateTime.MaxValue });
            var wrappedEnumerable = new WrappedEnumerable<DateTime>(collection);

            var tomlString = TomlSerializer.Serialize(wrappedEnumerable);
            var enumerableString = string.Join(ArrayDelimiter,
                wrappedEnumerable.Select(value => $"{value:yyyy-MM-dd HH:mm:ss.fffZ}"));
            
            Assert.AreEqual($"collection = [{enumerableString}]", tomlString.Trim());
        }
    }
}
