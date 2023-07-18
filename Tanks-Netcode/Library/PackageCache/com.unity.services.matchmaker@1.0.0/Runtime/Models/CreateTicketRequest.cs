//-----------------------------------------------------------------------------
// <auto-generated>
//     This file was generated by the C# SDK Code Generator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Scripting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Unity.Services.Matchmaker.Http;



namespace Unity.Services.Matchmaker.Models
{
    /// <summary>
    /// CreateTicketRequest model
    /// </summary>
    [Preserve]
    [DataContract(Name = "CreateTicketRequest")]
    internal class CreateTicketRequest
    {
        /// <summary>
        /// Creates an instance of CreateTicketRequest.
        /// </summary>
        /// <param name="players">A list of players.</param>
        /// <param name="queueName">A logical grouping of tickets, where tickets get considered for matchmaking together. Has to match one of the queue names in the matchmaking config. If not provided, the default queue will be used.</param>
        /// <param name="attributes">An object that holds a dictionary of attributes (number or string), indexed by the attribute name. The attributes are compared against the corresponding filters defined in the matchmaking config and used to segment the ticket population into pools. The default pool is used if a pool isn&#39;t provided.</param>
        [Preserve]
        public CreateTicketRequest(List<Player> players, string queueName = default, Dictionary<string, object> attributes = default)
        {
            QueueName = queueName;
            Attributes = (Dictionary<string, IDeserializable>) JsonObject.GetNewJsonObjectResponse(attributes);
            Players = players;
        }

        /// <summary>
        /// A logical grouping of tickets, where tickets get considered for matchmaking together. Has to match one of the queue names in the matchmaking config. If not provided, the default queue will be used.
        /// </summary>
        [Preserve]
        [DataMember(Name = "queueName", EmitDefaultValue = false)]
        public string QueueName{ get; }
        
        /// <summary>
        /// An object that holds a dictionary of attributes (number or string), indexed by the attribute name. The attributes are compared against the corresponding filters defined in the matchmaking config and used to segment the ticket population into pools. The default pool is used if a pool isn&#39;t provided.
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectCollectionConverter))]
        [DataMember(Name = "attributes", EmitDefaultValue = false)]
        public Dictionary<string, IDeserializable> Attributes{ get; }
        
        /// <summary>
        /// A list of players.
        /// </summary>
        [Preserve]
        [DataMember(Name = "players", IsRequired = true, EmitDefaultValue = true)]
        public List<Player> Players{ get; }
    
        /// <summary>
        /// Formats a CreateTicketRequest into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (QueueName != null)
            {
                serializedModel += "queueName," + QueueName + ",";
            }
            if (Attributes != null)
            {
                serializedModel += "attributes," + Attributes.ToString() + ",";
            }
            if (Players != null)
            {
                serializedModel += "players," + Players.ToString();
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a CreateTicketRequest as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            if (QueueName != null)
            {
                var queueNameStringValue = QueueName.ToString();
                dictionary.Add("queueName", queueNameStringValue);
            }
            
            if (Attributes != null)
            {
                var attributesStringValue = Attributes.ToString();
                dictionary.Add("attributes", attributesStringValue);
            }
            
            return dictionary;
        }
    }
}