using Demo.WebApi.Core.Entities;

namespace Demo.WebApi.Application.Abstractions.Repositories
{
    public interface IParkingSpotsRepository : IGenericRepository<ParkingSpot>
    {
        Task<ParkingSpot?> GetByPlateAsync(string plate);
    }
}
