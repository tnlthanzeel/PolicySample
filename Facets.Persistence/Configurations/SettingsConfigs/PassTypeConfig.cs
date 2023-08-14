using Facets.Core.Passes.Entities;
using Facets.SharedKernal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.SettingsConfigs;

internal sealed class PassTypeConfig : IEntityTypeConfiguration<PassType>
{
    public void Configure(EntityTypeBuilder<PassType> builder)
    {
        builder.HasKey(k => k.Id);

        builder.Property(k => k.Name).IsRequired().HasMaxLength(AppConstants.StringLengths.Description);

        builder.HasData(PassType.SeedDefaultData());

        builder.HasIndex(k => k.Name).IsUnique();
    }
}
