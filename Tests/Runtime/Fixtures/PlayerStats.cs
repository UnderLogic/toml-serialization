using System;

namespace UnderLogic.Serialization.Toml.Tests.Fixtures
{
    [Serializable]
    internal sealed class PlayerStats
    {
        private int strength = 3;
        private int intelligence = 3;
        private int wisdom = 3;
        private int constitution = 3;
        private int dexterity = 3;
        
        public int Strength
        {
            get => strength;
            set => strength = value;
        }
        
        public int Intelligence
        {
            get => intelligence;
            set => intelligence = value;
        }
        
        public int Wisdom
        {
            get => wisdom;
            set => wisdom = value;
        }
        
        public int Constitution
        {
            get => constitution;
            set => constitution = value;
        }
        
        public int Dexterity
        {
            get => dexterity;
            set => dexterity = value;
        }
    }
}
