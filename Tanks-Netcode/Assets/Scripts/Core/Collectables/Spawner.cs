using System.Collections;
using System.Collections.Generic;
using Tanks;
using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    [SerializeField] private BulletCollectable bulletCollectable;


    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        
        BulletCollectable bulletInstance = Instantiate(bulletCollectable, new Vector3(-5f, 1f, 0f), Quaternion.identity);

        bulletInstance.GetComponent<NetworkObject>().Spawn();
    }
}
