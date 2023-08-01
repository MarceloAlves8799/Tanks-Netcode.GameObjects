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

        public HostGameManager GameManager { get; private set; }


        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void CreateHost()
        {
            GameManager = new HostGameManager();
        }

        private void OnDestroy()
        {
            GameManager?.Dispose();
        }

    }
}
