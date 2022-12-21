using System;
using UnityEngine;

namespace UnderLogic.Serialization.Toml.Samples
{
    // NOTE: This must be marked as Serializable to be serialized by the TOML serializer!
    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private int strength = 3;
        [SerializeField] private int dexterity = 3;
        [SerializeField] private int constitution = 3;
        [SerializeField] private int intelligence = 3;
        [SerializeField] private int wisdom = 3;
        [SerializeField] private int charisma = 3;

        public int Strength
        {
            get => strength;
            set => strength = value;
        }
        
        public int Dexterity
        {
            get => dexterity;
            set => dexterity = value;
        }
        
        public int Constitution
        {
            get => constitution;
            set => constitution = value;
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
        
        public int Charisma
        {
            get => charisma;
            set => charisma = value;
        }
    }
}
