using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal sealed class EquipmentItem
    {
        private string _name = "Gear";
        private int _attackPower;
        private int _armorClass;
        private int _spellPower;
        private int _magicResist;
        private int _durability = 100;
        private int _maxDurability = 100;
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public int AttackPower
        {
            get => _attackPower;
            set => _attackPower = value;
        }
        
        public int ArmorClass
        {
            get => _armorClass;
            set => _armorClass = value;
        }
        
        public int SpellPower
        {
            get => _spellPower;
            set => _spellPower = value;
        }
        
        public int MagicResist
        {
            get => _magicResist;
            set => _magicResist = value;
        }
        
        public int Durability
        {
            get => _durability;
            set => _durability = value;
        }
        
        public int MaxDurability
        {
            get => _maxDurability;
            set => _maxDurability = value;
        }

        public bool IsEquivalentTo(EquipmentItem other)
        {
            return other.Name == Name &&
                   other.AttackPower == AttackPower &&
                   other.ArmorClass == ArmorClass &&
                   other.SpellPower == SpellPower &&
                   other.MagicResist == MagicResist &&
                   other.Durability == Durability &&
                   other.MaxDurability == MaxDurability;
        }
    }
}
