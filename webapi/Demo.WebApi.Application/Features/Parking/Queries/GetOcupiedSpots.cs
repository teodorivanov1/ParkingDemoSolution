using Demo.WebApi.Application.Abstractions;
using MediatR;

namespace Demo.WebApi.Application.Features.Parking.Queries
{
    public class GetOcupiedSpots : IRequest<Response<int>>
    {
        public GetOcupiedSpots()
        {

        }
    }
}
