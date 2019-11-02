using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArenaBrawl.Services
{
    public class MatchmakingService
    {
        private readonly List<Game> _games = new List<Game>();
        private readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();
        private CancellationTokenSource _pollingCancellationToken;

        public MatchmakingService()
        {
            AttemptToMatchPlayers();
        }

        public Task<bool> AddToQueue(string id)
        {
            _queue.Enqueue(id);
            return Task.FromResult(true);
        }

        public Task<(bool found, string matchesName)> AttemptToFindMatch(string playerId)
        {
            var game = _games.Find(g => g.Players.Contains(playerId));
            return Task.FromResult(game == null ? (false, "") : (true, game.Players.Find(p => p != playerId)));
        }

        public int AmountOfPlayersInQueue()
        {
            return _queue.Count;
        }

        public int GamesInProgress()
        {
            return _games.Count;
        }

        private async void AttemptToMatchPlayers()
        {
            _pollingCancellationToken = new CancellationTokenSource();
            while (!_pollingCancellationToken.IsCancellationRequested)
            {
                if (_queue.Count > 1)
                {
                    var dequeueResultOne = _queue.TryDequeue(out var resultOne);
                    var dequeueResultTwo = _queue.TryDequeue(out var resultTwo);
                    _games.Add(new Game(new List<string> {resultOne, resultTwo}));
                }

                await Task.Delay(5000);
            }
        }
    }

    public class Game
    {
        public Game(List<string> players)
        {
            Players = players;
        }

        public List<string> Players { get; }
    }
}