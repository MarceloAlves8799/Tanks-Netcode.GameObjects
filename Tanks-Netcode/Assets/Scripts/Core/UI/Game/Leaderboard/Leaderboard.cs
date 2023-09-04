using Unity.Netcode;
using UnityEngine;

namespace Tanks
{
    public class Leaderboard : NetworkBehaviour
    {
        [SerializeField] private Transform leadboardEntityHolder;
        [SerializeField] private LeaderboardEntityDisplay leaderboardEntityPrefab;

        private NetworkList<LeaderboardEntityState> leaderboardEntities;


        private void Awake()
        {
            leaderboardEntities = new NetworkList<LeaderboardEntityState>();
        }
    }
}
