using System;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullList_ShouldSetNull()
        {
            var wrappedList = WrappedList<string>.FromValues("hello", "world");
            const string toml = "list = null\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsTrue(wrappedList.IsNull, "List should be null");
        }

        [Test]
        public void Deserialize_EmptyList_ShouldSetEmpty()
        {
            var wrappedList = WrappedList<string>.FromValues("hello", "world");
            const string toml = "list = []\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsTrue(wrappedList.IsEmpty, "List should be empty");
        }
        
        [Test]
        public void Deserialize_ListOfNulls_ShouldSetNulls()
        {
            var wrappedList = WrappedList<string>.FromValues("hello", "world");
            const string toml = "list = [ null, null, null ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);

            var expectedValues = new string[] { null, null, null };
            Assert.IsTrue(wrappedList.SequenceEqual(expectedValues), "List should contain nulls");
        }

        [Test]
        public void Deserialize_BoolList_ShouldSetElements()
        {
            var wrappedList = WrappedList<bool>.Empty();
            const string toml = "list = [ true, false ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(true, wrappedList[0]);
            Assert.AreEqual(false, wrappedList[1]);
        }

        [Test]
        public void Deserialize_CharList_ShouldSetElements()
        {
            var wrappedList = WrappedList<char>.Empty();
            const string toml = "list = [ \"A\", \"0\", \"!\" ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual('A', wrappedList[0]);
            Assert.AreEqual('0', wrappedList[1]);
            Assert.AreEqual('!', wrappedList[2]);
        }

        [Test]
        public void Deserialize_StringList_ShouldSetElements()
        {
            var wrappedList = WrappedList<string>.Empty();
            const string toml = "list = [ \"Hello\", \"World!\" ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual("Hello", wrappedList[0]);
            Assert.AreEqual("World!", wrappedList[1]);
        }

        [Test]
        public void Deserialize_EnumList_ShouldSetElements()
        {
            var wrappedList = WrappedList<Direction>.Empty();
            const string toml = "list = [ \"Up\", \"Down\", \"Left\", \"Right\" ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(Direction.Up, wrappedList[0]);
            Assert.AreEqual(Direction.Down, wrappedList[1]);
            Assert.AreEqual(Direction.Left, wrappedList[2]);
            Assert.AreEqual(Direction.Right, wrappedList[3]);
        }

        [Test]
        public void Deserialize_EnumFlagsList_ShouldSetElements()
        {
            var wrappedList = WrappedList<StatusEffects>.Empty();
            const string toml =
                "list = [ \"Poison\", \"Blind\", \"Frozen,Sleep\", \"All\" ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(StatusEffects.Poison, wrappedList[0]);
            Assert.AreEqual(StatusEffects.Blind, wrappedList[1]);
            Assert.AreEqual(StatusEffects.Frozen | StatusEffects.Sleep, wrappedList[2]);
            Assert.AreEqual(StatusEffects.All, wrappedList[3]);
        }
        
        [Test]
        public void Deserialize_Int8List_ShouldSetElements()
        {
            var wrappedList = WrappedList<sbyte>.Empty();
            const string toml = "list = [ -100, -10, 0, 10, 100 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(-100, wrappedList[0]);
            Assert.AreEqual(-10, wrappedList[1]);
            Assert.AreEqual(0, wrappedList[2]);
            Assert.AreEqual(10, wrappedList[3]);
            Assert.AreEqual(100, wrappedList[4]);
        }

        [Test]
        public void Deserialize_Int16List_ShouldSetElements()
        {
            var wrappedList = WrappedList<short>.Empty();
            const string toml = "list = [ -100, -10, 0, 10, 100 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(-100, wrappedList[0]);
            Assert.AreEqual(-10, wrappedList[1]);
            Assert.AreEqual(0, wrappedList[2]);
            Assert.AreEqual(10, wrappedList[3]);
            Assert.AreEqual(100, wrappedList[4]);
        }

        [Test]
        public void Deserialize_Int32List_ShouldSetElements()
        {
            var wrappedList = WrappedList<int>.Empty();
            const string toml = "list = [ -100, -10, 0, 10, 100 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(-100, wrappedList[0]);
            Assert.AreEqual(-10, wrappedList[1]);
            Assert.AreEqual(0, wrappedList[2]);
            Assert.AreEqual(10, wrappedList[3]);
            Assert.AreEqual(100, wrappedList[4]);
        }

        [Test]
        public void Deserialize_Int64List_ShouldSetElements()
        {
            var wrappedList = WrappedList<long>.Empty();
            const string toml = "list = [ -100, -10, 0, 10, 100 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(-100, wrappedList[0]);
            Assert.AreEqual(-10, wrappedList[1]);
            Assert.AreEqual(0, wrappedList[2]);
            Assert.AreEqual(10, wrappedList[3]);
            Assert.AreEqual(100, wrappedList[4]);
        }

        [Test]
        public void Deserialize_UInt8List_ShouldSetElements()
        {
            var wrappedList = WrappedList<byte>.Empty();
            const string toml = "list = [ 0, 100, 200 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(0, wrappedList[0]);
            Assert.AreEqual(100, wrappedList[1]);
            Assert.AreEqual(200, wrappedList[2]);
        }

        [Test]
        public void Deserialize_UInt16List_ShouldSetElements()
        {
            var wrappedList = WrappedList<ushort>.Empty();
            const string toml = "list = [ 0, 100, 200 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(0, wrappedList[0]);
            Assert.AreEqual(100, wrappedList[1]);
            Assert.AreEqual(200, wrappedList[2]);
        }

        [Test]
        public void Deserialize_UInt32List_ShouldSetElements()
        {
            var wrappedList = WrappedList<uint>.Empty();
            const string toml = "list = [ 0, 100, 200 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(0, wrappedList[0]);
            Assert.AreEqual(100, wrappedList[1]);
            Assert.AreEqual(200, wrappedList[2]);
        }

        [Test]
        public void Deserialize_FloatList_ShouldSetElements()
        {
            var wrappedList = WrappedList<float>.Empty();
            const string toml = "list = [ -3.14, -1.0, -0.5, 0.0, 0.5, 1.0, 3.14 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(-3.14f, wrappedList[0]);
            Assert.AreEqual(-1f, wrappedList[1]);
            Assert.AreEqual(-0.5f, wrappedList[2]);
            Assert.AreEqual(0f, wrappedList[3]);
            Assert.AreEqual(0.5f, wrappedList[4]);
            Assert.AreEqual(1f, wrappedList[5]);
            Assert.AreEqual(3.14f, wrappedList[6]);
        }

        [Test]
        public void Deserialize_DoubleList_ShouldSetElements()
        {
            var wrappedList = WrappedList<double>.Empty();
            const string toml = "list = [ -3.14, -1.0, -0.5, 0.0, 0.5, 1.0, 3.14 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(-3.14, wrappedList[0]);
            Assert.AreEqual(-1, wrappedList[1]);
            Assert.AreEqual(-0.5, wrappedList[2]);
            Assert.AreEqual(0, wrappedList[3]);
            Assert.AreEqual(0.5, wrappedList[4]);
            Assert.AreEqual(1, wrappedList[5]);
            Assert.AreEqual(3.14, wrappedList[6]);
        }

        [Test]
        public void Deserialize_DateTimeList_ShouldSetElements()
        {
            var wrappedList = WrappedList<DateTime>.Empty();
            const string toml = "list = [ 2020-01-01, 2021-01-01, 2022-01-01 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual(new DateTime(2020, 1, 1), wrappedList[0]);
            Assert.AreEqual(new DateTime(2021, 1, 1), wrappedList[1]);
            Assert.AreEqual(new DateTime(2022, 1, 1), wrappedList[2]);
        }

        [Test]
        public void Deserialize_MixedList_ShouldSetElements()
        {
            var wrappedList = WrappedList<object>.Empty();
            const string toml = "list = [ \"John\", 42, 3.14, true, 1979-05-27 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedList);
            Assert.IsFalse(wrappedList.IsEmpty, "List should not be empty");

            Assert.AreEqual("John", wrappedList[0]);
            Assert.AreEqual(42L, wrappedList[1]);
            Assert.AreEqual(3.14, wrappedList[2]);
            Assert.AreEqual(true, wrappedList[3]);
            Assert.AreEqual(new DateTime(1979, 5, 27), wrappedList[4]);
        }
    }
}
