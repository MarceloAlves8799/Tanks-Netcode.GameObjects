using System.Collections.Generic;
using Unity.Services.Matchmaker.Models;

namespace Unity.Services.Matchmaker 
{
	/// <summary>
	/// Paramter class for making matchmaker backfill ticket requests.
	/// </summary>
	public class CreateBackfillTicketOptions 
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public CreateBackfillTicketOptions() 
		{
		}

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="connection"></param>
        /// <param name="attributes"></param>
        /// <param name="properties"></param>
        /// <param name="poolId"></param>
        /// <param name="matchId"></param>
        public CreateBackfillTicketOptions(
			string queueName, 
			string connection, 
			Dictionary<string, object> attributes = default, 
			BackfillTicketProperties properties = default,
			string poolId = default,
			string matchId = default
		) 
		{
			QueueName = queueName;
			Connection = connection;
			Attributes = attributes;
			Properties = properties;
			PoolId = poolId;
			MatchId = matchId;
		}

		/// <summary>
		/// Name of the queue to target the backfill request.
		/// </summary>
		public string QueueName { get; set; }

		/// <summary>
		/// The IP address and port of the server creating the backfill (using the format ip:port). This property is used to assign the server the matching tickets
		/// </summary>
		public string Connection { get; set; }

		/// <summary>
		/// A dictionary of attributes (number or string), indexed by the attribute name. 
		/// The attributes are compared against the corresponding filters defined in the matchmaking config and used to segment the ticket population into pools. 
		/// Example attributes include map, mode, platform, and build number. (Optional)
		/// </summary>
		public Dictionary<string, object> Attributes { get; set; }

		/// <summary>
		/// Properties object containing match information.
		/// </summary>
		public BackfillTicketProperties Properties { get; set; }

		/// <summary>
		/// The ID of the pool to create the backfill ticket in. Cannot be used if the &#x60;attributes&#x60; field is present. The allocation payload contains the pool ID of the match it was created in.
		/// </summary>
		public string PoolId { get; set; }

		/// <summary>
		/// The Id of the match that this backfill ticket is targeting. The match Id is contained in the allocation payload.
		/// </summary>
		public string MatchId { get; set; }
	}
}
