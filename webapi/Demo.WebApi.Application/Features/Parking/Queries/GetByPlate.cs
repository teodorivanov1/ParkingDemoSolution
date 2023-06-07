using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Core.Entities;
using MediatR;

namespace Demo.WebApi.Application.Features.Parking.Queries
{
    public class GetByPlate : IRequest<Response<ParkingSpot>>
    {
        public GetByPlate(string plateNumber)
        {
            PlateNumber = plateNumber;
        }
        public string PlateNumber { get; }
    }
}
