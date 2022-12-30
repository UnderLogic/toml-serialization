using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_LineComment_ShouldBeIgnored()
        {
            var toml = string.Join("\n", new[]
            {
                "[dictionary]",
                "value = 42",
                "# commentValue = 99"
            });

            var wrappedDict = new WrappedDictionary<int>();
            TomlSerializer.DeserializeInto($"{toml}\n", wrappedDict);

            Assert.IsTrue(wrappedDict.ContainsKey("value"));
            Assert.IsFalse(wrappedDict.ContainsKey("commentValue"));
        }

        [Test]
        public void Deserialize_InlineComment_ShouldBeIgnored()
        {
            var toml = string.Join("\n", new[]
            {
                "[dictionary]",
                "value = 42",
                "commentValue = 99    # this is a comment"
            });

            var wrappedDict = new WrappedDictionary<int>();
            TomlSerializer.DeserializeInto($"{toml}\n", wrappedDict);

            Assert.IsTrue(wrappedDict.ContainsKey("value"));
            Assert.IsTrue(wrappedDict.ContainsKey("commentValue"));
        }
    }
}
