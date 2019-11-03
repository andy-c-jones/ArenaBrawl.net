using System;
using System.Collections.Generic;

namespace ArenaBrawl.InMemoryData
{
    public class PlayerWaitingForGame
    {
        private sealed class IdInGameNameEqualityComparer : IEqualityComparer<PlayerWaitingForGame>
        {
            public bool Equals(PlayerWaitingForGame x, PlayerWaitingForGame y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Id.Equals(y.Id) && x.InGameName == y.InGameName;
            }

            public int GetHashCode(PlayerWaitingForGame obj)
            {
                unchecked
                {
                    return (obj.Id.GetHashCode() * 397) ^ (obj.InGameName != null ? obj.InGameName.GetHashCode() : 0);
                }
            }
        }

        public static IEqualityComparer<PlayerWaitingForGame> IdInGameNameComparer { get; } = new IdInGameNameEqualityComparer();

        public PlayerWaitingForGame(Guid sessionId, string name)
        {
            Id = sessionId;
            InGameName = name;
        }

        public Guid Id { get; }
        public string InGameName { get; }
    }
}
