using Demo.WebApi.Core.Entities;

namespace Demo.WebApi.Core.Services.RateCalculators
{
    public interface IParkingRateCalculator
    {
        decimal CalculateParkingRate(ParkingSpot obtainedSpot);
    }
}