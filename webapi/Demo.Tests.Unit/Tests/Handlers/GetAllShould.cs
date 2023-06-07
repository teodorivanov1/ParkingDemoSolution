using Demo.WebApi.Application.Abstractions.Pagination;
using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Application.Features.Parking.Queries;
using Demo.WebApi.Common.Enums;
using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Entities;
using Demo.WebApi.Core.Services.RateCalculators;
using Demo.WebApi.Core.Settings.Parking;
using Moq;

namespace Demo.Tests.Unit.Tests.Handlers
{
    public class GetAllShould
    {
        private readonly GetAllHandler handler;
        private readonly ParkingSpot parkingSpot;
        private readonly Mock<IParkingSpotsRepository> parkingSpotsRepository;

        public GetAllShould(IParkingRateCalculator parkingRateCalculator, ICoreValidator<ParkingSpot> validator)
        {
            parkingSpot = new(
                plate: "XX3333XXX",
                vehicleType: VehicleType.B,
                discountType: DiscountType.None,
                validator);

            var repo = new MockRepository(MockBehavior.Strict);

            parkingSpotsRepository = repo.Create<IParkingSpotsRepository>();

            handler = new GetAllHandler(parkingSpotsRepository.Object, parkingRateCalculator);
        }

        [Fact]
        public async Task Succeed()
        {
            var expectedHoursDeviation = 11;
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(expectedHoursDeviation - 1);
            var mockResults = new PagedResult<ParkingSpot>
            {
                Results = new List<ParkingSpot>() { parkingSpot }
            };

            parkingSpotsRepository.Setup(r => r
            .GetPagedReponseAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(Task.FromResult(mockResults));

            var actual = await handler.Handle(new GetAll(1), default);
            var expectedRate = ParkingSpotRateSettings.Rate[actual.Results.Single().VehicleType].Daily;

            Assert.True(actual.Results.Any());
            Assert.Equal(expectedHoursDeviation, actual.Results.Single().TotalHours);
            Assert.Equal(expectedRate, actual.Results.Single().TotalMoney);
        }
    }
}
