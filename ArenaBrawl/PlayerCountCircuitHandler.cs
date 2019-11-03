using System.Threading;
using System.Threading.Tasks;
using ArenaBrawl.Data;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace ArenaBrawl.Services
{
    public class PlayerCountCircuitHandler : CircuitHandler
    {
        private readonly PlayerCountRepository _repository;
        private bool playerOnline;

        public PlayerCountCircuitHandler(PlayerCountRepository repository)
        {
            _repository = repository;
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            if (!playerOnline) return base.OnConnectionDownAsync(circuit, cancellationToken);
            
            _repository.PlayerDisconnected();
            playerOnline = false;
            return base.OnConnectionDownAsync(circuit, cancellationToken);
        }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            playerOnline = true;
            _repository.PlayerConnected();
            return base.OnConnectionUpAsync(circuit, cancellationToken);
        }

    }
}
