using Facets.Core.Counters.Entities;
using Facets.SharedKernal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.CounterConfigs;

internal sealed class VisitorRegistrationCounterConfig : IEntityTypeConfiguration<VisitorRegistrationCounter>
{
    public void Configure(EntityTypeBuilder<VisitorRegistrationCounter> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(AppConstants.StringLengths.FirstName);
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(AppConstants.StringLengths.Description);

        builder.HasIndex(s => new { s.Name, s.EventId }).IsUnique();

        builder.HasOne(x => x.Event)
               .WithMany()
               .HasForeignKey(x => x.EventId);

        builder.ToTable(name: nameof(VisitorRegistrationCounter),
                        table => table.IsTemporal());
    }
}
