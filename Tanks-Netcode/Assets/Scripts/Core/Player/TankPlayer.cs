using Unity.Netcode;
using Cinemachine;
using UnityEngine;

namespace Tanks
{
    public class TankPlayer : NetworkBehaviour
    {
        [Header("References")]
        private CinemachineVirtualCamera virtualCamera;

        [Header("References")]
        [SerializeField] private int ownerPriority;


        private void Awake()
        {
            virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        }

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                virtualCamera.Priority = ownerPriority;
            }
        }
    }
}
