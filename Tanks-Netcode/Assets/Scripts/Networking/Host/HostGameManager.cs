using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Relay;
using System;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using UnityEngine.SceneManagement;

namespace Tanks
{
    public class HostGameManager
    {
        private Allocation allocation;
        private string joinCode;

        private const int MAX_CONNECTIONS = 10;


        public async Task StartHostAsync()
        {
            try
            {
                allocation = await Relay.Instance.CreateAllocationAsync(MAX_CONNECTIONS);
            }

            catch(Exception startHostException)
            {
                Debug.LogError(startHostException.Message);
                return;
            }

            try
            {
                joinCode = await Relay.Instance.GetJoinCodeAsync(allocation.AllocationId);
                Debug.Log(joinCode);
            }

            catch(Exception joinCodeException)
            {
                Debug.LogError(joinCodeException.Message);
                return;
            }

            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            
            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            transport.SetRelayServerData(relayServerData);

            NetworkManager.Singleton.StartHost();

            string gameSceneName = "Game";
            NetworkManager.Singleton.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }
    }
}
