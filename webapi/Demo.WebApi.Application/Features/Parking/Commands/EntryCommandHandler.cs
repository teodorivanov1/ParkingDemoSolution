using AutoMapper;
using Demo.WebApi.Application.Abstractions;
using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Application.Exceptions;
using Demo.WebApi.Core.Entities;
using MediatR;

namespace Demo.WebApi.Application.Features.Parking.Commands
{
    public class EntryCommandHandler : BaseInitialization, IRequestHandler<EntryCommand, Response<EntryCommandResult>>
    {
        public const string NotEnoughSpaceMessage = "There is not enough space in the parking lot.";

        private readonly IMapper mapper;

        public EntryCommandHandler(IParkingSpotsRepository parkingSpotsRepository, IMapper mapper)
            : base(parkingSpotsRepository)
        {
            this.mapper = mapper;
        }

        public async Task<Response<EntryCommandResult>> Handle(EntryCommand request, CancellationToken cancellationToken)
        {
            var requestedParkingSpot = mapper.Map<ParkingSpot>(request);
            if (request.Availability < requestedParkingSpot.Places)
            {
                throw new ApiException(NotEnoughSpaceMessage);
            }
            // For demo purposes I have not used cancellation tokens for the async operations in the repository
            // but the interface enforce me to have it as a parameter
            var obtainedSpot = await parkingSpotsRepository.AddAsync(requestedParkingSpot);

            return mapper.Map<Response<EntryCommandResult>>(obtainedSpot);
        }
    }
}
