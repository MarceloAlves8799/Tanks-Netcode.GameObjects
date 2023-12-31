//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using UnityEngine;
using System.Threading.Tasks;

using Unity.Services.Matchmaker.Apis.Backfill;

using Unity.Services.Matchmaker.Apis.Tickets;

using Unity.Services.Matchmaker.Http;
using Unity.Services.Core.Internal;
using Unity.Services.Authentication.Internal;

namespace Unity.Services.Matchmaker
{
    internal class MatchmakerServiceProvider : IInitializablePackage
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        {
            // Pass an instance of this class to Core
            var generatedPackageRegistry =
            CoreRegistry.Instance.RegisterPackage(new MatchmakerServiceProvider());
                // And specify what components it requires, or provides.
            generatedPackageRegistry.DependsOn<IAccessToken>();
;
        }

        public Task Initialize(CoreRegistry registry)
        {
            var httpClient = new HttpClient();

            var accessTokenMatchmaker = registry.GetServiceComponent<IAccessToken>();

            if (accessTokenMatchmaker != null)
            {
                MatchmakerServiceSdk.Instance =
                    new InternalMatchmakerServiceSdk(httpClient, registry.GetServiceComponent<IAccessToken>());
            }

            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// InternalMatchmakerService
    /// </summary>
    internal class InternalMatchmakerServiceSdk : IMatchmakerServiceSdk
    {
        /// <summary>
        /// Constructor for InternalMatchmakerService
        /// </summary>
        /// <param name="httpClient">The HttpClient for InternalMatchmakerService.</param>
        /// <param name="accessToken">The Authentication token for the service.</param>
        public InternalMatchmakerServiceSdk(HttpClient httpClient, IAccessToken accessToken = null)
        {
            
            BackfillApi = new BackfillApiClient(httpClient, accessToken);
            
            TicketsApi = new TicketsApiClient(httpClient, accessToken);

            AccessToken = accessToken;
            
            Configuration = new Configuration("https://matchmaker.services.api.unity.com", 10, 4, null);
        }
        
        /// <summary> Instance of IBackfillApiClient interface</summary>
        public IBackfillApiClient BackfillApi { get; set; }
        
        /// <summary> Instance of ITicketsApiClient interface</summary>
        public ITicketsApiClient TicketsApi { get; set; }

        /// <summary> Instance of AccessToken interface</summary>
        public IAccessToken AccessToken { get; set; }
        
        /// <summary> Configuration properties for the service.</summary>
        public Configuration Configuration { get; set; }
    }
}
