using System;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal struct LootTable
    {
        private string _tableName;
        private float _chance;
        private int _rolls;
        private bool _dropForAllPlayers;

        public string TableName
        {
            get => _tableName;
            set => _tableName = value;
        }

        public float Chance
        {
            get => _chance;
            set => _chance = value;
        }

        public int Rolls
        {
            get => _rolls;
            set => _rolls = value;
        }
        
        public bool DropForAllPlayers
        {
            get => _dropForAllPlayers;
            set => _dropForAllPlayers = value;
        }
    }
}
