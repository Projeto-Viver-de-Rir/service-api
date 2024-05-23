using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Institutional.Infrastructure.Configuration;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<VolunteerId.EfCoreValueConverter>();
        
        builder.Property(x => x.AccountId)
            .HasConversion<UserId.EfCoreValueConverter>();
        
        builder.Property(x => x.CreatedBy)
            .HasConversion<UserId.EfCoreValueConverter>();
        
        builder.Property(x => x.UpdatedBy)
            .HasConversion<UserId.EfCoreValueConverter>();        
    }
}