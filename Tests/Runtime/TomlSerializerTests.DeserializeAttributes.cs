using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_TomlKeyAttribute_ShouldMapKey()
        {
            const string toml = "renamedValue = \"The quick brown fox jumps over the lazy dog.\"";

            var deserializedValue = new WrappedRenamedValue<string>();
            TomlSerializer.DeserializeInto(toml, deserializedValue);

            Assert.AreEqual("The quick brown fox jumps over the lazy dog.", deserializedValue.Value);
        }
    }
}
