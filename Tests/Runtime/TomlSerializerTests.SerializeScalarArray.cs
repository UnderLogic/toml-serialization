using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    public partial class TomlSerializerTests
    {
        private const string ArrayDelimiter = ", ";

        [TestCase(new[] { true, false })]
        public void Serialize_ScalarArray_Bool(IEnumerable<bool> collection)
        {
            var wrappedCollection = new WrappedEnumerable<bool>(collection);
            var tomlString = TomlSerializer.Serialize(wrappedCollection);

            var arrayString = string.Join(ArrayDelimiter,
                wrappedCollection.Select(x => x.ToString().ToLowerInvariant()));

            Assert.AreEqual($"collection = [{arrayString}]", tomlString.Trim());
        }
    }
}
