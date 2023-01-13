using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal sealed class DungeonLoot
    {
        private string _lootTable;
        private int _dropChance;
        private int _rolls;
        private bool _dropsForAllPlayers;
        
        public string LootTable
        {
            get => _lootTable;
            set => _lootTable = value;
        }
        
        public int DropChance
        {
            get => _dropChance;
            set => _dropChance = value;
        }
        
        public int Rolls
        {
            get => _rolls;
            set => _rolls = value;
        }
        
        public bool DropsForAllPlayers
        {
            get => _dropsForAllPlayers;
            set => _dropsForAllPlayers = value;
        }
    }
}
