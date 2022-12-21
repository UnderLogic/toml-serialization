using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace UnderLogic.Serialization.Toml.Samples
{
    // NOTE: This must be marked as Serializable to be serialized by the TOML serializer!
    [Serializable]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private string playerName;
        [SerializeField] private int level = 1;
        
        [Header("Health")]
        [SerializeField] private int health = 100;
        [SerializeField] private int maxHealth = 100;

        [Space]
        [NonSerialized]
        public UnityEvent onSave;

        public string PlayerName => playerName;
        public int Level => level;
        public int Health => health;
        public int MaxHealth => maxHealth;

        public float HealthPercentage => maxHealth > 0 ? (health * 100f) / maxHealth : 0;
        
        public void SaveToFile()
        {
            var filePath = Path.Combine(Application.persistentDataPath, "player.toml");

            using (var stream = File.OpenWrite(filePath))
                TomlSerializer.Serialize(stream, this);
            
            Debug.Log($"Saved player data to {filePath}", this);
            onSave?.Invoke();
        }
    }
}
