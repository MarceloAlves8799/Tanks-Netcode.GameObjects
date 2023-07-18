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
    /// ProblemDetails model
    /// </summary>
    [Preserve]
    [DataContract(Name = "ProblemDetails")]
    internal class ProblemDetails
    {
        /// <summary>
        /// Creates an instance of ProblemDetails.
        /// </summary>
        /// <param name="type">type param</param>
        /// <param name="title">title param</param>
        /// <param name="errors">errors param</param>
        /// <param name="status">status param</param>
        /// <param name="detail">detail param</param>
        /// <param name="instance">instance param</param>
        [Preserve]
        public ProblemDetails(string type = default, string title = default, object errors = default, int? status = default, string detail = default, string instance = default)
        {
            Type = type;
            Title = title;
            Errors = (IDeserializable) JsonObject.GetNewJsonObjectResponse(errors);
            Status = status;
            Detail = detail;
            Instance = instance;
        }

        /// <summary>
        /// Parameter type of ProblemDetails
        /// </summary>
        [Preserve]
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type{ get; }
        
        /// <summary>
        /// Parameter title of ProblemDetails
        /// </summary>
        [Preserve]
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title{ get; }
        
        /// <summary>
        /// Parameter errors of ProblemDetails
        /// </summary>
        [Preserve][JsonConverter(typeof(JsonObjectConverter))]
        [DataMember(Name = "errors", EmitDefaultValue = false)]
        public IDeserializable Errors{ get; }
        
        /// <summary>
        /// Parameter status of ProblemDetails
        /// </summary>
        [Preserve]
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int? Status{ get; }
        
        /// <summary>
        /// Parameter detail of ProblemDetails
        /// </summary>
        [Preserve]
        [DataMember(Name = "detail", EmitDefaultValue = false)]
        public string Detail{ get; }
        
        /// <summary>
        /// Parameter instance of ProblemDetails
        /// </summary>
        [Preserve]
        [DataMember(Name = "instance", EmitDefaultValue = false)]
        public string Instance{ get; }
    
        /// <summary>
        /// Formats a ProblemDetails into a string of key-value pairs for use as a path parameter.
        /// </summary>
        /// <returns>Returns a string representation of the key-value pairs.</returns>
        internal string SerializeAsPathParam()
        {
            var serializedModel = "";

            if (Type != null)
            {
                serializedModel += "type," + Type + ",";
            }
            if (Title != null)
            {
                serializedModel += "title," + Title + ",";
            }
            if (Errors != null)
            {
                serializedModel += "errors," + Errors.ToString() + ",";
            }
            if (Status != null)
            {
                serializedModel += "status," + Status.ToString() + ",";
            }
            if (Detail != null)
            {
                serializedModel += "detail," + Detail + ",";
            }
            if (Instance != null)
            {
                serializedModel += "instance," + Instance;
            }
            return serializedModel;
        }

        /// <summary>
        /// Returns a ProblemDetails as a dictionary of key-value pairs for use as a query parameter.
        /// </summary>
        /// <returns>Returns a dictionary of string key-value pairs.</returns>
        internal Dictionary<string, string> GetAsQueryParam()
        {
            var dictionary = new Dictionary<string, string>();

            if (Type != null)
            {
                var typeStringValue = Type.ToString();
                dictionary.Add("type", typeStringValue);
            }
            
            if (Title != null)
            {
                var titleStringValue = Title.ToString();
                dictionary.Add("title", titleStringValue);
            }
            
            if (Status != null)
            {
                var statusStringValue = Status.ToString();
                dictionary.Add("status", statusStringValue);
            }
            
            if (Detail != null)
            {
                var detailStringValue = Detail.ToString();
                dictionary.Add("detail", detailStringValue);
            }
            
            if (Instance != null)
            {
                var instanceStringValue = Instance.ToString();
                dictionary.Add("instance", instanceStringValue);
            }
            
            return dictionary;
        }
    }
}