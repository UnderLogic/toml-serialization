using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal class MockUnorderedItem
    {
        private int _index;
        private string _lootTable;
        private float _dropChance;
        private bool _canPickpocket;
        
        public int Index
        {
            get => _index;
            set => _index = value;
        }
        
        public string LootTable
        {
            get => _lootTable;
            set => _lootTable = value;
        }
        
        public float DropChance
        {
            get => _dropChance;
            set => _dropChance = value;
        }
        
        public bool CanPickpocket
        {
            get => _canPickpocket;
            set => _canPickpocket = value;
        }
    }
}
