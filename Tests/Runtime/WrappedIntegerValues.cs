using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedIntegerValues
    {
        [TomlNumberFormat(NumberFormat.Decimal)]
        private long _decimalValue;
        
        [TomlNumberFormat(NumberFormat.HexLowerCase)]
        private long _hexLowerCaseValue;
        
        [TomlNumberFormat(NumberFormat.HexUpperCase)]
        private long _hexUpperCaseValue;
        
        [TomlNumberFormat(NumberFormat.Octal)]
        private long _octalValue;
        
        [TomlNumberFormat(NumberFormat.Binary)]
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
