using Facets.Core.Events.Entities;
using Facets.SharedKernal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.EventConfigs;

internal sealed class EventConfig : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(k => k.Id);
        builder.HasQueryFilter(k => k.IsDeleted == false);

        builder.Property(k => k.Name).IsRequired().HasMaxLength(AppConstants.StringLengths.Description);
        builder.Property(k => k.Description).IsRequired(false).HasMaxLength(AppConstants.StringLengths.Description);
        builder.Property(k => k.LogoUrl).IsRequired(false).HasMaxLength(AppConstants.StringLengths.Description);

        var navigation = builder.Metadata.FindNavigation(nameof(Event.EventDates));
        navigation!.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(s => s.EventDates)
               .WithOne(s => s.Event)
               .HasForeignKey(f => f.EventId);

        builder.ToTable(name: nameof(Event),
                        table => table.IsTemporal());

        builder.HasIndex(s => s.Name).IsUnique();

    }
}
