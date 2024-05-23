using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Institutional.Infrastructure.Configuration;

public class ScheduleEventConfiguration : IEntityTypeConfiguration<ScheduleEvent>
{
    public void Configure(EntityTypeBuilder<ScheduleEvent> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ScheduleEventId.EfCoreValueConverter>();
        
        builder.Property(x => x.CreatedBy)
            .HasConversion<UserId.EfCoreValueConverter>();
        
        builder.Property(x => x.UpdatedBy)
            .HasConversion<UserId.EfCoreValueConverter>();        
    }
}