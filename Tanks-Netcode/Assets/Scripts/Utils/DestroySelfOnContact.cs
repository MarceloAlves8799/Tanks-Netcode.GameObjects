using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public class DestroySelfOnContact : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Destroy(gameObject);
        }
    }
}
