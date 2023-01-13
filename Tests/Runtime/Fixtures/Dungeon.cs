using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal sealed class Dungeon
    {
        private string _name = "Dungeon";
        private string _description = "A dark and scary dungeon.";
        private int _minLevel = 1;
        private int _maxLevel = 10;
        
        private List<DungeonRoom> _rooms = new List<DungeonRoom>();
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        
        public int MinLevel
        {
            get => _minLevel;
            set => _minLevel = value;
        }
        
        public int MaxLevel
        {
            get => _maxLevel;
            set => _maxLevel = value;
        }

        public IList<DungeonRoom> Rooms => _rooms;
    }
}
