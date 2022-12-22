using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class MockInvalidClass
    {
        private string _name = "Underscore Name";
        private string name = "Name";
    }
}
