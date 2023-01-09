using System.Globalization;
using System.Linq;
using NUnit.Framework;
using UnderLogic.Serialization.Toml.Tests.Fixtures;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_NullFloatArray_ShouldBeNull()
        {
            var array = SerializableArray<float>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyFloatArray_ShouldBeEmpty()
        {
            var array = SerializableArray<float>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatArray_ShouldBeLiterals()
        {
            var array = SerializableArray<float>.WithValues(-3.14f, 0f, 3.14f);
            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = array.Select(x => ((double)x).ToString(CultureInfo.InvariantCulture));
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_FloatSpecialsArray_ShouldBeLiterals()
        {
            var array = SerializableArray<float>.WithValues(float.NaN, float.NegativeInfinity, float.PositiveInfinity);
            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = new[] { "nan", "-inf", "+inf" };
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_NullDoubleArray_ShouldBeNull()
        {
            var array = SerializableArray<double>.Null();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = null\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_EmptyDoubleArray_ShouldBeEmpty()
        {
            var array = SerializableArray<double>.Empty();
            var toml = TomlSerializer.Serialize(array);

            var expectedToml = "array = []\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }

        [Test]
        public void Serialize_DoubleArray_ShouldBeLiterals()
        {
            var array = SerializableArray<double>.WithValues(-3.14, 0, 3.14);
            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = array.Select(x => x.ToString(CultureInfo.InvariantCulture));
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
        
        [Test]
        public void Serialize_DoubleSpecialsArray_ShouldBeLiterals()
        {
            var array = SerializableArray<double>.WithValues(double.NaN, double.NegativeInfinity,
                double.PositiveInfinity);
            var toml = TomlSerializer.Serialize(array);

            var expectedValueStrings = new[] { "nan", "-inf", "+inf" };
            var expectedToml = $"array = [ {string.Join(", ", expectedValueStrings)} ]\n";
            Assert.That(toml, Is.EqualTo(expectedToml));
        }
    }
}
