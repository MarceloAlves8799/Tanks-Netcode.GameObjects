using System.Threading.Tasks;
using UnityEngine;

namespace Tanks
{
    public class ApplicationController : MonoBehaviour
    {

        [SerializeField] private ClientSingleton clientPrefab;
        [SerializeField] private HostSingleton hostPrefab;

        private async void Start()
        {
            DontDestroyOnLoad(gameObject);
            
            await LaunchInMode(SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Null);
        }

        private async Task LaunchInMode(bool isDedicatedServer)
        {
            if (isDedicatedServer)
            {

            }

            else
            {
                ClientSingleton clientSingleton = Instantiate(clientPrefab);
                await clientSingleton.CreateClient();

                HostSingleton hostSingleton = Instantiate(hostPrefab);
                clientSingleton.CreateClient();

                // Go to main menu
            }
        }
    }
}
