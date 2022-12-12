using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_Table_Inline_Bool()
        {
            var dict = new WrappedDictionary<bool>();
            dict.Add("yes", true);
            dict.Add("no", false);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString().ToLowerInvariant());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_Char()
        {
            var dict = new WrappedDictionary<char>();
            dict.Add("letter_a", 'A');
            dict.Add("letter_z", 'Z');
            dict.Add("number_0", '0');
            dict.Add("number_9", '9');
            dict.Add("symbol", '_');

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_Int8()
        {
            var dict = new WrappedDictionary<sbyte>();
            dict.Add("min_value", sbyte.MinValue);
            dict.Add("minus_one", -1);
            dict.Add("zero", 0);
            dict.Add("one", +1);
            dict.Add("max_value", sbyte.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }

        [Test]
        public void Serialize_Table_Inline_Int16()
        {
            var dict = new WrappedDictionary<short>();
            dict.Add("min_value", short.MinValue);
            dict.Add("minus_one", -1);
            dict.Add("zero", 0);
            dict.Add("one", +1);
            dict.Add("max_value", short.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_Int32()
        {
            var dict = new WrappedDictionary<int>();
            dict.Add("min_value", int.MinValue);
            dict.Add("minus_one", -1);
            dict.Add("zero", 0);
            dict.Add("one", +1);
            dict.Add("max_value", int.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_Int64()
        {
            var dict = new WrappedDictionary<long>();
            dict.Add("min_value", long.MinValue);
            dict.Add("minus_one", -1);
            dict.Add("zero", 0);
            dict.Add("one", +1);
            dict.Add("max_value", long.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_UInt8()
        {
            var dict = new WrappedDictionary<byte>();
            dict.Add("min_value", byte.MinValue);
            dict.Add("zero", 0);
            dict.Add("one", +1);
            dict.Add("max_value", byte.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_UInt16()
        {
            var dict = new WrappedDictionary<ushort>();
            dict.Add("min_value", ushort.MinValue);
            dict.Add("zero", 0);
            dict.Add("one", +1);
            dict.Add("max_value", ushort.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_UInt32()
        {
            var dict = new WrappedDictionary<uint>();
            dict.Add("min_value", uint.MinValue);
            dict.Add("zero", 0);
            dict.Add("one", +1);
            dict.Add("max_value", uint.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_Float()
        {
            var dict = new WrappedDictionary<float>();
            dict.Add("min_value", float.MinValue);
            dict.Add("minus_one", -1);
            dict.Add("zero", 0);
            dict.Add("one", +1);
            dict.Add("max_value", float.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => ((double)value).ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_Double()
        {
            var dict = new WrappedDictionary<double>();
            dict.Add("min_value", double.MinValue);
            dict.Add("minus_one", -1);
            dict.Add("zero", 0);
            dict.Add("one", +1);
            dict.Add("max_value", double.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => value.ToString());
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_DateTime()
        {
            var dict = new WrappedDictionary<DateTime>();
            dict.Add("min_value", DateTime.MinValue);
            dict.Add("now", DateTime.UtcNow);
            dict.Add("max_value", DateTime.MaxValue);

            var tomlString = TomlSerializer.Serialize(dict);

            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => $"{value:yyyy-MM-dd HH:mm:ss.fffZ}");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_Enum()
        {
            var dict = new WrappedDictionary<MockEnum>();
            dict.Add("north", MockEnum.North);
            dict.Add("south", MockEnum.South);
            dict.Add("east", MockEnum.East);
            dict.Add("west", MockEnum.West);

            var tomlString = TomlSerializer.Serialize(dict);
            
            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_Enum_Flags()
        {
            var dict = new WrappedDictionary<MockStateFlags>();
            dict.Add("none", MockStateFlags.None);
            dict.Add("pending", MockStateFlags.Pending);
            dict.Add("pending_inprogress", MockStateFlags.Pending | MockStateFlags.InProgress);
            dict.Add("all", MockStateFlags.All);
            
            var tomlString = TomlSerializer.Serialize(dict);
            
            var expectedTomlString =
                dict.ToTomlStringInline("dictionary", value => $"\"{value}\"");
            Assert.AreEqual(expectedTomlString, tomlString);
        }
        
        [Test]
        public void Serialize_Table_Inline_Mixed()
        {
            var mixedDictionary = new Dictionary<string, object>();
            var mockPlayerData = new MockPlayerData("Test", 99, 42, 1000);
            
            mixedDictionary.Add("displayName", mockPlayerData.DisplayName);
            mixedDictionary.Add("level", mockPlayerData.Level);
            mixedDictionary.Add("experience", mockPlayerData.Experience);
            mixedDictionary.Add("gold", mockPlayerData.Gold);
            mixedDictionary.Add("lastLogin", mockPlayerData.LastLogin);
            
            var wrappedData = new WrappedValue<Dictionary<string, object>>(mixedDictionary);
            var tomlString = TomlSerializer.Serialize(wrappedData);
            
            var expectedToml = mockPlayerData.ToTomlStringTableInline("value");
            Assert.AreEqual(expectedToml, tomlString);
        }
    }
}
