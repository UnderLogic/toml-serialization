using System.Globalization;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_NullFloatList_ShouldSetNull()
        {
            var toml = "list = null\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<float>>(toml);
            Assert.That(deserializedList.IsNull, Is.EqualTo(true), "List should be null");
        }
        
        [Test]
        public void Deserialize_EmptyFloatList_ShouldSetEmpty()
        {
            var toml = "list = []\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<float>>(toml);
            Assert.That(deserializedList.Count, Is.EqualTo(0), "List should be null");
        }
        
        [Test]
        public void Deserialize_FloatList_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14f, 0, 3.14f };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<float>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_FloatListMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14f, 0, 3.14f };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<float>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_FloatListSpecials_ShouldSetValues()
        {
            var expectedValues = new[] { float.NaN, float.NegativeInfinity, float.PositiveInfinity };
            var expectedValueStrings = new[] { "nan", "-inf", "+inf" };
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<float>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }
        
        [Test]
        public void Deserialize_NullDoubleList_ShouldSetNull()
        {
            var toml = "list = null\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<double>>(toml);
            Assert.That(deserializedList.IsNull, Is.EqualTo(true), "List should be null");
        }
        
        [Test]
        public void Deserialize_EmptyDoubleList_ShouldSetEmpty()
        {
            var toml = "list = []\n";
            var deserializedList = TomlSerializer.Deserialize<SerializableList<double>>(toml);
            Assert.That(deserializedList.Count, Is.EqualTo(0), "List should be null");
        }

        [Test]
        public void Deserialize_DoubleList_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14, 0, 3.14 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<double>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_DoubleListMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14, 0, 3.14 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"list = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<double>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_DoubleListSpecials_ShouldSetValues()
        {
            var expectedValues = new[] { double.NaN, double.NegativeInfinity, double.PositiveInfinity };
            var expectedValueStrings = new[] { "nan", "-inf", "+inf" };
            var toml = $"list = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedList = TomlSerializer.Deserialize<SerializableList<double>>(toml);
            Assert.That(deserializedList, Is.EqualTo(expectedValues));
        }
    }
}
