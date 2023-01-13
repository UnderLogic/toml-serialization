using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlInlineAttributeTests
    {
        [Test]
        public void Serialize_Table_ShouldBeInline()
        {
            var stats = new PlayerStats
            {
                Strength = 1,
                Intelligence = 2,
                Wisdom = 3,
                Constitution = 4,
                Dexterity = 5
            };

            var serializedInline = new SerializableInlineValue<PlayerStats>(stats);
            var toml = TomlSerializer.Serialize(serializedInline);

            var expectedToml = new StringBuilder()
                .Append("value = { ")
                .Append($"strength = {stats.Strength}, ")
                .Append($"intelligence = {stats.Intelligence}, ")
                .Append($"wisdom = {stats.Wisdom}, ")
                .Append($"constitution = {stats.Constitution}, ")
                .Append($"dexterity = {stats.Dexterity}")
                .AppendLine(" }")
                .ToString();
            
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
