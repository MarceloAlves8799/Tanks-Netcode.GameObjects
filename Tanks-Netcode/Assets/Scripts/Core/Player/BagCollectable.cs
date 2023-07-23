using Unity.Netcode;
using UnityEngine;

namespace Tanks
{
    
    public class BagCollectable : NetworkBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                if (!IsServer) return;

                collectable.OnCollect();
            }
        }
    }
}

