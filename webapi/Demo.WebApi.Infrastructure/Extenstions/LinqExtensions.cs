using Demo.WebApi.Application.Abstractions.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebApi.Infrastructure.Extenstions
{
    public static class LinqExtensions
    {
        public static async Task<PagedResult<T>> ТоPagedResultAsync<T>(
            this IQueryable<T> query,
            int page,
            int pageSize) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
