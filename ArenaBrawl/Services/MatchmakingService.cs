using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ArenaBrawl.Services
{
    public class MatchmakingService
    {
        private readonly ConcurrentQueue<string> _queue = new ConcurrentQueue<string>();

        public Task<bool> AddToQueue(string id)
        {
            _queue.Enqueue(id);
            return Task.FromResult(true);
        }

        public Task<(bool found, string matchesName)> AttemptToFindMatch(string playerId)
        {
            var dequeueResult = _queue.TryDequeue(out var result);
            if (result == playerId) _queue.Enqueue(result);

            return Task.FromResult(dequeueResult ? (false, "") : (true, result));
        }
    }
}