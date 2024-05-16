using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class ConfigConfiguration : IEntityTypeConfiguration<Config>
{
    public void Configure(EntityTypeBuilder<Config> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ConfigId.EfCoreValueConverter>();
        
        builder.Property(x => x.UpdatedBy)
            .HasConversion<UserId.EfCoreValueConverter>();        
    }
}