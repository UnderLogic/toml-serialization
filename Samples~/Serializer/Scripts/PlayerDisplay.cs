using UnityEngine;
using UnityEngine.UI;

namespace UnderLogic.Serialization.Toml.Samples
{
    public class PlayerDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerController player;

        [Header("User Interface")]
        [SerializeField] private Text nameText;
        [SerializeField] private Text levelText;
        [SerializeField] private Text healthText;
        [SerializeField] private Slider healthSlider;

        private void Update()
        {
            if (player == null)
                return;
            
            nameText.text = player.PlayerName;
            levelText.text = $"Level {player.Level}";
            healthText.text = $"Health: {player.Health} / {player.MaxHealth}";
            healthSlider.value = Mathf.Floor(player.HealthPercentage);
        }
    }
}
