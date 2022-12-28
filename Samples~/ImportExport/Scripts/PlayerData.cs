using System;
using UnityEngine;

namespace UnderLogic.Serialization.Toml.Samples
{
    // NOTE: This must be marked as Serializable to be serialized by the TOML serializer!
    [Serializable]
    [CreateAssetMenu(menuName = "Data/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [NonSerialized] private string _filename = "player_data.toml";

        [Header("Info")]
        [SerializeField] private string displayName = "Slade";
        [SerializeField] private int level = 1;

        [Header("Health")]
        [SerializeField] private int health = 50;
        [SerializeField] private int maxHealth = 50;

        [Header("Mana")]
        [SerializeField] private int mana = 25;
        [SerializeField] private int maxMana = 25;

        [Header("Stats")]
        [SerializeField] private PlayerStats stats;

        public string SaveFileName
        {
            get => _filename;
            set => _filename = value;
        }
        
        public string DisplayName => displayName;
        public int Level => level;
        
        public int Health => health;
        public int MaxHealth => maxHealth;
        public float HealthPercentage => MaxHealth > 0 ? Health * 100f / MaxHealth : 0;
        
        public int Mana => mana;
        public int MaxMana => maxMana;
        public float ManaPercentage => MaxMana > 0 ? Mana * 100f / MaxMana : 0;
        
        public PlayerStats Stats => stats;
    }
}
