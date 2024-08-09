using Institutional.Domain.Entities;
using Institutional.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Institutional.Infrastructure.Configuration;

public class ReportDebtConfiguration : IEntityTypeConfiguration<ReportDebt>
{
    public void Configure(EntityTypeBuilder<ReportDebt> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion<ReportDebtId.EfCoreValueConverter>();
        
        builder.Property(x => x.VolunteerId)
            .HasConversion<VolunteerId.EfCoreValueConverter>();
        
        builder.Property(x => x.CreatedBy)
            .HasConversion<UserId.EfCoreValueConverter>();
    }
}