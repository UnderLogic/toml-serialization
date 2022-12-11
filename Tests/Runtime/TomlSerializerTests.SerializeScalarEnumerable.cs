using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    public partial class TomlSerializerTests
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
    }
}
