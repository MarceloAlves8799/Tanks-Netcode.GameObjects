using System.Threading.Tasks;
using UnityEngine;

namespace Tanks
{
    public class HostSingleton : MonoBehaviour
    {
        private static HostSingleton instance;

        public static HostSingleton Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = FindObjectOfType<HostSingleton>();

                if(instance == null)
                {
                    Debug.LogError("No Host Singleton in scene!");
                    return null;
                }

                return instance;
            }
        }

        private HostGameManager gameManager;


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void CreateHost()
        {
            gameManager = new HostGameManager();
        }

    }
}
