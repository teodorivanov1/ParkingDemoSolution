using Demo.WebApi.Application.Abstractions.Repositories;

namespace Demo.WebApi.Application.Features.Clients
{
    public abstract class BaseInitialization
    {
        protected readonly IClientsRepository _clientsRepository;
        protected BaseInitialization(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository ?? throw new ArgumentNullException(nameof(clientsRepository));
        }
    }
}
