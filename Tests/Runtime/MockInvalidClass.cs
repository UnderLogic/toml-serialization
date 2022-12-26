using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal class MockInvalidClass
    {
        private string _name = "Underscore Name";
        private string name = "Name";
    }
}
