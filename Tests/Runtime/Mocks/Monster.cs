using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class Monster
    {
        private string _name = "Monster";
        private Dictionary<string, LootTable> _loot = new();

        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public IReadOnlyDictionary<string, LootTable> Loot => _loot;

        public void AddLoot(string key, LootTable lootTable)
        {
            if (!_loot.TryAdd(key, lootTable))
                throw new ArgumentException($"Loot table with key '{key}' already exists");
        }
    }
}
