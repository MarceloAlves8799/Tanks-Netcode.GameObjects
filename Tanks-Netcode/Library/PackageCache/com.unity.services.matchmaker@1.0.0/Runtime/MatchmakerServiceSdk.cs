//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using Unity.Services.Authentication.Internal;
using Unity.Services.Matchmaker.Apis.Backfill;
using Unity.Services.Matchmaker.Apis.Tickets;


namespace Unity.Services.Matchmaker
{
    /// <summary>
    /// MatchmakerService
    /// </summary>
    internal static class MatchmakerServiceSdk
    {
        /// <summary>
        /// The static instance of MatchmakerService.
        /// </summary>
        public static IMatchmakerServiceSdk Instance { get; internal set; }
    }

    /// <summary> Interface for MatchmakerService</summary>
    internal interface IMatchmakerServiceSdk
    {
        
        /// <summary> Accessor for BackfillApi methods.</summary>
        IBackfillApiClient BackfillApi { get; set; }
        
        /// <summary> Accessor for TicketsApi methods.</summary>
        ITicketsApiClient TicketsApi { get; set; }

        /// <summary> Accessor for AccessToken methods.</summary>
        IAccessToken AccessToken { get; set; }

        /// <summary> Configuration properties for the service.</summary>
        Configuration Configuration { get; set; }
    }
}
