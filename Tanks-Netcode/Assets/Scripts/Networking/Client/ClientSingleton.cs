using System.Threading.Tasks;
using UnityEngine;

namespace Tanks
{
    public class ClientSingleton : MonoBehaviour
    {
        private static ClientSingleton instance;

        public static ClientSingleton Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<ClientSingleton>();

                if(instance == null)
                {
                    Debug.LogError("No ClientSingleton in the scene!");
                    return null;
                }

                return instance;
            }
        }

        public ClientGameManager GameManager { get; private set; }


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public async Task<bool> CreateClient()
        {
            GameManager = new ClientGameManager();

            return await GameManager.InitAsync();
        }

        private void OnDestroy()
        {
            GameManager?.Dispose();
        }
    }
}
