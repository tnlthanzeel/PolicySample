using Facets.Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.UserConfigs;

internal sealed class UserAssignedEventConfig : IEntityTypeConfiguration<UserAssignedEvent>
{
    public void Configure(EntityTypeBuilder<UserAssignedEvent> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasQueryFilter(e => e.Event.IsDeleted == false);
    }
}
