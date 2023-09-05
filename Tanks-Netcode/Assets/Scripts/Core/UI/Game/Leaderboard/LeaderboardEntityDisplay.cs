using UnityEngine;
using TMPro;
using Unity.Collections;

namespace Tanks
{
    public class LeaderboardEntityDisplay : MonoBehaviour
    {
        private TMP_Text displayText;

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

            UpdateCoins(coins);
        }

        public void UpdateCoins(int coins)
        {
            Coins = coins;
            UpdateText();
        }

        private void UpdateText()
        {
            displayText.text = $"1. {PlayerName} ({Coins})";
        }


    }
}
