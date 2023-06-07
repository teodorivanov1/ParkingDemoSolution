using Demo.WebApi.Common.Enums;
using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Entities;
using Demo.WebApi.Core.Services.RateCalculators;
using Demo.WebApi.Core.Services.RateCalculators.Discount;

namespace Demo.Tests.Unit.Tests.Core
{
    public class ParkingRateCalculatorShould
    {
        private readonly ParkingSpot parkingSpot;
        private readonly IParkingRateCalculator parkingRateCalculator;
        private readonly decimal discount = EmployeeDiscountCalculator.Discount;

        public ParkingRateCalculatorShould(IParkingRateCalculator parkingRateCalculator, ICoreValidator<ParkingSpot> validator)
        {
            parkingSpot = new(
            plate: "XX3333XXX",
            vehicleType: VehicleType.B,
            discountType: DiscountType.None,
            validator);

            this.parkingRateCalculator = parkingRateCalculator;
        }

        [Fact]
        public void Calculate_Rate_For_7h()
        {
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(6);

            decimal expectedFromHourlyRates = parkingSpot.Rate.Hourly;

            Assert.Equal(expectedFromHourlyRates * 7, parkingRateCalculator.CalculateParkingRate(parkingSpot));
        }

        [Fact]
        public void Calculate_Rate_For_9h()
        {
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(10);

            decimal expectedFromDailyRates = parkingSpot.Rate.Daily;

            Assert.Equal(expectedFromDailyRates, parkingRateCalculator.CalculateParkingRate(parkingSpot));

        }

        [Fact]
        public void Calculate_Rate_For_25h()
        {
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(24);

            decimal expectedFromHourlyRates = parkingSpot.Rate.Hourly;
            decimal expectedFromDailyRates = parkingSpot.Rate.Daily;

            Assert.Equal(expectedFromHourlyRates + expectedFromDailyRates, parkingRateCalculator.CalculateParkingRate(parkingSpot));
        }

        [Fact]
        public void Calculate_Rate_For_72h()
        {
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(24 * 3 - 1);

            decimal expectedFromDailyRates = parkingSpot.Rate.Daily;

            Assert.Equal(expectedFromDailyRates * 3, parkingRateCalculator.CalculateParkingRate(parkingSpot));
        }

        [Fact]
        public void Calculate_Rate_For_74h()
        {
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(24 * 3 + 2 - 1);

            decimal expectedFromDailyRates = parkingSpot.Rate.Daily;
            decimal expectedFromHourlyRates = parkingSpot.Rate.Hourly;

            Assert.Equal(expectedFromDailyRates * 3 + expectedFromHourlyRates * 2, parkingRateCalculator.CalculateParkingRate(parkingSpot));
        }

        [Fact]
        public void Calculate_Employee_Discount_Rate_For_2h()
        {
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(1);
            parkingSpot.VehicleType = VehicleType.A;
            parkingSpot.DiscountType = DiscountType.Employee;

            decimal expectedFromHourlyRates = parkingSpot.Rate.Hourly;

            decimal actual = parkingRateCalculator.CalculateParkingRate(parkingSpot);

            // there is no discount because it is given per day
            Assert.Equal(expectedFromHourlyRates * 2, actual);
        }

        [Fact]
        public void Calculate_Employee_Discount_Rate_For_9h()
        {
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(8);
            parkingSpot.VehicleType = VehicleType.A;
            parkingSpot.DiscountType = DiscountType.Employee;

            decimal expectedFromDailyRates = parkingSpot.Rate.Daily;

            decimal actual = parkingRateCalculator.CalculateParkingRate(parkingSpot);

            // 9h (apply rule for if greeter than 8h charge as 1d) - discount
            Assert.Equal(expectedFromDailyRates - discount, actual);
        }

        [Fact]
        public void Calculate_Employee_Discount_Rate_For_25h()
        {
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(24);
            parkingSpot.VehicleType = VehicleType.A;
            parkingSpot.DiscountType = DiscountType.Employee;

            decimal expectedFromDailyRates = parkingSpot.Rate.Daily;
            decimal expectedHourlyRates = parkingSpot.Rate.Hourly;

            var actual = parkingRateCalculator.CalculateParkingRate(parkingSpot);

            // 1d, 1h with discount
            Assert.Equal(expectedFromDailyRates + expectedHourlyRates - discount, actual);
        }

        [Fact]
        public void Calculate_Employee_Discount_Rate_For_33h()
        {
            parkingSpot.EntryAt = DateTime.Now - TimeSpan.FromHours(32);
            parkingSpot.VehicleType = VehicleType.A;
            parkingSpot.DiscountType = DiscountType.Employee;

            decimal expectedFromDailyRates = parkingSpot.Rate.Daily;

            var actual = parkingRateCalculator.CalculateParkingRate(parkingSpot);

            // 1d, 9h == 2d with discount for 2 days
            Assert.Equal(expectedFromDailyRates * 2 - discount * 2, actual);
        }
    }
}