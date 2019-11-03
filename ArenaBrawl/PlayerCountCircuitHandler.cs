using System.Threading;
using System.Threading.Tasks;
using ArenaBrawl.Data;
using ArenaBrawl.InMemoryData;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace ArenaBrawl.Services
{
    public class PlayerCountCircuitHandler : CircuitHandler
    {
        private readonly PlayerCountRepository _repository;
        private bool _playerOnline;

        public PlayerCountCircuitHandler(PlayerCountRepository repository)
        {
            _repository = repository;
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            if (!_playerOnline) return base.OnConnectionDownAsync(circuit, cancellationToken);
            
            _repository.PlayerDisconnected();
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
