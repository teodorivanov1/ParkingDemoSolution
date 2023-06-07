using AutoMapper;
using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Application.Exceptions;
using Demo.WebApi.Application.Features.Parking.Commands;
using Demo.WebApi.Common.Enums;
using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Entities;
using Moq;

namespace Demo.Tests.Unit.Tests.Handlers
{
    public class EntryCommandShould
    {
        private readonly EntryCommandHandler handler;
        private readonly ParkingSpot parkingSpot;
        private readonly Mock<IParkingSpotsRepository> parkingSpotsRepository;

        public EntryCommandShould(ICoreValidator<ParkingSpot> validator, IMapper mapper)
        {
            parkingSpot = new(
                plate: "XX3333XXX",
                vehicleType: VehicleType.B,
                discountType: DiscountType.None,
                validator);

            var repo = new MockRepository(MockBehavior.Strict);

            parkingSpotsRepository = repo.Create<IParkingSpotsRepository>();

            handler = new EntryCommandHandler(parkingSpotsRepository.Object, mapper);
        }

        [Fact]
        public async Task Succeed()
        {
            var command = new EntryCommand("XX3333XX", VehicleType.A, DiscountType.None, 350);

            parkingSpot.EntryAt = DateTime.Now;
            parkingSpot.Id = 1;
            var mockResults = new Response<EntryCommandResult>()
            {
                Data = new EntryCommandResult() { Ticket = 1 }
            };

            parkingSpotsRepository.Setup(r => r.AddAsync(It.IsAny<ParkingSpot>())).Returns(Task.FromResult(parkingSpot));

            var actual = await handler.Handle(command, default);

            parkingSpotsRepository.Verify(x => x.AddAsync(It.IsAny<ParkingSpot>()), Times.Once);
            Assert.NotNull(actual.Data);
            Assert.True(actual.Succeeded);
            Assert.Equal(mockResults.Data.Ticket, actual.Data.Ticket);
        }

        [Fact]
        public async Task Fail_When_Not_Enough_Space()
        {
            var availability = 1;
            var command = new EntryCommand("XX3333XX", VehicleType.B, DiscountType.None, availability);

            parkingSpotsRepository.Setup(r => r.AddAsync(It.IsAny<ParkingSpot>())).Returns(Task.FromResult(parkingSpot));

            await Assert.ThrowsAnyAsync<ApiException>(async () => await handler.Handle(command, default));
        }
    }
}
