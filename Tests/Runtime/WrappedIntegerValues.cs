using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedIntegerValues
    {
        private long _decimalValue;
        
        [TomlHexNumber]
        private long _hexLowerCaseValue;
        
        [TomlHexNumber(true)]
        private long _hexUpperCaseValue;
        
        [TomlOctalNumber]
        private long _octalValue;
        
        [TomlBinaryNumber]
        private long _binaryValue;

        public long DecimalValue => _decimalValue;
        public long HexLowerCaseValue => _hexLowerCaseValue;
        public long HexUpperCaseValue => _hexUpperCaseValue;
        public long OctalValue => _octalValue;
        public long BinaryValue => _binaryValue;

        public WrappedIntegerValues() : this(0) { }

        public WrappedIntegerValues(long value)
        {
            _decimalValue = value;
            _hexLowerCaseValue = value;
            _hexUpperCaseValue = value;
            _octalValue = value;
            _binaryValue = value;
        }
    }
}
