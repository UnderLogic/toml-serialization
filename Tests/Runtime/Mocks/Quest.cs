using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class Quest
    {
        [TomlKey("quest_name")]
        private string _name;

        [TomlKey("quest_title")]
        private string _title;

        [TomlKey("quest_description")]
        private string _description;

        [TomlKey("quest_objectives")]
        private List<string> _objectives = new();

        [TomlKey("reward")]
        private int _goldReward;
        
        [TomlKey("repeatable")]
        private bool _isRepeatable;

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

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public List<string> Objectives
        {
            get => _objectives;
            set => _objectives = value;
        }
        
        public int GoldReward
        {
            get => _goldReward;
            set => _goldReward = value;
        }

        public bool IsRepeatable
        {
            get => _isRepeatable;
            set => _isRepeatable = value;
        }
    }
}
