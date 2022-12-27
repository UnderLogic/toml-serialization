using System;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullDict_ShouldSerializeNull()
        {
            var dict = new WrappedDictionary<string>();

            const string toml = "dictionary = null\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.IsTrue(dict.IsNull, "Dictionary should be null");
        }

        [Test]
        public void Deserialize_EmptyDict_ShouldSerializeEmpty()
        {
            var dict = WrappedDictionary<string>.Null();

            const string toml = "dictionary = {}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.IsTrue(dict.IsEmpty, "Dictionary should be empty");
        }

        [Test]
        public void Deserialize_BoolDict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<bool>();

            const string toml = "dictionary = { on = true, off = false }\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(true, dict.Get("on"));
            Assert.AreEqual(false, dict.Get("off"));
        }

        [Test]
        public void Deserialize_CharDict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<char>();

            const string toml = "dictionary = { letter_a = \"A\", digit_0 = \"0\", underscore = \"_\" }\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual('A', dict.Get("letter_a"));
            Assert.AreEqual('0', dict.Get("digit_0"));
            Assert.AreEqual('_', dict.Get("underscore"));
        }

        [Test]
        public void Deserialize_StringDict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<string>();

            const string toml = "dictionary = { hello = \"world\", foo = \"bar\" }\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual("world", dict.Get("hello"));
            Assert.AreEqual("bar", dict.Get("foo"));
        }

        [Test]
        public void Deserialize_EnumDict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<Direction>();

            const string toml =
                "dictionary = { none = \"None\", up = \"Up\", down = \"Down\", left = \"Left\", right = \"Right\" }\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(Direction.None, dict.Get("none"));
            Assert.AreEqual(Direction.Up, dict.Get("up"));
            Assert.AreEqual(Direction.Down, dict.Get("down"));
            Assert.AreEqual(Direction.Left, dict.Get("left"));
            Assert.AreEqual(Direction.Right, dict.Get("right"));
        }

        [Test]
        public void Deserialize_EnumFlagsDict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<StatusEffects>();

            const string toml =
                "dictionary = { none = \"None\", poison = \"Poison\", immobile = \"Frozen,Sleep,Stun\", all = \"All\" }\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(StatusEffects.None, dict.Get("none"));
            Assert.AreEqual(StatusEffects.Poison, dict.Get("poison"));
            Assert.AreEqual(StatusEffects.Frozen | StatusEffects.Sleep | StatusEffects.Stun, dict.Get("immobile"));
            Assert.AreEqual(StatusEffects.All, dict.Get("all"));
        }

        [Test]
        public void Deserialize_Int8Dict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<sbyte>();

            var toml =
                $"dictionary = {{ min_value = {sbyte.MinValue}, minus_one = -1, zero = 0, plus_one = 1, max_value = {sbyte.MaxValue} }}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(sbyte.MinValue, dict.Get("min_value"));
            Assert.AreEqual(-1, dict.Get("minus_one"));
            Assert.AreEqual(0, dict.Get("zero"));
            Assert.AreEqual(1, dict.Get("plus_one"));
            Assert.AreEqual(sbyte.MaxValue, dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_Int16Dict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<short>();

            var toml =
                $"dictionary = {{ min_value = {short.MinValue}, minus_one = -1, zero = 0, plus_one = 1, max_value = {short.MaxValue} }}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(short.MinValue, dict.Get("min_value"));
            Assert.AreEqual(-1, dict.Get("minus_one"));
            Assert.AreEqual(0, dict.Get("zero"));
            Assert.AreEqual(1, dict.Get("plus_one"));
            Assert.AreEqual(short.MaxValue, dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_Int32Dict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<int>();

            var toml =
                $"dictionary = {{ min_value = {int.MinValue}, minus_one = -1, zero = 0, plus_one = 1, max_value = {int.MaxValue} }}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(int.MinValue, dict.Get("min_value"));
            Assert.AreEqual(-1, dict.Get("minus_one"));
            Assert.AreEqual(0, dict.Get("zero"));
            Assert.AreEqual(1, dict.Get("plus_one"));
            Assert.AreEqual(int.MaxValue, dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_Int64Dict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<long>();

            var toml =
                $"dictionary = {{ min_value = {long.MinValue}, minus_one = -1, zero = 0, plus_one = 1, max_value = {long.MaxValue} }}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(long.MinValue, dict.Get("min_value"));
            Assert.AreEqual(-1, dict.Get("minus_one"));
            Assert.AreEqual(0, dict.Get("zero"));
            Assert.AreEqual(1, dict.Get("plus_one"));
            Assert.AreEqual(long.MaxValue, dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_UInt8Dict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<byte>();

            var toml =
                $"dictionary = {{ zero = 0, plus_one = 1, max_value = {byte.MaxValue} }}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(0, dict.Get("zero"));
            Assert.AreEqual(1, dict.Get("plus_one"));
            Assert.AreEqual(byte.MaxValue, dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_UInt16Dict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<ushort>();

            var toml =
                $"dictionary = {{ zero = 0, plus_one = 1, max_value = {ushort.MaxValue} }}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(0, dict.Get("zero"));
            Assert.AreEqual(1, dict.Get("plus_one"));
            Assert.AreEqual(ushort.MaxValue, dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_UInt32Dict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<uint>();

            var toml =
                $"dictionary = {{ zero = 0, plus_one = 1, max_value = {uint.MaxValue} }}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(0, dict.Get("zero"));
            Assert.AreEqual(1, dict.Get("plus_one"));
            Assert.AreEqual(uint.MaxValue, dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_FloatDict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<float>();

            var toml =
                $"dictionary = {{ min_value = -3.14e6, minus_one = -1, zero = 0, plus_one = 1, max_value = +3.14e6 }}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(-3.14e6f, dict.Get("min_value"));
            Assert.AreEqual(-1f, dict.Get("minus_one"));
            Assert.AreEqual(0f, dict.Get("zero"));
            Assert.AreEqual(1f, dict.Get("plus_one"));
            Assert.AreEqual(3.14e6f, dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_DoubleDict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<double>();

            const string toml =
                "dictionary = { min_value = -3.14e6, minus_one = -1, zero = 0, plus_one = 1, max_value = +3.14e6 }\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(-3.14e6, dict.Get("min_value"));
            Assert.AreEqual(-1, dict.Get("minus_one"));
            Assert.AreEqual(0, dict.Get("zero"));
            Assert.AreEqual(1, dict.Get("plus_one"));
            Assert.AreEqual(3.14e6, dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_DateTimeDict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<DateTime>();

            const string toml = "dictionary = { min_value = 1979-05-27, max_value = 2020-10-01 }\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(new DateTime(1979, 5, 27), dict.Get("min_value"));
            Assert.AreEqual(new DateTime(2020, 10,1), dict.Get("max_value"));
        }

        [Test]
        public void Deserialize_MixedDict_ShouldSetValues()
        {
            var dict = new WrappedDictionary<object>();

            var toml =
                $"dictionary = {{ id = 42, name = \"John\", weight = 128.6, isAdmin = true }}\n";
            TomlSerializer.DeserializeInto(toml, dict);

            Assert.AreEqual(42, dict.Get("id"));
            Assert.AreEqual("John", dict.Get("name"));
            Assert.AreEqual(128.6, dict.Get("weight"));
            Assert.AreEqual(true, dict.Get("isAdmin"));
        }
    }
}
