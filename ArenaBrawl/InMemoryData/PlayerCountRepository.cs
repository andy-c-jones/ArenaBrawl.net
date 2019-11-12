using System;
using Microsoft.ApplicationInsights;

namespace ArenaBrawl.InMemoryData
{
    public class PlayerCountRepository
    {
        private readonly TelemetryClient _telemetry;
        private int _currentOnlinePlayers;

        public event Action<int> CountUpdated;
        private void NotifyStateChanged() => CountUpdated?.Invoke(_currentOnlinePlayers);

        public PlayerCountRepository(TelemetryClient telemetry)
        {
            _telemetry = telemetry;
        }

        public void PlayerConnected()
        {
            _currentOnlinePlayers++;
            UpdateMetrics();
        }

        public void PlayerDisconnected()
        {
            _currentOnlinePlayers--;
            UpdateMetrics();
        }

        public void UpdateMetrics()
        {
            _telemetry.TrackMetric("OnlinePlayerCount",_currentOnlinePlayers);
            NotifyStateChanged();
        }

        public int CurrentPlayers() => _currentOnlinePlayers;
    }
}
