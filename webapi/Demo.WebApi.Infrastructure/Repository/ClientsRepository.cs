using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Core.Entities.Clients;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebApi.Infrastructure.Repository
{
    public class ClientsRepository : GenericRepository<Client>, IClientsRepository
    {
        private readonly DbSet<Client> clients;

        public ClientsRepository(AppDbContext dbContext) : base(dbContext)
        {
            clients = dbContext.Set<Client>() ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Client?> GetByNameAsync(string name)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));

            return await clients
                .Where(x => EF.Functions
                    .Like(x.Name!, $"{name}"))
                .FirstOrDefaultAsync();
        }
    }
}
