using Demo.WebApi.Common.Enums;

namespace Demo.WebApi.Core.Settings.Parking
{
    public class PlacesSettings
    {
        public static Dictionary<VehicleType, int> PlacesPerVehicle = new()
        {
            { VehicleType.A, 1 },
            { VehicleType.B, 2 },
            { VehicleType.C, 8 },
        };
    }
}
