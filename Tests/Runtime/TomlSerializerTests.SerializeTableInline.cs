using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_TableInline_Bool()
        {
            var dict = new WrappedDictionary<bool>();
            dict.Add("yes", true);
            dict.Add("no", false);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString().ToLowerInvariant());
            Assert.AreEqual(expectedTomlString.Trim(), tomlString.Trim());
        }
    }
}
