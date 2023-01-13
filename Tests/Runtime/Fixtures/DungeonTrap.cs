using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal sealed class DungeonTrap
    {
        private string _name = "Spike Trap";
        private string _type = "Physical";
        private int _damage = 10;
        private int _x;
        private int _y;
        private int _triggerCount = 1;
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public string Type
        {
            get => _type;
            set => _type = value;
        }
        
        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
        
        public int X
        {
            get => _x;
            set => _x = value;
        }
        
        public int Y
        {
            get => _y;
            set => _y = value;
        }
        
        public int TriggerCount
        {
            get => _triggerCount;
            set => _triggerCount = value;
        }
    }
}
