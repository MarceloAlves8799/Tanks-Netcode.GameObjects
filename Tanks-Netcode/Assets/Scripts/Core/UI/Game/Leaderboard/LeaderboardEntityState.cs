
using System;
using Unity.Collections;
using Unity.Netcode;

namespace Tanks
{
    public struct LeaderboardEntityState : INetworkSerializable, IEquatable<LeaderboardEntityState>
    {
        public ulong ClientId;
        public FixedString32Bytes PlayerName;
        public int Coins;

       

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ClientId);
            serializer.SerializeValue(ref PlayerName);
            serializer.SerializeValue(ref Coins);
        }

        public bool Equals(LeaderboardEntityState other)
        {
            return ClientId.Equals(other.ClientId) &&
                   PlayerName.Equals(other.PlayerName) &&
                   Coins.Equals(other.Coins);
        }
    }
}
