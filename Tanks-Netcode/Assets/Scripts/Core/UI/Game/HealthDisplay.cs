using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks
{
    public class HealthDisplay : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private Health health;
        [SerializeField] private Slider healthSlider;


        public override void OnNetworkSpawn()
        {
            if (!IsClient) return;

#if UNITY_EDITOR
            
            if (health == null)
                Debug.LogError("Component Health is null in " + gameObject.name);

#endif

            if (health != null)
            {
                health.CurrentHealth.OnValueChanged += HandleHealthChanged;
                HandleHealthChanged(0, health.CurrentHealth.Value);
            }
        }

        public override void OnNetworkDespawn()
        {
            if (!IsClient) return;

            if (health != null)
                health.CurrentHealth.OnValueChanged -= HandleHealthChanged;
        }

        private void HandleHealthChanged(int oldHealth, int newHealth)
        {
            if (health == null) return;

            healthSlider.value = (float) newHealth / health.MaxHealth;
        }
    }
}
