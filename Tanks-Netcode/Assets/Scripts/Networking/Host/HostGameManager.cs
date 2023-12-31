using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.Relay;
using System;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay.Models;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using UnityEngine.SceneManagement;
using Unity.Services.Lobbies;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Unity.Services.Authentication;

namespace Tanks
{
    public class HostGameManager: IDisposable
    {
        private Allocation allocation;
        private string joinCode;
        private string lobbyId;

        public NetworkServer NetworkServer { get; private set; }

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

            try
            {
                CreateLobbyOptions lobbyOptions = new CreateLobbyOptions();
                lobbyOptions.IsPrivate = false;
                lobbyOptions.Data = new Dictionary<string, DataObject>()
                {
                    {
                        "JoinCode",
                        new DataObject(visibility: DataObject.VisibilityOptions.Member,
                                       value: joinCode)
                    }
                };

                string playerName = PlayerPrefs.GetString(NameSelector.PlayerNameKey, "Unknown");
                Lobby lobby = await Lobbies.Instance.CreateLobbyAsync($"{playerName}'s Lobby", MAX_CONNECTIONS, lobbyOptions);
                lobbyId = lobby.Id;

                HostSingleton.Instance.StartCoroutine(HeartbeatLobby(15f));

            }

            catch(LobbyServiceException lobbyException)
            {
                Debug.LogError(lobbyException.Message);
                return;
            }

            NetworkServer = new NetworkServer(NetworkManager.Singleton);

            UserData userData = new UserData
            {
                userName = PlayerPrefs.GetString(NameSelector.PlayerNameKey, "Missing Name"),
                userAuthId = AuthenticationService.Instance.PlayerId
            };

            string payload = JsonUtility.ToJson(userData);
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload);

            NetworkManager.Singleton.NetworkConfig.ConnectionData = payloadBytes;

            NetworkManager.Singleton.StartHost();
            NetworkServer.OnClientLeft += HandleClientLeft;

            string gameSceneName = "Game";
            NetworkManager.Singleton.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }

        private IEnumerator HeartbeatLobby(float waitTimeSeconds)
        {
            WaitForSecondsRealtime delay = new WaitForSecondsRealtime(waitTimeSeconds);

            while(true)
            {
                Lobbies.Instance.SendHeartbeatPingAsync(lobbyId);
                yield return delay;
            }
        }

        public void Dispose()
        {
            Shutdown();
        }

        public async void Shutdown()
        {
            HostSingleton.Instance.StopCoroutine(nameof(HeartbeatLobby));

            if (!string.IsNullOrEmpty(lobbyId))
            {
                try
                {
                    await Lobbies.Instance.DeleteLobbyAsync(lobbyId);
                }

                catch (LobbyServiceException lobbyServiceException)
                {
                    Debug.LogError(lobbyServiceException.Message);
                }

                lobbyId = string.Empty;
            }

            NetworkServer.OnClientLeft -= HandleClientLeft;

            NetworkServer?.Dispose();
        }

        private async void HandleClientLeft(string authId)
        {
            try
            {
                await LobbyService.Instance.RemovePlayerAsync(lobbyId, authId);
            }
            catch(LobbyServiceException e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
