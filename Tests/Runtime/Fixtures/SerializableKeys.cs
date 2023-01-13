using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal class SerializableKeys
    {
        [TomlKey("renamedValue")]
        private string _value;

        [TomlCamelCase]
        private string _camel_case_value;

        [TomlPascalCase]
        private string _pascal_case_value;

        [TomlSnakeCase]
        private string _snakeCaseValue;

        [TomlSnakeCase(true)]
        private string _upperSnakeCaseValue;

        [TomlKebabCase]
        private string _kebabCaseValue;

        public string Value
        {
            get => _value;
            set => _value = value;
        }
        
        public string CamelCaseValue
        {
            get => _camel_case_value;
            set => _camel_case_value = value;
        }
        
        public string PascalCaseValue
        {
            get => _pascal_case_value;
            set => _pascal_case_value = value;
        }
        
        public string SnakeCaseValue
        {
            get => _snakeCaseValue;
            set => _snakeCaseValue = value;
        }
        
        public string UpperSnakeCaseValue
        {
            get => _upperSnakeCaseValue;
            set => _upperSnakeCaseValue = value;
        }
        
        public string KebabCaseValue
        {
            get => _kebabCaseValue;
            set => _kebabCaseValue = value;
        }

        public SerializableKeys() : this(string.Empty) { }

        public SerializableKeys(string value)
        {
            _value = value;
            _camel_case_value = value;
            _pascal_case_value = value;
            _snakeCaseValue = value;
            _upperSnakeCaseValue = value;
            _kebabCaseValue = value;
        }
    }
}
