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

        public DbSet<ClientsMap> Clients { get; set; }
        public DbSet<AddressesMap> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(nameof(builder));
            builder.ApplyConfiguration(new ClientsMap());
            builder.ApplyConfiguration(new AddressesMap());

            base.OnModelCreating(builder);
        }
    }
}