using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Tanks
{
    public enum AuthenticationState
    {
        NotAuthenticated,
        Authenticating,
        Authenticated,
        Error,
        TimeOut
    }

    public static class AuthenticationWrapper
    {
        public static AuthenticationState AuthenticationState { get; private set; } = AuthenticationState.NotAuthenticated;


        public static async Task<AuthenticationState> DoAuthentication(int maxRetries = 5)
        {
            if (AuthenticationState.Equals(AuthenticationState.Authenticated))
            {
                return AuthenticationState;
            }

            if (AuthenticationState.Equals(AuthenticationState.Authenticating))
            {
                Debug.LogWarning("Already authenticating");
                await Authenticating();
                return AuthenticationState;
            }

            await SignInAnonymouslyAsync(maxRetries);

            return AuthenticationState;
        }

        private static async Task SignInAnonymouslyAsync(int maxRetries)
        {
            AuthenticationState = AuthenticationState.Authenticating;

            int retries = 0;

            while (retries < maxRetries && AuthenticationState.Equals(AuthenticationState.Authenticating))
            {
                try
                {
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();

                    if (AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized)
                    {
                        AuthenticationState = AuthenticationState.Authenticated;
                        break;
                    }
                }

                catch(AuthenticationException exception)
                {
                    Debug.LogError(exception);
                    AuthenticationState = AuthenticationState.Error;
                }

                catch(RequestFailedException exception)
                {
                    Debug.LogError(exception);
                    AuthenticationState = AuthenticationState.Error;
                }


                retries++;

                int secondsToNextTry = 5 * 1000;
                await Task.Delay(secondsToNextTry);
            }

            if (!AuthenticationState.Equals(AuthenticationState.Authenticated))
            {
                Debug.LogWarning($"Player was not signed in successfully after {retries} retries");
                AuthenticationState = AuthenticationState.TimeOut;
            }
        }

        private static async Task<AuthenticationState> Authenticating()
        {
            while(AuthenticationState.Equals(AuthenticationState.Authenticating) ||
                  AuthenticationState.Equals(AuthenticationState.NotAuthenticated))
            {
                await Task.Delay(200);
            }

            return AuthenticationState;
        }
    }
}
