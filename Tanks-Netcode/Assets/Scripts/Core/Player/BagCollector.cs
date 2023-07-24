using Unity.Netcode;
using UnityEngine;

namespace Tanks
{
    
    public class BagCollector : NetworkBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<ICollectable>(out ICollectable collectable)) return;

            if (!IsServer) return;

            collectable.OnCollect();
        }
    }
}

