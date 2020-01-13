using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;

namespace ArenaBrawl.InMemoryData.Matchmaking
{
    public class PlayerInGameCountRepository
    {
        private readonly TelemetryClient _telemetry;
        private readonly ConcurrentDictionary<BrawlFormat, List<Guid>> _inGameCount = new ConcurrentDictionary<BrawlFormat, List<Guid>>();
        private CancellationTokenSource _pollingCancellationToken;

        public PlayerInGameCountRepository(TelemetryClient telemetry)
        {
            _telemetry = telemetry;
            UpdateStats();
        }

        public event Action<Dictionary<BrawlFormat, int>> UpdatedCounts;

        private async void UpdateStats()
        {
            _pollingCancellationToken = new CancellationTokenSource();
            while (!_pollingCancellationToken.IsCancellationRequested)
            {
                var update = new Dictionary<BrawlFormat, int>();
                foreach (var (key, value) in _inGameCount)
                {
                    update.Add(key, value.Count);
                    _telemetry.TrackMetric($"PlayersInGame.{key}", value.Count);
                }

                UpdatedCounts?.Invoke(update);
                await Task.Delay(10000);
            }
        }

        public void PlayerJoinedGame(Guid sessionId, BrawlFormat format)
        {
            _inGameCount.AddOrUpdate(format, f => new List<Guid>{sessionId}, (f, i) =>
            {
                i.Add(sessionId);
                return i;
            });
            _telemetry.TrackMetric($"PlayersInGame.{format}", _inGameCount.GetValueOrDefault(format,new List<Guid>()).Count);

        }

        public async void PlayerLeftGame(Guid sessionId, BrawlFormat format)
        {
            await Task.Run(() =>
            {
                _inGameCount.AddOrUpdate(format, f => new List<Guid>(), (f, i) =>
                {
                    i.Remove(sessionId);
                    return i;
                });
                var playersInGameInFormat = _inGameCount.GetValueOrDefault(format, new List<Guid>()).Count;
                _telemetry.TrackMetric($"PlayersInGame.{format}", playersInGameInFormat);
            });
        }

        public async void PlayerDisconnected(Guid sessionId)
        {
            await Task.Run(() =>
            {
                foreach (var format in _inGameCount)
                {
                    format.Value.Remove(sessionId);
                    _telemetry.TrackMetric($"PlayersInGame.{format}", format.Value.Count);
                }
            });
        }

        ~PlayerInGameCountRepository()
        {
            _pollingCancellationToken.Cancel();
        }
    }
}
