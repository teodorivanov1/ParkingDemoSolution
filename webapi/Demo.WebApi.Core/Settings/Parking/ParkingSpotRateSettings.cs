using Demo.WebApi.Common.Enums;
using Demo.WebApi.Core.Entities;

namespace Demo.WebApi.Core.Settings.Parking
{
    public class ParkingSpotRateSettings
    {
        public static readonly Dictionary<VehicleType, ParkingSpotRates> Rate = new()
        {
            { VehicleType.A, new ParkingSpotRates(hourly: 3, daily: 25) },
            { VehicleType.B, new ParkingSpotRates(hourly: 4, daily: 35) },
            { VehicleType.C, new ParkingSpotRates(hourly: 5, daily: 50) }
        };
    }
}
