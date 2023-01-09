using System.Globalization;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Deserialize_FloatArray_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14f, 0, 3.14f };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<float>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_FloatArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14f, 0, 3.14f };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<float>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_FloatArrayScientific_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14e-10f, 0, 3.14e-10f };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<float>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_FloatArraySpecials_ShouldSetValues()
        {
            var expectedValues = new[] { float.NaN, float.NegativeInfinity, float.PositiveInfinity };
            var expectedValueStrings = new[] { "nan", "-inf", "+inf" };
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<float>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_DoubleArray_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14, 0, 3.14 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<double>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_DoubleArrayMultiline_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14, 0, 3.14 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [\n{string.Join(",\n", expectedValueStrings)}\n]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<double>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_DoubleArrayScientific_ShouldSetValues()
        {
            var expectedValues = new[] { -3.14e-10, 0, 3.14e-10 };
            var expectedValueStrings = expectedValues.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<double>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }

        [Test]
        public void Deserialize_DoubleArraySpecials_ShouldSetValues()
        {
            var expectedValues = new[] { double.NaN, double.NegativeInfinity, double.PositiveInfinity };
            var expectedValueStrings = new[] { "nan", "-inf", "+inf" };
            var toml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";

            var deserializedArray = TomlSerializer.Deserialize<SerializableArray<double>>(toml);
            Assert.That(deserializedArray, Is.EqualTo(expectedValues));
        }
    }
}
