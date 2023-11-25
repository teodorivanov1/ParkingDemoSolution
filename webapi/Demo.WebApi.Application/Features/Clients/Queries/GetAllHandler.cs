using Demo.WebApi.Application.Abstractions.Pagination;
using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Core.Entities.Clients;
using MediatR;

namespace Demo.WebApi.Application.Features.Clients.Queries
{
    public class GetAllHandler : BaseInitialization, IRequestHandler<GetAll, PagedResult<Client>>
    {

        public GetAllHandler(IClientsRepository clientsRepository)
            : base(clientsRepository)
        {
        }
        public async Task<PagedResult<Client>> Handle(GetAll request, CancellationToken cancellationToken)
        {
            const int itemsPerPage = 10;
            // Separation of concerns violation. PagedReponse should be generated in the application layer, not in the repository
            var paged = await _clientsRepository.GetPagedReponseAsync(request.Page, itemsPerPage);

            return paged;
        }
    }
}
