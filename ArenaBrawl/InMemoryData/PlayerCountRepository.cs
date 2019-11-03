using System;

namespace ArenaBrawl.InMemoryData
{
    public class PlayerCountRepository
    {
        private int _currentOnlinePlayers;

        public event Action<int> CountUpdated;
        private void NotifyStateChanged() => CountUpdated?.Invoke(_currentOnlinePlayers);

        public void PlayerConnected()
        {
            _currentOnlinePlayers++;
            NotifyStateChanged();
        }

        public void PlayerDisconnected()
        {
            _currentOnlinePlayers--;
            NotifyStateChanged();
        }

        public int CurrentPlayers() => _currentOnlinePlayers;
    }
}
