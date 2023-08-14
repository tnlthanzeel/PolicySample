using Facets.Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.StoredProcConfigs;

public sealed class SP_GetUserEventConfig : IEntityTypeConfiguration<SP_GetUserEvent>
{
    public void Configure(EntityTypeBuilder<SP_GetUserEvent> builder)
    {
        builder.HasNoKey();
        builder.ToTable((string)null!);
    }
}
