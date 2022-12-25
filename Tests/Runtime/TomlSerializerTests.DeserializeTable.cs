using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_ClassType_ShouldSetFields()
        {
            var classObject = new MockSimpleClass();
            var wrappedObject = new WrappedValue<MockSimpleClass>(classObject);

            var tableValues = string.Join("\n", new[]
            {
                "id = 99",
                "name = \"Hidden Item\"",
                "weight = 0.5",
                "hidden = true",
                "createdAt = 1979-05-27"
            });

            var toml = $"[value]\n{tableValues}\n";
            TomlSerializer.DeserializeInto(toml, wrappedObject);

            Assert.AreEqual(99, wrappedObject.Value.Id);
            Assert.AreEqual("Hidden Item", wrappedObject.Value.Name);
            Assert.AreEqual(0.5, wrappedObject.Value.Weight);
            Assert.AreEqual(true, wrappedObject.Value.Hidden);
            Assert.AreEqual(new DateTime(1979, 5, 27), wrappedObject.Value.CreatedAt);
        }

        [Test]
        public void Deserialize_StructType_ShouldSetField()
        {
            var structObject = new MockSimpleStruct();
            var wrappedObject = new WrappedValue<MockSimpleStruct>(structObject);

            var tableValues = string.Join("\n", new[]
            {
                "index = 42",
                "x = 1.42",
                "y = 3.14",
                "z = 9.99",
            });

            var toml = $"[value]\n{tableValues}\n";
            TomlSerializer.DeserializeInto(toml, wrappedObject);

            Assert.AreEqual(42, wrappedObject.Value.Index);
            Assert.AreEqual(1.42f, wrappedObject.Value.X);
            Assert.AreEqual(3.14f, wrappedObject.Value.Y);
            Assert.AreEqual(9.99f, wrappedObject.Value.Z);
        }
    }
}
