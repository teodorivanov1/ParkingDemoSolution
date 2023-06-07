using Demo.WebApi.Application.Abstractions.Pagination;
using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Core.Entities;
using Demo.WebApi.Core.Services.RateCalculators;
using MediatR;

namespace Demo.WebApi.Application.Features.Parking.Queries
{
    public class GetAllHandler : BaseInitialization, IRequestHandler<GetAll, PagedResult<ParkingSpot>>
    {
        private readonly IParkingRateCalculator parkingRateCalculator;

        public GetAllHandler(IParkingSpotsRepository parkingSpotsRepository, IParkingRateCalculator parkingRateCalculator)
            : base(parkingSpotsRepository)
        {
            this.parkingRateCalculator = parkingRateCalculator;
        }
        public async Task<PagedResult<ParkingSpot>> Handle(GetAll request, CancellationToken cancellationToken)
        {
            // 10 must be taken from some setting.
            const int itemsPerPage = 10;
            var page = await parkingSpotsRepository.GetPagedReponseAsync(request.Page, itemsPerPage);

            page.Results.ToList()
                .ForEach(x => x.TotalMoney = parkingRateCalculator.CalculateParkingRate(x));

            return page;
        }
    }
}
