using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ArenaBrawl.Services
{
    public class Queue
    {
        private readonly ConcurrentQueue<(Guid, string)> _queue = new ConcurrentQueue<(Guid, string)>();
        public Task<bool> AddToQueue(Guid id, string name)
        {
            _queue.Enqueue((id, name));
            return Task.FromResult(true);
        }

        public Task<(bool found, string matchesName)> AttemptToFindMatch(Guid playerId)
        {
            var dequeueResult = _queue.TryDequeue(out var result);
            if (result.Item1 == playerId)
            {
                _queue.Enqueue(result);
            }

            return Task.FromResult(dequeueResult ? (false, "") : (true, result.Item2));
        }
    }
}