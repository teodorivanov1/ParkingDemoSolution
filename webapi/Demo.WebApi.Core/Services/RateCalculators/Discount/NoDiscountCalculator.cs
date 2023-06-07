using Demo.WebApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApi.Core.Services.RateCalculators.Discount
{
    public class NoDiscountCalculator : DiscountCalculator
    {
        public override decimal CalculateDiscount(ParkingSpot obtainedSpot)
        {
            if (NextCalculator != null)
            {
                return NextCalculator.CalculateDiscount(obtainedSpot);
            }

            return 0;
        }
    }
}
