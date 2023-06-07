using Demo.WebApi.Common.Enums;
using Demo.WebApi.Core.Entities;

namespace Demo.WebApi.Core.Services.RateCalculators.Discount
{
    public class EmployeeDiscountCalculator : DiscountCalculator
    {
        public const decimal Discount = 6;

        public override decimal CalculateDiscount(ParkingSpot obtainedSpot)
        {
            decimal discount = 0;
            int totalDays = obtainedSpot.TotalHours / 24;
            int remainingHours = obtainedSpot.TotalHours % 24;

            if (obtainedSpot.DiscountType == DiscountType.Employee && (totalDays >= 1 || remainingHours > 8))
            {
                discount = (totalDays + 1) * Discount;
                if (remainingHours < 8)
                {
                    discount -= Discount;
                }
            }

            if (NextCalculator != null)
            {
                discount += NextCalculator.CalculateDiscount(obtainedSpot);
            }

            return discount < 1 ? 0 : discount;
        }
    }
}
