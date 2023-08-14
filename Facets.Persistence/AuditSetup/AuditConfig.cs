using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.AuditSetup;

internal sealed class AuditConfig : IEntityTypeConfiguration<Audit>
{
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(p => p.UserId).HasMaxLength(128);

        builder.Property(p => p.AuditType).HasConversion<string>().HasMaxLength(16);

        builder.Property(p => p.TableName).IsRequired(true);

        builder.Property(p => p.OldValues).IsRequired(false);

        builder.Property(p => p.NewValues).IsRequired(false);

        builder.Property(p => p.AffectedColumns).IsRequired(false);

        builder.Property(p => p.PrimaryKey).IsRequired().HasMaxLength(128);

        builder.ToTable("AppAuditLogs");
    }
}
