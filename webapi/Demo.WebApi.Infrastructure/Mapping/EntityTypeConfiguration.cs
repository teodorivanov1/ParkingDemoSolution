using Demo.WebApi.Core.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SimpleStoreWeb.Data.EntityConfigurations
{
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
    {
        public const string TablePrefix = "App";
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            BuildEntityType(builder);
        }
        public abstract void BuildEntityType(EntityTypeBuilder<TEntity> builder);
    }
}