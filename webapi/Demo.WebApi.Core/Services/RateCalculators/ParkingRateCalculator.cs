using Demo.WebApi.Core.Entities;
using Demo.WebApi.Core.Services.RateCalculators.Discount;

namespace Demo.WebApi.Core.Services.RateCalculators
{
    public class ParkingRateCalculator : IParkingRateCalculator
    {
        private DiscountCalculator discountCalculator;

        public ParkingRateCalculator(EmployeeDiscountCalculator epmloyeeDiscountCalculator, NoDiscountCalculator noDiscountCalculator)
        {
            this.discountCalculator = epmloyeeDiscountCalculator;
            InitializeChain(epmloyeeDiscountCalculator, noDiscountCalculator);
        }

        private void InitializeChain(DiscountCalculator epmloyeeDiscountCalculator, DiscountCalculator noDiscountCalculator)
        {
            discountCalculator = epmloyeeDiscountCalculator;
            discountCalculator.SetNextCalculator(noDiscountCalculator);

        }

        public decimal CalculateParkingRate(ParkingSpot obtainedSpot)
        {
            decimal discount = discountCalculator.CalculateDiscount(obtainedSpot);

            int totalDays = obtainedSpot.TotalHours / 24;
            int remainingHours = obtainedSpot.TotalHours % 24;

            decimal totalFee = obtainedSpot.Rate.Daily * totalDays;

            if (remainingHours <= 8)
            {
                totalFee += remainingHours * obtainedSpot.Rate.Hourly;
            }
            else
            {
                totalFee += obtainedSpot.Rate.Daily;
            }

            totalFee -= discount;

            return totalFee <= 0 ? 0 : totalFee;
        }
    }
}
