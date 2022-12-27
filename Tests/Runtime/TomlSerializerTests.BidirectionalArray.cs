using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void SerializeDeserialize_NullArray_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<string>.Null();
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializeArray = WrappedArray<string>.FromValues("Hello", "World!");
            TomlSerializer.DeserializeInto(tomlString, deserializeArray);

            Assert.IsTrue(deserializeArray.IsNull);
        }
        
        [Test]
        public void SerializeDeserialize_EmptyArray_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<string>.Empty();
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializeArray = WrappedArray<string>.FromValues("Hello", "World!");
            TomlSerializer.DeserializeInto(tomlString, deserializeArray);

            Assert.IsTrue(deserializeArray.IsEmpty);
        }

        [Test]
        public void SerializeDeserialize_ArrayOfNulls_ShouldBeEqual()
        {
            var serializedArray = WrappedArray<string>.FromValues(null, null, null);
            var tomlString = TomlSerializer.Serialize(serializedArray);

            var deserializeArray = WrappedArray<string>.FromValues("Hello", "World!");
            TomlSerializer.DeserializeInto(tomlString, deserializeArray);

            Assert.IsTrue(deserializeArray.ElementsAreSame(serializedArray), "Array contents are not equal");
        }
    }
}
