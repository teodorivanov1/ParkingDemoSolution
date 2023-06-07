using Demo.WebApi.Common.Enums;
using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Entities;
using Demo.WebApi.Core.Exceptions;

namespace Demo.Tests.Unit.Tests.Core
{
    public class ParkingSpotShould
    {
        private readonly ICoreValidator<ParkingSpot> validator = new ParkingSpotValidator();

        [Fact]
        public void Construct_When_Discount_Rule_Satisfied()
        {
            Assert.NotNull(new ParkingSpot(
                plate: "XX3333XXX",
                vehicleType: VehicleType.A,
                discountType: DiscountType.Employee,
                validator));
        }

        [Fact]
        public void Throws_When_Break_Discount_Rule()
        {
            Assert.Throws<CoreValidationException>(() => new ParkingSpot(
                plate: "XX3333XXX",
                vehicleType: VehicleType.B,
                discountType: DiscountType.Employee,
                validator));
        }
    }
}