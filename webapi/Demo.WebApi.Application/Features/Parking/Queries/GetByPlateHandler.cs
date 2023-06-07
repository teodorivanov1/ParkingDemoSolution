using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Application.Exceptions;
using Demo.WebApi.Core.Entities;
using Demo.WebApi.Core.Services.RateCalculators;
using MediatR;

namespace Demo.WebApi.Application.Features.Parking.Queries
{
    public class GetByPlateHandler : BaseInitialization, IRequestHandler<GetByPlate, Response<ParkingSpot>>
    {
        private const string NotFoundMessage = "Тhere is no vehicle with such a license plate.";
        private readonly IParkingRateCalculator parkingRateCalculator;

        public GetByPlateHandler(IParkingSpotsRepository parkingSpotsRepository, IParkingRateCalculator parkingRateCalculator)
            : base(parkingSpotsRepository)
        {
            this.parkingRateCalculator = parkingRateCalculator;
        }
        public async Task<Response<ParkingSpot>> Handle(GetByPlate request, CancellationToken cancellationToken)
        {
            // this must be part of separate data validation (integrity and etc..) and must be part from a mediatr pipeline.
            var spot = await parkingSpotsRepository.GetByPlateAsync(request.PlateNumber) ?? throw new ApiException(NotFoundMessage);

            spot.TotalMoney = parkingRateCalculator.CalculateParkingRate(spot);
            return new Response<ParkingSpot>(spot);
        }
    }
}
