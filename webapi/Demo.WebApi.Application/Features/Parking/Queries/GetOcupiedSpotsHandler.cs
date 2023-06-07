using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Abstractions.Repositories;
using MediatR;

namespace Demo.WebApi.Application.Features.Parking.Queries
{
    public class GetOcupiedSpotsHandler : BaseInitialization, IRequestHandler<GetOcupiedSpots, Response<int>>
    {
        public GetOcupiedSpotsHandler(IParkingSpotsRepository parkingSpotsRepository)
            : base(parkingSpotsRepository)
        {
        }
        public async Task<Response<int>> Handle(GetOcupiedSpots request, CancellationToken cancellationToken)
        {
            // we do not need to get all data from the dataset just to return single sum value.
            var all = await parkingSpotsRepository.GetAllAsync();
            var x = all.Sum(x => x.Places);
            return new Response<int>(all.Sum(x => x.Places));
        }
    }
}
