using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using Unity.Networking.Transport.Relay;
using System.Text;

namespace Tanks
{
    public class ClientGameManager
    {

        private JoinAllocation allocation;


        public async Task<bool> InitAsync()
        {
            await UnityServices.InitializeAsync();

            AuthenticationState authState = await AuthenticationWrapper.DoAuthentication();

            return authState.Equals(AuthenticationState.Authenticated);
        }

        public void GoToMenu()
        {
            string menuSceneName = "MainMenu";
            SceneManager.LoadScene(menuSceneName);
        }

        public async Task StartClientAsync(string joinCode)
        {
            try
            {
                allocation = await Relay.Instance.JoinAllocationAsync(joinCode);
            }

            catch(Exception startClientException)
            {
                Debug.LogError(startClientException.Message);
                return;
            }

            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            transport.SetRelayServerData(relayServerData);

            UserData userData = new UserData
            {
                userName = PlayerPrefs.GetString(NameSelector.PlayerNameKey, "Missing Name")
            };

            string payload = JsonUtility.ToJson(userData);
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);

            NetworkManager.Singleton.NetworkConfig.ConnectionData = payloadBytes;

            NetworkManager.Singleton.StartClient();

        }
    }
}
