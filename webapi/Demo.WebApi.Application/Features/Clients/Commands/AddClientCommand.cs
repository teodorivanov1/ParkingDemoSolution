using Demo.WebApi.Application.Abstractions;
using MediatR;

namespace Demo.WebApi.Application.Features.Clients.Commands
{
    public class AddClientCommand : IRequest<Response<BoolResult>>
    {
        public AddClientCommand(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
