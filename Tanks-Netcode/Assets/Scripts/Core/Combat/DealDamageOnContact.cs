using UnityEngine;
using Unity.Netcode;

namespace Tanks
{
    public class DealDamageOnContact : MonoBehaviour
    {
        [SerializeField] private int damage;

        private ulong clientId;

        public void SetOwner(ulong ownerClientId)
        {
            clientId = ownerClientId;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.attachedRigidbody == null) return; 

            if(other.attachedRigidbody.TryGetComponent<NetworkObject>(out NetworkObject networkObj))
            {
                if (clientId.Equals(networkObj.OwnerClientId))
                    return;
            }

            if(other.attachedRigidbody.TryGetComponent<Health>(out Health health))
            {
                health.TakeDamage(damage);
            }
        }
    }
}
