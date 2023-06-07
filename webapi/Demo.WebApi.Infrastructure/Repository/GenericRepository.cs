using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Abstractions.Pagination;
using Demo.WebApi.Core.Entities;
using Demo.WebApi.Infrastructure.Extenstions;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebApi.Infrastructure.Repository
{
    // It might be a good idea in the future to add CancellationToken as a parameter to every async method.
    // Not all methods are needed.
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly AppDbContext dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
#nullable disable
            return await dbContext.Set<T>().FindAsync(id);
#nullable enable
        }

        public async Task<PagedResult<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await dbContext
                .Set<T>()
                .OrderByDescending(x => x.Id)
                .ТоPagedResultAsync(pageNumber, pageSize);
        }

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            await SaveAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entity)
        {
            await dbContext.Set<T>().AddRangeAsync(entity);
            await SaveAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbContext
                 .Set<T>()
                 .ToListAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var e = await dbContext.Set<T>().Where(x => x.Id == id).SingleAsync();

            if (e != null)
            {
                dbContext.Set<T>().Remove(e);
                await SaveAsync();
            }
        }

        private async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}