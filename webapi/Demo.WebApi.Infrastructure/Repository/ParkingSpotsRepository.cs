using Demo.WebApi.Application.Abstractions.Repositories;
using Demo.WebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.WebApi.Infrastructure.Repository
{
    public class ParkingSpotsRepository : GenericRepository<ParkingSpot>, IParkingSpotsRepository
    {
        private readonly DbSet<ParkingSpot> parkingSpots;

        public ParkingSpotsRepository(AppDbContext dbContext) : base(dbContext)
        {
            parkingSpots = dbContext.Set<ParkingSpot>() ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<ParkingSpot?> GetByPlateAsync(string plate)
        {
            _ = plate ?? throw new ArgumentNullException(nameof(plate));
            return await parkingSpots
                .Where(x => EF.Functions
                    .Like(x.Plate!, $"%{plate}%"))
                .FirstOrDefaultAsync();
        }
    }
}
