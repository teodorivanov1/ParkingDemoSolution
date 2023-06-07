using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Common.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Demo.WebApi.Application.Features.Parking.Commands
{
    public class EntryCommand : IRequest<Response<EntryCommandResult>>
    {
        public EntryCommand(string plate, VehicleType vehicleType, DiscountType discountType, int availability)
        {
            Plate = plate;
            VehicleType = vehicleType;
            DiscountType = discountType;
            Availability = availability;
        }
        public string? Plate { get; }

        public VehicleType VehicleType { get; }

        public DiscountType DiscountType { get; }

        [JsonIgnore]
        public int Availability { get; set; }
    }
}
