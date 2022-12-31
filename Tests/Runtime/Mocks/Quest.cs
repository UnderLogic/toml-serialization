using System;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class Quest
    {
        private string _key = "quest_key";

        [TomlLiteral]
        private string _scriptPath = @"scripts/quests/gather_wood.lua";

        private string _title = "Starter Quest";

        [TomlMultiline]
        private string _description = "This is a starter quest.\nYou can complete it by talking to the quest giver.";

        [TomlLiteral]
        [TomlMultiline]
        private string _summary = "Hey adventurer!\nDo you have a minute?\nI need you to gather me some wood.";

        private int _minLevel;
        private bool _repeatable;

        public string Key
        {
            get => _key;
            set => _key = value;
        }
        
        public string ScriptPath
        {
            get => _scriptPath;
            set => _scriptPath = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }
        
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        
        public string Summary
        {
            get => _summary;
            set => _summary = value;
        }
        
        public int MinLevel
        {
            get => _minLevel;
            set => _minLevel = value;
        }
        
        public bool Repeatable
        {
            get => _repeatable;
            set => _repeatable = value;
        }
    }
}
