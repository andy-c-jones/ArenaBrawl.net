using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ArenaBrawl.InMemoryData.Matchmaking
{
    public class MatchmatchingQueue
    {
        private readonly ConcurrentBag<PlayerWaitingForGame> _queue = new ConcurrentBag<PlayerWaitingForGame>();
        private CancellationTokenSource _pollingCancellationToken;

        public MatchmatchingQueue()
        {
            AttemptToMatchPlayers();
        }

        public async Task<bool> Add(PlayerWaitingForGame playerWaitingForGame)
        {
            _queue.Add(playerWaitingForGame);
            return true;
        }

        private async void AttemptToMatchPlayers()
        {
            _pollingCancellationToken = new CancellationTokenSource();
            while (!_pollingCancellationToken.IsCancellationRequested)
            {
                if (_queue.Count > 1)
                {
                    _queue.TryTake(out var playerOne);
                    _queue.TryTake(out var playerTwo);
                    MatchFound?.Invoke(new List<PlayerWaitingForGame>
                    {
                        playerOne,
                        playerTwo
                    });
                }

                await Task.Delay(5000);
            }
        }

        public event Action<IList<PlayerWaitingForGame>> MatchFound;

        ~MatchmatchingQueue()
        {
            _pollingCancellationToken.Cancel();
        }
    }
}