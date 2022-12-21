using UnityEngine;
using UnityEngine.UI;

namespace UnderLogic.Serialization.Toml.Samples
{
    public class PlayerDataDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Header("User Interface")]
        [SerializeField] private Text nameText;
        [SerializeField] private Text levelText;
        [SerializeField] private Text healthText;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private Text manaText;
        [SerializeField] private Slider manaSlider;
        
        private void Update()
        {
            if (playerData == null)
                return;
            
            nameText.text = playerData.DisplayName;
            levelText.text = $"Level {playerData.Level}";
            healthText.text = $"Health: {playerData.Health} / {playerData.MaxHealth}";
            healthSlider.value = Mathf.Floor(playerData.HealthPercentage);
            manaText.text = $"Mana: {playerData.Mana} / {playerData.MaxMana}";
            manaSlider.value = Mathf.Floor(playerData.ManaPercentage);
        }
    }
}
