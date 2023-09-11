using UnityEngine;
using TMPro;
using Unity.Collections;
using Unity.Netcode;

namespace Tanks
{
    public class LeaderboardEntityDisplay : MonoBehaviour
    {
        private TMP_Text displayText;

        [SerializeField] private Color textColor;

        public ulong ClientId { get; private set; }
        public FixedString32Bytes PlayerName { get; private set; }
        public int Coins { get; private set; }


        private void Awake()
        {
            displayText = GetComponent<TMP_Text>();
        }

        public void Initialize(ulong clientId, FixedString32Bytes playerName, int coins)
        {
            ClientId = clientId;
            PlayerName = playerName;

            if(clientId == NetworkManager.Singleton.LocalClientId)
            {
                displayText.color = textColor;
            }

            UpdateCoins(coins);
        }

        public void UpdateCoins(int coins)
        {
            Coins = coins;
            UpdateText();
        }

        public void UpdateText()
        {
            displayText.text = $"{transform.GetSiblingIndex() + 1}. {PlayerName} ({Coins})";
        }


    }
}
