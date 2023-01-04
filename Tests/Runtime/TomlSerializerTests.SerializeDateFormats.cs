using System;
using NUnit.Framework;

namespace UnderLogic.Serialization.Toml.Tests
{
    internal partial class TomlSerializerTests
    {
        [Test]
        public void Serialize_DateTimeFormat_ShouldFormatDateTime()
        {
            var wrappedDate = new WrappedFormattedDate(new DateTime(1979, 5, 27));
            var toml = TomlSerializer.Serialize(wrappedDate);
            
            Assert.AreEqual("value = 1979-05-27\n", toml);
        }
    }
}
