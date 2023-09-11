using Unity.Netcode;
using Cinemachine;
using UnityEngine;
using Unity.Collections;
using System;

namespace Tanks
{
    public class TankPlayer : NetworkBehaviour
    {

        public static event Action<TankPlayer> OnPlayerSpawned;
        public static event Action<TankPlayer> OnPlayerDespawned;


        [Header("References")]
        private CinemachineVirtualCamera virtualCamera;
        public Health Health { get; private set; }

        [Header("Settings")]
        [SerializeField] private int ownerPriority;

        public NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>();


        private void Awake()
        {
            virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            Health = GetComponent<Health>();
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                UserData userData = HostSingleton.Instance.GameManager.NetworkServer.GetUserDataByClientId(OwnerClientId);
                PlayerName.Value = userData.userName;

                OnPlayerSpawned?.Invoke(this);
            }

            if (IsOwner)
            {
                virtualCamera.Priority = ownerPriority;
            }
        }

        public override void OnNetworkDespawn()
        {
            if (IsServer)
            {
                OnPlayerDespawned?.Invoke(this);
            }
        }
    }
}
