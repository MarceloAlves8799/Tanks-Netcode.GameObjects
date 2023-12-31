using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks
{

    public class NetworkClient: IDisposable
    {
        private NetworkManager networkManager;


        public NetworkClient(NetworkManager networkManager)
        {
            this.networkManager = networkManager;

            networkManager.OnClientDisconnectCallback += OnClientDisconnect;
        }

        private void OnClientDisconnect(ulong clientId)
        {
            if (clientId != 0 && clientId != networkManager.LocalClientId) return;

            Disconnect();
        }

        public void Disconnect()
        {
            string menuSceneName = "MainMenu";
            if (SceneManager.GetActiveScene().name != menuSceneName)
            {
                SceneManager.LoadScene(menuSceneName);
            }

            if (networkManager.IsConnectedClient)
            {
                networkManager.Shutdown();
            }
        }

        public void Dispose()
        {
            if (networkManager != null)
            {
                networkManager.OnClientDisconnectCallback -= OnClientDisconnect;
            }
        }
    }
}
