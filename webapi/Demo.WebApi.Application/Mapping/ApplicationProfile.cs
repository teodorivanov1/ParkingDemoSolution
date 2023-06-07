using AutoMapper;
using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Features.Parking.Commands;
using Demo.WebApi.Core.Abstraction;
using Demo.WebApi.Core.Entities;

namespace Demo.WebApi.Application.Mapping
{
    // When application code grows,
    // it's a good practice to make separation on different profiles(files) for each feature command/query.
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile(ICoreValidator<ParkingSpot> parkingSpotValidator)
        {
            var x = parkingSpotValidator;
            CreateMap<EntryCommand, ParkingSpot>()
                .ConstructUsing(src => new ParkingSpot(
                    src.Plate!,
                    src.VehicleType,
                    src.DiscountType,
                    parkingSpotValidator));

            CreateMap<ParkingSpot, Response<EntryCommandResult>>()
                .ConstructUsing(src => new Response<EntryCommandResult>()
                {
                    Succeeded = src.Id > 0,
                    Data = new EntryCommandResult() { Ticket = src.Id }
                });
        }
    }
}
