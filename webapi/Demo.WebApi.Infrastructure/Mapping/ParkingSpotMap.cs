using Demo.WebApi.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStoreWeb.Data.EntityConfigurations;

namespace Demo.WebApi.Infrastructure.Mapping
{
    public class ParkingSpotMap : EntityTypeConfiguration<ParkingSpot>
    {
        public override void BuildEntityType(EntityTypeBuilder<ParkingSpot> builder)
        {
            builder.ToTable($"{TablePrefix}ParkingSpots");
            builder.Property(p => p.Plate).HasColumnType("TEXT COLLATE NOCASE").IsRequired();
            builder.Property(p => p.VehicleType).IsRequired();
            builder.Property(p => p.EntryAt).IsRequired();
            builder.Property(p => p.DiscountType).IsRequired();

            builder.Ignore(p => p.TotalMoney);
            builder.HasIndex(b => b.Plate).IsUnique();
        }
    }
}
