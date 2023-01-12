using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal sealed class PlayerCharacter
    {
        private string _name = "Player";
        private int _level = 1;
        private int _health= 100;
        private int _maxHealth = 100;
        private int _mana = 50;
        private int _maxMana = 50;

        private PlayerStats _stats = new PlayerStats();
        private int _statPoints;

        private List<InventoryItem> _inventory = new List<InventoryItem>();
        private int gold;
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public int Level
        {
            get => _level;
            set => _level = value;
        }
        
        public int Health
        {
            get => _health;
            set => _health = value;
        }
        
        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }
        
        public int Mana
        {
            get => _mana;
            set => _mana = value;
        }
        
        public int MaxMana
        {
            get => _maxMana;
            set => _maxMana = value;
        }
        
        public PlayerStats Stats
        {
            get => _stats;
            set => _stats = value;
        }
        
        public int StatPoints
        {
            get => _statPoints;
            set => _statPoints = value;
        }

        public List<InventoryItem> Inventory => _inventory;
        
        public int Gold
        {
            get => gold;
            set => gold = value;
        }
    }
}
