using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Mocks;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullDict_ShouldSerializeNull()
        {
            var dict = WrappedDictionary<string>.Null();
            var toml = TomlSerializer.Serialize(dict);

            Assert.AreEqual("dictionary = null\n", toml);
        }

        [Test]
        public void Serialize_EmptyDict_ShouldSerializeEmpty()
        {
            var dict = new WrappedDictionary<string>();
            var toml = TomlSerializer.Serialize(dict);

            Assert.AreEqual("dictionary = {}\n", toml);
        }

        [Test]
        public void Serialize_BoolDict_ShouldSerializeLowerCase()
        {
            var dict = new WrappedDictionary<bool>
            {
                { "true", true },
                { "false", false }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value.ToString().ToLowerInvariant()}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_CharDict_ShouldSerializeQuoted()
        {
            var dict = new WrappedDictionary<char>
            {
                { "letter_a", 'A' },
                { "letter_z", 'Z' },
                { "digit_0", '0' },
                { "digit_9", '9' },
                { "underscore", '_' }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = \"{pair.Value}\"");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_StringDict_ShouldSerializeQuoted()
        {
            var dict = new WrappedDictionary<string>
            {
                { "hello", "world" },
                { "foo", "bar" },
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = \"{pair.Value}\"");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_EnumDict_ShouldSerializeQuoted()
        {
            var dict = new WrappedDictionary<Direction>
            {
                { "none", Direction.None },
                { "up", Direction.Up },
                { "down", Direction.Down },
                { "left", Direction.Left },
                { "right", Direction.Right },
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = \"{pair.Value:F}\"");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_EnumFlagsDict_ShouldSerializeQuoted()
        {
            var dict = new WrappedDictionary<StatusEffects>
            {
                { "none", StatusEffects.None },
                { "poison", StatusEffects.Poison },
                { "blind", StatusEffects.Blind },
                { "immobile", StatusEffects.Frozen | StatusEffects.Sleep | StatusEffects.Stun },
                { "all", StatusEffects.All },
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = \"{pair.Value:F}\"");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_Int8Dict_ShouldSerializeLiteral()
        {
            var dict = new WrappedDictionary<sbyte>
            {
                { "min_value", sbyte.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", sbyte.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_Int16Dict_ShouldSerializeLiteral()
        {
            var dict = new WrappedDictionary<short>
            {
                { "min_value", short.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", short.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_Int32Dict_ShouldSerializeLiteral()
        {
            var dict = new WrappedDictionary<int>
            {
                { "min_value", int.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", int.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_Int64Dict_ShouldSerializeLiteral()
        {
            var dict = new WrappedDictionary<long>
            {
                { "min_value", long.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", long.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_UInt8Dict_ShouldSerializeLiteral()
        {
            var dict = new WrappedDictionary<byte>
            {
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", byte.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_UInt16Dict_ShouldSerializeLiteral()
        {
            var dict = new WrappedDictionary<ushort>
            {
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", ushort.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_UInt32Dict_ShouldSerializeLiteral()
        {
            var dict = new WrappedDictionary<uint>
            {
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", uint.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_FloatDict_ShouldSerializeLiteral()
        {
            var dict = new WrappedDictionary<float>
            {
                { "min_value", float.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", float.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {(double)pair.Value}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_DoubleDict_ShouldSerializeLiteral()
        {
            var dict = new WrappedDictionary<double>
            {
                { "min_value", double.MinValue },
                { "minus_one", -1 },
                { "zero", 0 },
                { "plus_one", 1 },
                { "max_value", double.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_DateTimeDict_ShouldSerializeIsoFormat()
        {
            var dict = new WrappedDictionary<DateTime>
            {
                { "min_value", DateTime.MinValue },
                { "now", DateTime.Now },
                { "utc_now", DateTime.UtcNow },
                { "unix_epoch", DateTime.UnixEpoch },
                { "max_value", DateTime.MaxValue }
            };

            var toml = TomlSerializer.Serialize(dict);

            var pairStrings = dict.Select(pair => $"{pair.Key} = {pair.Value:yyyy-MM-dd HH:mm:ss.fffZ}");
            var dictString = string.Join(", ", pairStrings);

            Assert.AreEqual($"dictionary = {{ {dictString} }}\n", toml);
        }

        [Test]
        public void Serialize_MixedDict_ShouldSerializeInline()
        {
            var location = new PlayerLocation
            {
                Map = 500,
                X = 24,
                Y = 42,
                ZIndex = 1
            };

            var rewards = new Dictionary<string, string>();
            rewards.Add("option_1", "sword");
            rewards.Add("option_2", "axe");
            rewards.Add("option_3", "dagger");

            var dict = new WrappedDictionary<object>
            {
                { "questName", "Test Quest" },
                { "questId", 1 },
                { "levelRange", new[] { 10, 15 } },
                { "hazards", StatusEffects.Poison },
                { "rewards", rewards },
                { "location", location },
                { "startedAt", new DateTime(2022, 10, 1) }
            };

            var toml = TomlSerializer.Serialize(dict);

            Assert.AreEqual(
                "dictionary = { questName = \"Test Quest\", questId = 1, levelRange = [ 10, 15 ], hazards = \"Poison\", rewards = { option_1 = \"sword\", option_2 = \"axe\", option_3 = \"dagger\" }, location = { map = 500, x = 24, y = 42, zIndex = 1 }, startedAt = 2022-10-01 00:00:00.000Z }\n",
                toml);
        }
    }
}
