using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Institutional.Infrastructure.Configuration;

public class ScheduleEventCoordinatorConfiguration : IEntityTypeConfiguration<ScheduleEventCoordinator>
{
    public void Configure(EntityTypeBuilder<ScheduleEventCoordinator> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ScheduleEventCoordinatorId.EfCoreValueConverter>();
        
        builder.Property(x => x.ScheduleEventId)
            .HasConversion<ScheduleEventId.EfCoreValueConverter>();
        
        builder.Property(x => x.VolunteerId)
            .HasConversion<VolunteerId.EfCoreValueConverter>();
        
        builder.Property(x => x.CreatedBy)
            .HasConversion<UserId.EfCoreValueConverter>();
        
    }
}