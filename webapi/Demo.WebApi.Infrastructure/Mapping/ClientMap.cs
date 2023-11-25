using Demo.WebApi.Core.Entities.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleStoreWeb.Data.EntityConfigurations;

namespace Demo.WebApi.Infrastructure.Mapping
{
    public class ClientsMap : EntityTypeConfiguration<Client>
    {
        public override void BuildEntityType(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable($"{TablePrefix}Clients");
            builder.Property(p => p.Name).HasColumnType("TEXT COLLATE NOCASE").IsRequired();
            builder.HasMany(e => e.Addresses)
                .WithOne(e => e.Client)
                .HasForeignKey(e => e.ClientId)
                .IsRequired();
        }
    }
}
