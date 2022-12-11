using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_Table_Inline()
        {
            var dict = new MockDictionary();
            dict.Add("first", 10);
            dict.Add("second", 20);
            dict.Add("third", 30);

            var expectedString = dict.ToTomlStringInline();

            var tomlString = TomlSerializer.Serialize(dict);
            Assert.AreEqual(expectedString.Trim(), tomlString.Trim());
        }
    }
}
