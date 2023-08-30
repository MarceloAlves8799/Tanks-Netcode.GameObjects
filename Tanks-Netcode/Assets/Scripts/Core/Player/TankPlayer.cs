using Unity.Netcode;
using Cinemachine;
using UnityEngine;
using Unity.Collections;

namespace Tanks
{
    public class TankPlayer : NetworkBehaviour
    {
        [Header("References")]
        private CinemachineVirtualCamera virtualCamera;

        [Header("References")]
        [SerializeField] private int ownerPriority;

        public NetworkVariable<FixedString32Bytes> PlayerName = new NetworkVariable<FixedString32Bytes>();


        private void Awake()
        {
            virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                UserData userData = HostSingleton.Instance.GameManager.NetworkServer.GetUserDataByClientId(OwnerClientId);
                PlayerName.Value = userData.userName;
            }

            if (IsOwner)
            {
                virtualCamera.Priority = ownerPriority;
            }
        }
    }
}
