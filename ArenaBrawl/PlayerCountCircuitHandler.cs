using System;
using System.Threading;
using System.Threading.Tasks;
using ArenaBrawl.InMemoryData;
using ArenaBrawl.InMemoryData.Matchmaking;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace ArenaBrawl.Services
{
    public class PlayerCountCircuitHandler : CircuitHandler
    {
        private readonly PlayerCountRepository _repository;
        private readonly PlayerInGameCountRepository _playerInGameCountRepository;
        private readonly PlayerSession _session;
        private bool _playerOnline;

        public PlayerCountCircuitHandler(PlayerCountRepository repository, PlayerInGameCountRepository playerInGameCountRepository, PlayerSession session)
        {
            _repository = repository;
            _playerInGameCountRepository = playerInGameCountRepository;
            _session = session;
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _playerInGameCountRepository.PlayerDisconnected(_session.Id);
            return base.OnCircuitClosedAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            if (!_playerOnline) return base.OnConnectionDownAsync(circuit, cancellationToken);
            _repository.PlayerDisconnected();
            _playerInGameCountRepository.PlayerDisconnected(_session.Id);
            _playerOnline = false;
            return base.OnConnectionDownAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _playerOnline = true;
            _repository.PlayerConnected();
            return base.OnConnectionUpAsync(circuit, cancellationToken);
        }

    }
}
