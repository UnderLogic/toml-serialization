using System;

namespace UnderLogic.Serialization.Toml.Tests
{
    [Serializable]
    internal class MockUnorderedStats
    {
        private int _health = 100;
        private int _maxHealth = 100;
        private int _mana = 100;
        private int _maxMana = 100;
        private int _strength = 3;
        private int _dexterity = 3;
        private int _intelligence = 3;
        private int _wisdom = 3;
        private int _constitution = 3;
        private int _charisma = 3;
        private int _luck = 50;

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

        public int Strength
        {
            get => _strength;
            set => _strength = value;
        }

        public int Dexterity
        {
            get => _dexterity;
            set => _dexterity = value;
        }

        public int Intelligence
        {
            get => _intelligence;
            set => _intelligence = value;
        }

        public int Wisdom
        {
            get => _wisdom;
            set => _wisdom = value;
        }

        public int Constitution
        {
            get => _constitution;
            set => _constitution = value;
        }

        public int Charisma
        {
            get => _charisma;
            set => _charisma = value;
        }

        public int Luck
        {
            get => _luck;
            set => _luck = value;
        }
    }
}
