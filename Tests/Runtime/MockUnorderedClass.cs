using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal class MockUnorderedClass
    {
        private int _id;
        private string _name;
        private float _aggroRadius;

        private Dictionary<string, float> _aggroTable = new();
        private string _currentTarget;

        private List<MockUnorderedItem> _loot = new();
        private int _gold;
        private int _experience;

        private MockUnorderedStats _stats;
        
        private DateTime _spawnedAt = DateTime.Now;
        private DateTime _despawnAt = DateTime.Now + TimeSpan.FromMinutes(5);

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

        public float AggroRadius
        {
            get => _aggroRadius;
            set => _aggroRadius = value;
        }

        public IReadOnlyDictionary<string, float> AggroTable => _aggroTable;

        public string CurrentTarget
        {
            get => _currentTarget;
            set => _currentTarget = value;
        }

        public IReadOnlyList<MockUnorderedItem> Loot => _loot;

        public int Gold
        {
            get => _gold;
            set => _gold = value;
        }
        
        public int Experience
        {
            get => _experience;
            set => _experience = value;
        }
        
        public MockUnorderedStats Stats
        {
            get => _stats;
            set => _stats = value;
        }

        public DateTime SpawnedAt
        {
            get => _spawnedAt;
            set => _spawnedAt = value;
        }

        public DateTime DespawnAt
        {
            get => _despawnAt;
            set => _despawnAt = value;
        }

        public void AddLoot(MockUnorderedItem item)
        {
            if (!_loot.Contains(item))
                _loot.Add(item);
        }

        public void SetAggro(string name, float value)
        {
            if (_aggroTable.ContainsKey(name))
                _aggroTable[name] = value;
            else
                _aggroTable.Add(name, value);
        }
    }
}
