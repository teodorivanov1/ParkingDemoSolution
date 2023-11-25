using AutoMapper;
using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Core.Entities.Clients;
using MediatR;

namespace Demo.WebApi.Application.Features.Clients.Commands
{
    public class AddClientCommandHandler : BaseInitialization, IRequestHandler<AddClientCommand, Response<BoolResult>>
    {
        private readonly IMapper _mapper;

        public AddClientCommandHandler(IClientsRepository clientsRepository, IMapper mapper)
            : base(clientsRepository)
        {
            _mapper = mapper;
        }

        public async Task<Response<BoolResult>> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            Client? added = await _clientsRepository.AddAsync(_mapper.Map<Client>(request));

            BoolResult result = new() { Value = added is not null || added!.Id != 0 };

            // Here we can use exsisting response builder
            return new Response<BoolResult>() { Succeeded = result.Value };
        }
    }
}
