using Demo.WebApi.Application.Abstractions;
using MediatR;

namespace Demo.WebApi.Application.Features.Parking.Commands
{
    public class ExitCommand : IRequest<Response<bool>>
    {
        public ExitCommand(int tiket)
        {
            Tiket = tiket;
        }
        public int Tiket { get; set; }
    }
}
