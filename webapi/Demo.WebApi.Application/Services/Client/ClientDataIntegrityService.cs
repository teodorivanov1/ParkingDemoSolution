using Demo.WebApi.Application.Abstractions.DataIntegrity;
using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Application.Features.Clients.Commands;

namespace Demo.WebApi.Application.Services.Client
{
    public class ClientDataIntegrityService :
        IDataIntegrityService<AddClientCommand>
    {
        private readonly IClientsRepository _clientRespository;

        public ClientDataIntegrityService(IClientsRepository clientRespository)
        {
            _clientRespository = clientRespository ?? throw new ArgumentNullException(nameof(clientRespository));
        }

        public async Task<DataIntegrityResult> ExecuteAsync(AddClientCommand request, CancellationToken cancellationToken = default)
        {
            _ = request ?? throw new ArgumentNullException(nameof(request));

            Core.Entities.Clients.Client? client = await _clientRespository.GetByNameAsync(request.Name);

            if (client == null)
            {
                return new DataIntegrityResult();
            }
            throw new InvalidDataException($"Client with name {client.Name} already exists.");
        }
    }
}
