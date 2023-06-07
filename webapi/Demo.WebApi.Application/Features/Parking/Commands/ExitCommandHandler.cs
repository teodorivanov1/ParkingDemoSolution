using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Abstractions.Repositories;
using MediatR;

namespace Demo.WebApi.Application.Features.Parking.Commands
{
    public class ExitCommandHandler : BaseInitialization, IRequestHandler<ExitCommand, Response<bool>>
    {
        public ExitCommandHandler(IParkingSpotsRepository parkingSpotsRepository)
            : base(parkingSpotsRepository)
        {

        }

        public async Task<Response<bool>> Handle(ExitCommand request, CancellationToken cancellationToken)
        {
            await parkingSpotsRepository.DeleteAsync(request.Tiket);

            // we are here, not exception (but still bad solution here it is appropriate to use event rater than command)
            // we just need to fire and forget ...
            return new Response<bool>(true);
        }
    }
}
