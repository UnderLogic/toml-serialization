using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_Table_UserClass()
        {
            var mockPlayerData = new MockPlayerData("Test", 99, 42, 1000);
            var wrappedData = new WrappedValue<MockPlayerData>(mockPlayerData);

            var tomlString = TomlSerializer.Serialize(wrappedData);

            var expectedToml = mockPlayerData.ToTomlStringTable("value");
            Assert.AreEqual(expectedToml, tomlString);
        }
    }
}
