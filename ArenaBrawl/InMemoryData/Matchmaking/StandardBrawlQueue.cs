using System;
using System.Collections.Concurrent;

namespace ArenaBrawl.InMemoryData.Matchmaking
{
    public class StandardBrawlQueue : MatchmakingQueueBase
    {
        public StandardBrawlQueue() 
            : base(new ConcurrentDictionary<Guid, PlayerWaitingForGame>(), new ConcurrentDictionary<Guid, PotentialMatch>())
        {
        }
    }
}