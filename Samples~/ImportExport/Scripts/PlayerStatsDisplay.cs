using UnityEngine;
using UnityEngine.UI;

namespace UnderLogic.Serialization.Toml.Samples
{
    public class PlayerStatsDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Header("User Interface")]
        [SerializeField] private Text strengthText;
        [SerializeField] private Text dexterityText;
        [SerializeField] private Text intelligenceText;
        [SerializeField] private Text wisdomText;
        [SerializeField] private Text constitutionText;
        [SerializeField] private Text charismaText;

        private void Update()
        {
            if (playerData == null)
                return;

            strengthText.text = $"STR: {playerData.Stats.Strength}";
            dexterityText.text = $"DEX: {playerData.Stats.Dexterity}";
            intelligenceText.text = $"INT: {playerData.Stats.Intelligence}";
            wisdomText.text = $"WIS: {playerData.Stats.Wisdom}";
            constitutionText.text = $"CON: {playerData.Stats.Constitution}";
            charismaText.text = $"CHA: {playerData.Stats.Charisma}";
        }
    }
}
