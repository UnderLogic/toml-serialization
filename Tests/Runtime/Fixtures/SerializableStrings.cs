using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableStrings
    {
        private string _basicString;
        
        [TomlLiteral]
        private string _literalString;

        [TomlLiteral]
        [TomlMultiline]
        private string _multilineLiteralString;

        [TomlMultiline]
        private string _multilineBasicString;
        
        public string BasicString
        {
            get => _basicString;
            set => _basicString = value;
        }
        
        public string LiteralString
        {
            get => _literalString;
            set => _literalString = value;
        }
        
        public string MultilineLiteralString
        {
            get => _multilineLiteralString;
            set => _multilineLiteralString = value;
        }
        
        public string MultilineBasicString
        {
            get => _multilineBasicString;
            set => _multilineBasicString = value;
        }

        public SerializableStrings() : this(string.Empty) { }

        public SerializableStrings(string value)
        {
            _basicString = value;
            _literalString = value;
            _multilineLiteralString = value;
            _multilineBasicString = value;
        }
    }
}
