using Unity.Netcode;
using UnityEngine;

namespace Tanks
{
    public class ConnectionButtons : MonoBehaviour
    {
        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }

        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }
    }
}
