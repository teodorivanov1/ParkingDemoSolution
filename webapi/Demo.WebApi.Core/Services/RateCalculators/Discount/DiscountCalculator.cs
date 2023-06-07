using Demo.WebApi.Core.Entities;

namespace Demo.WebApi.Core.Services.RateCalculators.Discount
{
    public abstract class DiscountCalculator
    {
#nullable disable
        protected DiscountCalculator NextCalculator;
#nullable enable
        public void SetNextCalculator(DiscountCalculator nextCalculator)
        {
            NextCalculator = nextCalculator ?? throw new ArgumentNullException(nameof(nextCalculator));
        }

        public abstract decimal CalculateDiscount(ParkingSpot obtainedSpot);
    }
}
