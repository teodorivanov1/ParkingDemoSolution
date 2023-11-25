using Demo.WebApi.Core.Entities.Addresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStoreWeb.Data.EntityConfigurations;

namespace Demo.WebApi.Infrastructure.Mapping
{
    public class AddressesMap : EntityTypeConfiguration<Address>
    {
        public override void BuildEntityType(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable($"{TablePrefix}Addresses");
            builder.Property(p => p.AddressLine).HasColumnType("TEXT COLLATE NOCASE").IsRequired();
            builder.Property(p => p.ZipCode).HasColumnType("TEXT COLLATE NOCASE").IsRequired(false);
            builder.HasOne(e => e.Client)
                .WithMany(e => e.Addresses)
                .HasForeignKey(e => e.ClientId)
                .IsRequired();
        }
    }
}
