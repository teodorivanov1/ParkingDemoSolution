using Demo.WebApi.Core.Entities;
using Demo.WebApi.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Demo.WebApi.Infrastructure
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = configuration.GetConnectionString("DB")!;
            optionsBuilder.UseSqlite(connection);
        }

        public DbSet<ParkingSpot> ParkingSpots { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(nameof(builder));
            builder.ApplyConfiguration(new ParkingSpotMap());

            base.OnModelCreating(builder);
        }
    }
}