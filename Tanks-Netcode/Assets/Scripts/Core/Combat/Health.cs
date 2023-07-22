using System;
using Unity.Netcode;
using UnityEngine;

namespace Tanks
{
    public class Health : NetworkBehaviour
    {
        public Action<Health> OnDie;

        [field: SerializeField] public int MaxHealth { get; private set; } = 100;

        public NetworkVariable<int> CurrentHealth = new NetworkVariable<int>();

        private bool isDead;


        public override void OnNetworkSpawn()
        {
            if(!IsServer) return;

            CurrentHealth.Value = MaxHealth;
        }

        public override void OnNetworkDespawn()
        {
            
        }

        public void TakeDamage(int damageTaken)
        {
            ModifyHealth(-damageTaken);
        }

        public void RestoreHealth(int healthRestored)
        {
            ModifyHealth(healthRestored);
        }

        private void ModifyHealth(int value)
        {
            if(isDead) return;

            int newHealth = CurrentHealth.Value + value;
            CurrentHealth.Value = Mathf.Clamp(newHealth, 0, MaxHealth);

            if(CurrentHealth.Value == 0)
            {
                OnDie?.Invoke(this);
                isDead = true;
            }
        }
    }
}
