using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal sealed class DungeonRoom
    {
        private int _id = 1;
        private string _name = "Dungeon Room";
        private string _description = "A dark and gloomy room.";
        private int _width = 8;
        private int _height = 8;
        
        private List<DungeonTrap> _traps = new List<DungeonTrap>();
        private List<DungeonMonster> _monsters = new List<DungeonMonster>();
        
        public int Id
        {
            get => _id;
            set => _id = value;
        }
        
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
        
        public int Width
        {
            get => _width;
            set => _width = value;
        }
        
        public int Height
        {
            get => _height;
            set => _height = value;
        }

        public IList<DungeonTrap> Traps => _traps;
        public IList<DungeonMonster> Monsters => _monsters;
    }
}
