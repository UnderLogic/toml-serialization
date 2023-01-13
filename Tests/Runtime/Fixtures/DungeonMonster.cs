using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal sealed class DungeonMonster
    {
        private string _name;
        private int _health;
        private int _attack;
        private int _defense;
        private int _movement;

        private Dictionary<string, DungeonLoot> _loot = new Dictionary<string, DungeonLoot>();

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public int Health
        {
            get => _health;
            set => _health = value;
        }
        
        public int Attack
        {
            get => _attack;
            set => _attack = value;
        }
        
        public int Defense
        {
            get => _defense;
            set => _defense = value;
        }
        
        public int Movement
        {
            get => _movement;
            set => _movement = value;
        }

        public IDictionary<string, DungeonLoot> Loot => _loot;
    }
}
