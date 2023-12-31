using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tanks
{
    public class SpawnPoint : MonoBehaviour
    {
        private static List<SpawnPoint> spawnPoints = new List<SpawnPoint>();


        private void OnEnable()
        {
            spawnPoints.Add(this);
        }

        public static Vector3 GetRandomSpawnPos()
        {
            if(spawnPoints.Count == 0)
            {
                return Vector3.zero;
            }

            return spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
        }

        private void OnDisable()
        {
            spawnPoints.Remove(this);
        }
    }
}
