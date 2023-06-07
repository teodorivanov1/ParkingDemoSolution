using Demo.WebApi.Application.Abstractions.Repositories;

namespace Demo.WebApi.Application.Features.Parking
{
    public abstract class BaseInitialization
    {
        protected readonly IParkingSpotsRepository parkingSpotsRepository;
        protected BaseInitialization(IParkingSpotsRepository parkingSpotsRepository)
        {
            this.parkingSpotsRepository = parkingSpotsRepository ?? throw new ArgumentNullException(nameof(parkingSpotsRepository));
        }
    }
}
