using System;
using System.Collections.Generic;

namespace ArenaBrawl.InMemoryData.Matchmaking
{
    public class PotentialMatch
    {
        public List<PlayerWaitingForGame> PlayersWaitingForGames { get; set; }
        public Guid Id { get; set; }

        public PotentialMatch(List<PlayerWaitingForGame> playersWaitingForGames)
        {
            Id = Guid.NewGuid();
            PlayersWaitingForGames = playersWaitingForGames;
        }
    }
}