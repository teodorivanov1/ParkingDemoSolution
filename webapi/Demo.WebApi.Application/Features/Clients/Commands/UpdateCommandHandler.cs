using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Abstractions.Repositories;
using MediatR;

namespace Demo.WebApi.Application.Features.Clients.Commands
{
    public class UpdateCommandHandler : BaseInitialization, IRequestHandler<UpdateCommand, Response<bool>>
    {
        public UpdateCommandHandler(IClientsRepository clientsRepository)
            : base(clientsRepository)
        {

        }

        public async Task<Response<bool>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            await _clientsRepository.DeleteAsync(request.Id);

            // we are here, not exception (but still bad solution here it is appropriate to use event rater than command)
            // we just need to fire and forget ...
            return new Response<bool>(true);
        }
    }
}
