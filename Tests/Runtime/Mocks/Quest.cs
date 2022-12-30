using System;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class Quest
    {
        [TomlKey("quest_name")]
        private string _name;

        [TomlKey("quest_title")]
        private string _title;

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public string Title
        {
            get => _title;
            set => _title = value;
        }
    }
}
