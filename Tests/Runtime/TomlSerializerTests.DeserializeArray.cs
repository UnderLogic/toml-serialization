using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullArray_ShouldSetNull()
        {
            var wrappedArray = WrappedArray<string>.FromValues("hello", "world");
            const string toml = "array = null\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsTrue(wrappedArray.IsNull, "Array should be null");
        }

        [Test]
        public void Deserialize_EmptyArray_ShouldSetEmpty()
        {
            var wrappedArray = WrappedArray<string>.FromValues("hello", "world");
            const string toml = "array = []\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsTrue(wrappedArray.IsEmpty, "Array should be empty");
        }

        [Test]
        public void Deserialize_BoolArray_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<bool>.Empty();
            const string toml = "array = [ true, false ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(true, wrappedArray[0]);
            Assert.AreEqual(false, wrappedArray[1]);
        }

        [Test]
        public void Deserialize_CharArray_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<char>.Empty();
            const string toml = "array = [ \"A\", \"0\", \"!\" ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual('A', wrappedArray[0]);
            Assert.AreEqual('0', wrappedArray[1]);
            Assert.AreEqual('!', wrappedArray[2]);
        }

        [Test]
        public void Deserialize_StringArray_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<string>.Empty();
            const string toml = "array = [ \"Hello\", \"World!\" ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual("Hello", wrappedArray[0]);
            Assert.AreEqual("World!", wrappedArray[1]);
        }

        [Test]
        public void Deserialize_EnumArray_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<Direction>.Empty();
            const string toml = "array = [ \"Up\", \"Down\", \"Left\", \"Right\" ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(Direction.Up, wrappedArray[0]);
            Assert.AreEqual(Direction.Down, wrappedArray[1]);
            Assert.AreEqual(Direction.Left, wrappedArray[2]);
            Assert.AreEqual(Direction.Right, wrappedArray[3]);
        }

        [Test]
        public void Deserialize_EnumFlagsArray_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<StatusEffects>.Empty();
            const string toml =
                "array = [ \"Poison\", \"Blind\", \"Frozen,Sleep\", \"All\" ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(StatusEffects.Poison, wrappedArray[0]);
            Assert.AreEqual(StatusEffects.Blind, wrappedArray[1]);
            Assert.AreEqual(StatusEffects.Frozen | StatusEffects.Sleep, wrappedArray[2]);
            Assert.AreEqual(StatusEffects.All, wrappedArray[3]);
        }

        [Test]
        public void Deserialize_Int8Array_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<sbyte>.Empty();
            const string toml = "array = [ -100, -10, 0, 10, 100 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(-100, wrappedArray[0]);
            Assert.AreEqual(-10, wrappedArray[1]);
            Assert.AreEqual(0, wrappedArray[2]);
            Assert.AreEqual(10, wrappedArray[3]);
            Assert.AreEqual(100, wrappedArray[4]);
        }

        [Test]
        public void Deserialize_Int16Array_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<short>.Empty();
            const string toml = "array = [ -100, -10, 0, 10, 100 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(-100, wrappedArray[0]);
            Assert.AreEqual(-10, wrappedArray[1]);
            Assert.AreEqual(0, wrappedArray[2]);
            Assert.AreEqual(10, wrappedArray[3]);
            Assert.AreEqual(100, wrappedArray[4]);
        }

        [Test]
        public void Deserialize_Int32Array_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<int>.Empty();
            const string toml = "array = [ -100, -10, 0, 10, 100 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(-100, wrappedArray[0]);
            Assert.AreEqual(-10, wrappedArray[1]);
            Assert.AreEqual(0, wrappedArray[2]);
            Assert.AreEqual(10, wrappedArray[3]);
            Assert.AreEqual(100, wrappedArray[4]);
        }

        [Test]
        public void Deserialize_Int64Array_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<long>.Empty();
            const string toml = "array = [ -100, -10, 0, 10, 100 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(-100, wrappedArray[0]);
            Assert.AreEqual(-10, wrappedArray[1]);
            Assert.AreEqual(0, wrappedArray[2]);
            Assert.AreEqual(10, wrappedArray[3]);
            Assert.AreEqual(100, wrappedArray[4]);
        }

        [Test]
        public void Deserialize_UInt8Array_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<byte>.Empty();
            const string toml = "array = [ 0, 100, 200 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(0, wrappedArray[0]);
            Assert.AreEqual(100, wrappedArray[1]);
            Assert.AreEqual(200, wrappedArray[2]);
        }

        [Test]
        public void Deserialize_UInt16Array_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<ushort>.Empty();
            const string toml = "array = [ 0, 100, 200 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(0, wrappedArray[0]);
            Assert.AreEqual(100, wrappedArray[1]);
            Assert.AreEqual(200, wrappedArray[2]);
        }

        [Test]
        public void Deserialize_UInt32Array_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<uint>.Empty();
            const string toml = "array = [ 0, 100, 200 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(0, wrappedArray[0]);
            Assert.AreEqual(100, wrappedArray[1]);
            Assert.AreEqual(200, wrappedArray[2]);
        }

        [Test]
        public void Deserialize_FloatArray_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<float>.Empty();
            const string toml = "array = [ -3.14, -1.0, -0.5, 0.0, 0.5, 1.0, 3.14 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(-3.14f, wrappedArray[0]);
            Assert.AreEqual(-1f, wrappedArray[1]);
            Assert.AreEqual(-0.5f, wrappedArray[2]);
            Assert.AreEqual(0f, wrappedArray[3]);
            Assert.AreEqual(0.5f, wrappedArray[4]);
            Assert.AreEqual(1f, wrappedArray[5]);
            Assert.AreEqual(3.14f, wrappedArray[6]);
        }

        [Test]
        public void Deserialize_DoubleArray_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<double>.Empty();
            const string toml = "array = [ -3.14, -1.0, -0.5, 0.0, 0.5, 1.0, 3.14 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(-3.14, wrappedArray[0]);
            Assert.AreEqual(-1, wrappedArray[1]);
            Assert.AreEqual(-0.5, wrappedArray[2]);
            Assert.AreEqual(0, wrappedArray[3]);
            Assert.AreEqual(0.5, wrappedArray[4]);
            Assert.AreEqual(1, wrappedArray[5]);
            Assert.AreEqual(3.14, wrappedArray[6]);
        }

        [Test]
        public void Deserialize_DateTimeArray_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<DateTime>.Empty();
            const string toml = "array = [ 2020-01-01, 2021-01-01, 2022-01-01 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual(new DateTime(2020, 1, 1), wrappedArray[0]);
            Assert.AreEqual(new DateTime(2021, 1, 1), wrappedArray[1]);
            Assert.AreEqual(new DateTime(2022, 1, 1), wrappedArray[2]);
        }

        [Test]
        public void Deserialize_MixedArray_ShouldSetElements()
        {
            var wrappedArray = WrappedArray<object>.Empty();
            const string toml = "array = [ \"John\", 42, 3.14, true, 1979-05-27 ]\n";

            TomlSerializer.DeserializeInto(toml, wrappedArray);
            Assert.IsFalse(wrappedArray.IsEmpty, "Array should not be empty");

            Assert.AreEqual("John", wrappedArray[0]);
            Assert.AreEqual(42L, wrappedArray[1]);
            Assert.AreEqual(3.14, wrappedArray[2]);
            Assert.AreEqual(true, wrappedArray[3]);
            Assert.AreEqual(new DateTime(1979, 5, 27), wrappedArray[4]);
        }
    }
}
