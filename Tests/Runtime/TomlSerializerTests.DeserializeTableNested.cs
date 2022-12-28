using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_ObjectDict_ShouldSetValues()
        {
            var toml = string.Join("\n", new []
            {
                "name = \"Boss\"",
                "",
                "[loot]",
                "",
                "[loot.common]",
                "tableName = \"boss-common\"",
                "chance = 0.50",
                "rolls = 3",
                "dropForAllPlayers = false",
                "",
                "[loot.uncommon]",
                "tableName = \"boss-uncommon\"",
                "chance = 0.25",
                "rolls = 2",
                "dropForAllPlayers = false",
                "",
                "[loot.rare]",
                "tableName = \"boss-rare\"",
                "chance = 0.05",
                "rolls = 1",
                "dropForAllPlayers = true",
            });

            var monster = new Monster();
            TomlSerializer.DeserializeInto(toml, monster);

            Assert.IsNotEmpty(monster.Loot, "Loot should not be empty");
            Assert.IsNotNull(monster.Loot["common"], "Loot should contain common table");
            Assert.IsNotNull(monster.Loot["uncommon"], "Loot should contain uncommon table");
            Assert.IsNotNull(monster.Loot["rare"], "Loot should contain rare table");

            var commonLoot = monster.Loot["common"];
            Assert.AreEqual("boss-common", commonLoot.TableName);
            Assert.AreEqual((float)0.50, commonLoot.Chance);
            Assert.AreEqual(3, commonLoot.Rolls);
            Assert.AreEqual(false, commonLoot.DropForAllPlayers);

            var uncommonLoot = monster.Loot["uncommon"];
            Assert.AreEqual("boss-uncommon", uncommonLoot.TableName);
            Assert.AreEqual((float)0.25, uncommonLoot.Chance);
            Assert.AreEqual(2, uncommonLoot.Rolls);
            Assert.AreEqual(false, uncommonLoot.DropForAllPlayers);

            var rareLoot = monster.Loot["rare"];
            Assert.AreEqual("boss-rare", rareLoot.TableName);
            Assert.AreEqual((float)0.05, rareLoot.Chance);
            Assert.AreEqual(1, rareLoot.Rolls);
            Assert.AreEqual(true, rareLoot.DropForAllPlayers);
        }
    }
}
