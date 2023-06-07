using Microsoft.OpenApi.Attributes;

namespace Demo.WebApi.Common.Enums
{
    public enum VehicleType
    {
        [Display("Car/Motorcycle")]
        A,
        [Display("Van")]
        B,
        [Display("Bus/Truck")]
        C
    }
}
