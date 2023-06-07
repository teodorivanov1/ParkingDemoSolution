using Demo.WebApi.Application.Features.Parking.Commands;
using Demo.WebApi.Common.Enums;

namespace Demo.Tests.Api.Data
{
    public class EntryCommandFactory
    {
        private const int Availability = 350;

        public static EntryCommand Create(
            string plate = "XX1111XX",
            VehicleType vehicleType = VehicleType.A,
            DiscountType discountType = DiscountType.None)
        => new(plate, vehicleType, discountType, Availability);
    }
}
