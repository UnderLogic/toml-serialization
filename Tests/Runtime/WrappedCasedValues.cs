using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedCasedValues<T>
    {
        [TomlCamelCase]
        private T _CamelValue;
        
        [TomlPascalCase]
        private T _pascalValue;
        
        [TomlSnakeCase(false)]
        private T _snakeValue;
        
        [TomlSnakeCase(true)]
        private T _upperSnakeValue;
        
        [TomlKebabCase]
        private T _kebabValue;

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
        
        public T UpperSnakeValue
        {
            get => _upperSnakeValue;
            set => _upperSnakeValue = value;
        }
        
        public T KebabValue
        {
            get => _kebabValue;
            set => _kebabValue = value;
        }

        public WrappedCasedValues() { }

        public WrappedCasedValues(T value)
        {
            _CamelValue = value;
            _pascalValue = value;
            _snakeValue = value;
            _upperSnakeValue = value;
            _kebabValue = value;
        }
    }
}
