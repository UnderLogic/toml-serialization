using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableIntegers
    {
        private long _decimalValue;

        [TomlBinaryNumber]
        private long _binaryNumber;

        [TomlOctalNumber]
        private long _octalNumber;

        [TomlHexNumber]
        private long _lowerHexNumber;

        [TomlHexNumber(true)]
        private long _upperHexNumber;
        
        public long DecimalValue
        {
            get => _decimalValue;
            set => _decimalValue = value;
        }
        
        public long BinaryNumber
        {
            get => _binaryNumber;
            set => _binaryNumber = value;
        }
        
        public long OctalNumber
        {
            get => _octalNumber;
            set => _octalNumber = value;
        }
        
        public long LowerHexNumber
        {
            get => _lowerHexNumber;
            set => _lowerHexNumber = value;
        }
        
        public long UpperHexNumber
        {
            get => _upperHexNumber;
            set => _upperHexNumber = value;
        }

        public SerializableIntegers() : this(0) { }

        public SerializableIntegers(long initialValue)
        {
            _decimalValue = initialValue;
            _binaryNumber = initialValue;
            _octalNumber = initialValue;
            _lowerHexNumber = initialValue;
            _upperHexNumber = initialValue;
        }
    }
}
