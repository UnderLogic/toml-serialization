using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_ClassArray_ShouldSetElements()
        {
            var toml = string.Join("\n", new[]
            {
                "[[array]]",
                "id = 42",
                "name = \"Normal Item\"",
                "weight = 1.5",
                "hidden = false",
                "createdAt = 2020-10-01",
                "",
                "[[array]]",
                "id = 99",
                "name = \"Hidden Item\"",
                "weight = 0.1",
                "hidden = true",
                "createdAt = 2021-11-22",
            });

            var array = WrappedArray<MockSimpleClass>.Empty();
            TomlSerializer.DeserializeInto(toml, array);
            
            Assert.IsFalse(array.IsEmpty, "Array should not be empty");
            
            Assert.AreEqual(42, array[0].Id);
            Assert.AreEqual("Normal Item", array[0].Name);
            Assert.AreEqual(1.5f, array[0].Weight);
            Assert.AreEqual(false, array[0].Hidden);
            Assert.AreEqual(new DateTime(2020, 10, 1), array[0].CreatedAt);
            
            Assert.AreEqual(99, array[1].Id);
            Assert.AreEqual("Hidden Item", array[1].Name);
            Assert.AreEqual(0.1f, array[1].Weight);
            Assert.AreEqual(true, array[1].Hidden);
            Assert.AreEqual(new DateTime(2021, 11, 22), array[1].CreatedAt);
        }
        
        [Test]
        public void Deserialize_StructArray_ShouldSetElements()
        {
            var toml = string.Join("\n", new[]
            {
                "[[array]]",
                "index = 10",
                "x = -1",
                "y = -2",
                "z = -3",
                "",
                "[[array]]",
                "index = 20",
                "x = 2.22",
                "y = 4.44",
                "z = 6.66"
            });
            
            var array = WrappedArray<MockSimpleStruct>.Empty();
            TomlSerializer.DeserializeInto(toml, array);

            Assert.IsFalse(array.IsEmpty, "Array should not be empty");
            
            Assert.AreEqual(10, array[0].Index);
            Assert.AreEqual(-1f, array[0].X);
            Assert.AreEqual(-2f, array[0].Y);
            Assert.AreEqual(-3f, array[0].Z);
            
            Assert.AreEqual(20, array[1].Index);
            Assert.AreEqual(2.22f, array[1].X);
            Assert.AreEqual(4.44f, array[1].Y);
            Assert.AreEqual(6.66f, array[1].Z);
        }
    }
}
