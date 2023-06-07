using Demo.WebApi.Application.Abstractions.Pagination;
using Demo.WebApi.Core.Entities;
using MediatR;

namespace Demo.WebApi.Application.Features.Parking.Queries
{
    public class GetAll : IRequest<PagedResult<ParkingSpot>>
    {
        public GetAll(int page)
        {
            Page = page;
        }
        public int Page { get; }
    }
}
