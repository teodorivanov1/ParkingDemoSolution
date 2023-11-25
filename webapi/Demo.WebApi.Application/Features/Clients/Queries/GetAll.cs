using Demo.WebApi.Application.Abstractions.Pagination;
using Demo.WebApi.Core.Entities.Clients;
using MediatR;

namespace Demo.WebApi.Application.Features.Clients.Queries
{
    public class GetAll : IRequest<PagedResult<Client>>
    {
        public GetAll(int page)
        {
            Page = page;
        }
        public int Page { get; }
    }
}
