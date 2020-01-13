using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;

namespace ArenaBrawl.InMemoryData
{
    public class PlayerCountRepository
    {
        private readonly TelemetryClient _telemetry;
        private int _currentOnlinePlayers;
        private CancellationTokenSource _pollingCancellationToken;

        public event Action<int> CountUpdated;
        private void NotifyStateChanged() => CountUpdated?.Invoke(_currentOnlinePlayers);

        public PlayerCountRepository(TelemetryClient telemetry)
        {
            _telemetry = telemetry;
            UpdateStats();
        }
        
        private async void UpdateStats()
        {
            _pollingCancellationToken = new CancellationTokenSource();
            while (!_pollingCancellationToken.IsCancellationRequested)
            {
                _telemetry.TrackMetric("OnlinePlayerCount",_currentOnlinePlayers);
                NotifyStateChanged();
                await Task.Delay(10000);
            }
        }

        public void PlayerConnected()
        {
            _currentOnlinePlayers++;
        }

        public void PlayerDisconnected()
        {
            _currentOnlinePlayers--;
        }

        public int CurrentPlayers() => _currentOnlinePlayers;
    }
}
