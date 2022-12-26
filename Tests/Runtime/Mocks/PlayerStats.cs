using System;

namespace UnderLogic.Serialization.Toml.Tests.Mocks
{
    [Serializable]
    internal sealed class PlayerStats
    {
        private int _health = 50;
        private int _maxHealth = 50;
        private int _mana = 50;
        private int _maxMana = 50;
        private int _strength = 3;
        private int _intellect = 3;
        private int _wisdom = 3;
        private int _constitution = 3;
        private int _dexterity = 3;
        
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
        
        public int Intellect
        {
            get => _intellect;
            set => _intellect = value;
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
        
        public int Dexterity
        {
            get => _dexterity;
            set => _dexterity = value;
        }
    }
}
