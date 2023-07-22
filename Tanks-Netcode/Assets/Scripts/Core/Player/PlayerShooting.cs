using Unity.Netcode;
using UnityEngine;

namespace Tanks
{
    public class PlayerShooting : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private InputReader inputReader;

        [SerializeField] private Transform projectileSpawnPoint;

        [SerializeField] private GameObject serverProjectilePrefab;
        [SerializeField] private GameObject clientProjectilePrefab;

        [Header("Projectiles Setting")]
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float fireRate;
        private float previousFireTime;

        private bool hasShot;



        public override void OnNetworkSpawn()
        {
            if (!IsOwner) return;

            inputReader.PrimaryFireEvent += HandlePrimaryFire;
        }

        public override void OnNetworkDespawn()
        {
            if (!IsOwner) return;

            inputReader.PrimaryFireEvent -= HandlePrimaryFire;
        }

        private void Update()
        {
            if(!IsOwner) return;

            if (!hasShot) return;

            if (Time.time < (1 / fireRate) + previousFireTime) return;

            PrimaryFireServerRpc(projectileSpawnPoint.position, projectileSpawnPoint.forward);

            SpawnDummyProjectile(projectileSpawnPoint.position, projectileSpawnPoint.forward);

            previousFireTime = Time.time;
        }

        private void HandlePrimaryFire(bool shouldFire)
        {
            hasShot = shouldFire;
        }

        [ServerRpc]
        private void PrimaryFireServerRpc(Vector3 spawnPos, Vector3 direction)
        {
            GameObject projectileInstance = Instantiate(serverProjectilePrefab, spawnPos, Quaternion.identity);

            projectileInstance.transform.forward = direction;

            if (projectileInstance.TryGetComponent<DealDamageOnContact>(out DealDamageOnContact dealDamage))
            {
                dealDamage.SetOwner(OwnerClientId);
            }

            if (projectileInstance.TryGetComponent<Rigidbody>(out Rigidbody projectileRb))
            {
                projectileRb.velocity = projectileRb.transform.forward * projectileSpeed;
            }

            SpawnDummyProjectileClientRpc(spawnPos, direction);
        }

        [ClientRpc]
        private void SpawnDummyProjectileClientRpc(Vector3 spawnPos, Vector3 direction)
        {
            if (IsOwner) return;

            SpawnDummyProjectile(spawnPos, direction);
        }

        private void SpawnDummyProjectile(Vector3 spawnPos, Vector3 direction)
        {
            GameObject projectileInstance = Instantiate(clientProjectilePrefab, spawnPos, Quaternion.identity);

            projectileInstance.transform.forward = direction;

            if(projectileInstance.TryGetComponent<Rigidbody>(out Rigidbody projectileRb))
            {
                projectileRb.velocity = projectileRb.transform.forward * projectileSpeed;
            }
        }
    }
}
