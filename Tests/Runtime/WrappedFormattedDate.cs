using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal sealed class WrappedFormattedDate
    {
        [TomlDateTimeFormat("yyyy-MM-dd")]
        private DateTime _value;
        
        public DateTime Value
        {
            get => _value;
            set => _value = value;
        }
        
        public WrappedFormattedDate(DateTime dateTime)
        {
            _value = dateTime;
        }
    }
}
