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
using Unity.Services.Authentication;

namespace Tanks
{
    public class ClientGameManager: IDisposable
    {

        private JoinAllocation allocation;

        private NetworkClient networkClient;


        public async Task<bool> InitAsync()
        {
            await UnityServices.InitializeAsync();

            networkClient = new NetworkClient(NetworkManager.Singleton);

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
                userName = PlayerPrefs.GetString(NameSelector.PlayerNameKey, "Missing Name"),
                userAuthId = AuthenticationService.Instance.PlayerId
            };

            string payload = JsonUtility.ToJson(userData);
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);

            NetworkManager.Singleton.NetworkConfig.ConnectionData = payloadBytes;

            NetworkManager.Singleton.StartClient();

        }

        public void Disconnect()
        {
            networkClient.Disconnect();
        }

        public void Dispose()
        {
            networkClient?.Dispose();
        }

    }
}
