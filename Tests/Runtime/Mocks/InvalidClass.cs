using System;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal class InvalidClass
    {
        private string _name = "Underscore Name";
        private string name = "Name";
    }
}
