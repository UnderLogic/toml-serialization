using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_Table_PlayerData()
        {
            var mockPlayerData = new MockPlayerData();
            var wrappedData = new WrappedValue<MockPlayerData>(mockPlayerData);

            var tomlString = TomlSerializer.Serialize(wrappedData);

            var expectedToml = mockPlayerData.ToTomlStringTable("value");
            Assert.AreEqual(expectedToml, tomlString);
        }
    }
}
