using System;
using System.Collections.Generic;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    public class MockNestedClass
    {
        private int _id = 1;
        private string _name = "Player";
        private int _level = 1;
        private int _health = 50;
        private int _maxHealth = 50;
        private int _mana = 25;
        private int _maxMana = 25;
        private int _gold;
        private int _experience;

        private List<MockNestedItem> _inventory = new();

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

        public ICollection<MockNestedItem> Inventory => _inventory;

        public void AddItem(MockNestedItem item)
        {
            if (_inventory.Contains(item))
                return;

            _inventory.Add(item);
        }
    }
}
