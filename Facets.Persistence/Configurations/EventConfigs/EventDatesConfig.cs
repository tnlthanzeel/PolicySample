using Facets.Core.Events.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.EventConfigs;

internal sealed class EventDatesConfig : IEntityTypeConfiguration<EventDate>
{
    public void Configure(EntityTypeBuilder<EventDate> builder)
    {
        builder.HasKey(k => k.Id);

        builder.ToTable(name: nameof(EventDate),
                       table => table.IsTemporal());
    }
}
