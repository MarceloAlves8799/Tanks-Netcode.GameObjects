using TMPro;
using Unity.Collections;
using UnityEngine;

namespace Tanks
{
    public class PlayerNameDisplay : MonoBehaviour
    {

        private TankPlayer player;
        [SerializeField] private TMP_Text playerNameText;


        private void Awake()
        {
            player = GetComponentInParent<TankPlayer>();
        }

        private void OnEnable()
        {
            HandlePlayerNameChanged(string.Empty, player.PlayerName.Value);

            player.PlayerName.OnValueChanged += HandlePlayerNameChanged;
        }

        private void HandlePlayerNameChanged(FixedString32Bytes oldName, FixedString32Bytes newName)
        {
            playerNameText.SetText(newName.ToString());
        }

        private void OnDisable()
        {
            player.PlayerName.OnValueChanged -= HandlePlayerNameChanged;
        }
    }
}
