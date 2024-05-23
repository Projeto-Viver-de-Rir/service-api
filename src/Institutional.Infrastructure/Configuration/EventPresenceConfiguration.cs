using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Institutional.Infrastructure.Configuration;

public class EventPresenceConfiguration : IEntityTypeConfiguration<EventPresence>
{
    public void Configure(EntityTypeBuilder<EventPresence> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<EventPresenceId.EfCoreValueConverter>();
        
        builder.Property(x => x.EventId)
            .HasConversion<EventId.EfCoreValueConverter>();
        
        builder.Property(x => x.VolunteerId)
            .HasConversion<VolunteerId.EfCoreValueConverter>();
        
        builder.Property(x => x.CreatedBy)
            .HasConversion<UserId.EfCoreValueConverter>();
        
        builder.Property(x => x.UpdatedBy)
            .HasConversion<UserId.EfCoreValueConverter>();        
    }
}