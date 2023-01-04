using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_TomlKeyAttribute_ShouldSetEqual()
        {
            var serializedValue = new WrappedRenamedValue<string>("Hello, World!");
            var tomlString = TomlSerializer.Serialize(serializedValue);

            var deserializedValue = new WrappedRenamedValue<string>();
            TomlSerializer.DeserializeInto(tomlString, deserializedValue);

            Assert.AreEqual(serializedValue.Value, deserializedValue.Value);
        }

        [Test]
        public void SerializeDeserialize_TomlInlineAttribute_ShouldSetEqual()
        {
            var serializedGuard = new Guardian
            {
                Name = "Town Guard",
                AggroRadius = 5.5f
            };
            serializedGuard.AddWaypoint("town_square", new PlayerLocation(500, 24, 42));
            serializedGuard.AddWaypoint("town_gate", new PlayerLocation(500, 42, 24));

            var tomlString = TomlSerializer.Serialize(serializedGuard);

            var deserializedGuard = new Guardian();
            TomlSerializer.DeserializeInto(tomlString, deserializedGuard);

            Assert.AreEqual(serializedGuard.Name, deserializedGuard.Name);
            Assert.AreEqual(serializedGuard.AggroRadius, deserializedGuard.AggroRadius);

            Assert.AreEqual(serializedGuard.Waypoints.Count, deserializedGuard.Waypoints.Count);
            Assert.AreEqual(serializedGuard.Waypoints["town_square"], deserializedGuard.Waypoints["town_square"]);
            Assert.AreEqual(serializedGuard.Waypoints["town_gate"], deserializedGuard.Waypoints["town_gate"]);
        }

        [Test]
        public void SerializeDeserialize_TomlExpandAttribute_ShouldSetEqual()
        {
            var serializedGuard = new Guardian
            {
                Name = "Town Guard",
                AggroRadius = 5.5f
            };
            serializedGuard.AddDialogueChoice("hail", "Hello, traveler.");
            serializedGuard.AddDialogueChoice("farewell", "Goodbye, traveler.");

            var tomlString = TomlSerializer.Serialize(serializedGuard);

            var deserializedGuard = new Guardian();
            TomlSerializer.DeserializeInto(tomlString, deserializedGuard);

            Assert.AreEqual(serializedGuard.Name, deserializedGuard.Name);
            Assert.AreEqual(serializedGuard.AggroRadius, deserializedGuard.AggroRadius);

            Assert.AreEqual(deserializedGuard.DialogueChoices.Count, deserializedGuard.DialogueChoices.Count);
            Assert.AreEqual(serializedGuard.DialogueChoices["hail"], deserializedGuard.DialogueChoices["hail"]);
            Assert.AreEqual(serializedGuard.DialogueChoices["farewell"], deserializedGuard.DialogueChoices["farewell"]);
        }
    }
}
