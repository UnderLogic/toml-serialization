using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    [TomlCasing(StringCasing.Default)]
    internal sealed class WrappedCasedValues<T>
    {
        [TomlCasing(StringCasing.LowerCase)]
        private T _lowerValue;
        
        [TomlCasing(StringCasing.UpperCase)]
        private T _upperValue;
        
        [TomlCasing(StringCasing.CamelCase)]
        private T _CamelValue;
        
        [TomlCasing(StringCasing.PascalCase)]
        private T _pascalValue;
        
        [TomlCasing(StringCasing.SnakeCase)]
        private T _snakeValue;
        
        [TomlCasing(StringCasing.KebabCase)]
        private T _kebabValue;
        
        public T LowerValue
        {
            get => _lowerValue;
            set => _lowerValue = value;
        }
        
        public T UpperValue
        {
            get => _upperValue;
            set => _upperValue = value;
        }
        
        public T CamelValue
        {
            get => _CamelValue;
            set => _CamelValue = value;
        }
        
        public T PascalValue
        {
            get => _pascalValue;
            set => _pascalValue = value;
        }
        
        public T SnakeValue
        {
            get => _snakeValue;
            set => _snakeValue = value;
        }
        
        public T KebabValue
        {
            get => _kebabValue;
            set => _kebabValue = value;
        }

        public WrappedCasedValues() { }

        public WrappedCasedValues(T value)
        {
            _lowerValue = value;
            _upperValue = value;
            _CamelValue = value;
            _pascalValue = value;
            _snakeValue = value;
            _kebabValue = value;
        }
    }
}
