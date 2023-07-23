using Unity.Netcode;
using UnityEngine;

namespace Tanks 
{ 
    public class BulletCollectable : NetworkBehaviour, ICollectable
    {
        [SerializeField] private GameObject visualCollectable;

        [field:SerializeField] public bool AlreadyCollect { get; set; }


        public void OnCollect()
        {
            if (!IsServer)
            {
                visualCollectable.SetActive(false);
                return;
            }

            if (AlreadyCollect)
            {
                return;
            }

            AlreadyCollect = true;
            visualCollectable.SetActive(false);
        }
    }
}
