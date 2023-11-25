using Demo.WebApi.Core.Entities.Clients;

namespace Demo.WebApi.Application.Abstractions.Repositories
{
    public interface IClientsRepository : IGenericRepository<Client>
    {
        Task<Client?> GetByNameAsync(string name);
    }
}
