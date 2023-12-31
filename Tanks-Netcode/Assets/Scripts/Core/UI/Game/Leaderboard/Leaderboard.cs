using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Tanks
{
    public class Leaderboard : NetworkBehaviour
    {
        [SerializeField] private Transform leadboardEntityHolder;
        [SerializeField] private LeaderboardEntityDisplay leaderboardEntityPrefab;
        [SerializeField] private int entitiesToDisplay = 8;

        private NetworkList<LeaderboardEntityState> leaderboardEntities;
        private List<LeaderboardEntityDisplay> entityDisplays = new List<LeaderboardEntityDisplay>();


        private void Awake()
        {
            leaderboardEntities = new NetworkList<LeaderboardEntityState>();
        }

        public override void OnNetworkSpawn()
        {
            if (IsClient)
            {
                leaderboardEntities.OnListChanged += HandleLeaderboardEntitiesChanged;
                
                foreach(LeaderboardEntityState entity in leaderboardEntities)
                {
                    HandleLeaderboardEntitiesChanged(new NetworkListEvent<LeaderboardEntityState>
                    {
                        Type = NetworkListEvent<LeaderboardEntityState>.EventType.Add,
                        Value = entity
                    });
                }
            }

            if (IsServer)
            {
                TankPlayer[] players = FindObjectsOfType<TankPlayer>();
                foreach (TankPlayer player in players)
                {
                    HandlePlayerSpawned(player);
                }

                TankPlayer.OnPlayerSpawned += HandlePlayerSpawned;
                TankPlayer.OnPlayerDespawned += HandlePlayerDespawned;
            }
        }

        public override void OnNetworkDespawn()
        {
            if (IsClient)
            {
                leaderboardEntities.OnListChanged -= HandleLeaderboardEntitiesChanged;
            }

            if (IsServer)
            {
                TankPlayer.OnPlayerSpawned -= HandlePlayerSpawned;
                TankPlayer.OnPlayerDespawned -= HandlePlayerDespawned;
            }
        }

        private void HandleLeaderboardEntitiesChanged(NetworkListEvent<LeaderboardEntityState> changeEvent)
        {
            switch (changeEvent.Type)
            {
                case NetworkListEvent<LeaderboardEntityState>.EventType.Add:

                    if (!entityDisplays.Any(x => x.ClientId == changeEvent.Value.ClientId))
                    {
                        LeaderboardEntityDisplay leaderboardInstance = Instantiate(leaderboardEntityPrefab, leadboardEntityHolder);

                        leaderboardInstance.Initialize(changeEvent.Value.ClientId,
                                                       changeEvent.Value.PlayerName,
                                                       changeEvent.Value.Coins);

                        entityDisplays.Add(leaderboardInstance);
                    }

                    break;

                case NetworkListEvent<LeaderboardEntityState>.EventType.Remove:

                    LeaderboardEntityDisplay displayToRemove = entityDisplays.FirstOrDefault(x => x.ClientId == changeEvent.Value.ClientId);

                    if(displayToRemove != null)
                    {
                        displayToRemove.transform.SetParent(null);
                        Destroy(displayToRemove.gameObject);
                        entityDisplays.Remove(displayToRemove);
                    }

                    break;

                case NetworkListEvent<LeaderboardEntityState>.EventType.Value:

                    LeaderboardEntityDisplay displayToUpdate = entityDisplays.FirstOrDefault(x => x.ClientId == changeEvent.Value.ClientId);

                    if(displayToUpdate != null)
                    {
                        displayToUpdate.UpdateCoins(changeEvent.Value.Coins);
                    }

                    break;
            }

            entityDisplays.Sort((x, y) => y.Coins.CompareTo(x.Coins));

            for(int i = 0; i < entityDisplays.Count; i++)
            {
                entityDisplays[i].transform.SetSiblingIndex(i);
                entityDisplays[i].UpdateText();
                entityDisplays[i].gameObject.SetActive(i <= entitiesToDisplay - 1);
            }

            LeaderboardEntityDisplay myDisplay = entityDisplays.FirstOrDefault(x => x.ClientId == NetworkManager.Singleton.LocalClientId);

            if(myDisplay != null)
            {
                if(myDisplay.transform.GetSiblingIndex() >= entitiesToDisplay)
                {
                    leadboardEntityHolder.GetChild(entitiesToDisplay - 1).gameObject.SetActive(false);
                    myDisplay.gameObject.SetActive(true);
                }
            }
        }

        private void HandlePlayerSpawned(TankPlayer player)
        {
            leaderboardEntities.Add(new LeaderboardEntityState
            {
                ClientId = player.OwnerClientId,
                PlayerName = player.PlayerName.Value,
                Coins = 0
            });

        }

        private void HandlePlayerDespawned(TankPlayer player) 
        {
            if(leaderboardEntities == null) return;

            foreach(LeaderboardEntityState entity in leaderboardEntities)
            {
                if (entity.ClientId != player.OwnerClientId) continue;

                leaderboardEntities.Remove(entity);
                break;
            }
        }
        
    }
}
