using System.Text;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal class TomlExpandAttributeTests
    {
        [Test]
        public void Serialize_Dictionary_ShouldBeStandardTable()
        {
            var serializedDictionary = new SerializableTable<int>
            {
                { "strength", 1 },
                { "intelligence", 2 },
                { "wisdom", 3 },
                { "constitution", 4 },
                { "dexterity", 5 }
            };
            
            var toml = TomlSerializer.Serialize(serializedDictionary);

            var expectedToml = new StringBuilder()
                .AppendLine("[table]")
                .AppendLine("strength = 1")
                .AppendLine("intelligence = 2")
                .AppendLine("wisdom = 3")
                .AppendLine("constitution = 4")
                .AppendLine("dexterity = 5")
                .ToString();
            
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
