using System.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks
{
    public class ClientGameManager
    {
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
    }
}
