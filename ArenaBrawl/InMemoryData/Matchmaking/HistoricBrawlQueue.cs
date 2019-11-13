using System;
using System.Collections.Concurrent;

namespace ArenaBrawl.InMemoryData.Matchmaking
{
    public class HistoricBrawlQueue : MatchmakingQueueBase
    {
        public HistoricBrawlQueue() 
            : base(new ConcurrentDictionary<Guid, PlayerWaitingForGame>(), new ConcurrentDictionary<Guid, PotentialMatch>())
        {
        }
    }
}