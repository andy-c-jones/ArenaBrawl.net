using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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