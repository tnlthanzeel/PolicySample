using Facets.Core.Passes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.SettingsConfigs;

internal sealed class PassTemplateConfig : IEntityTypeConfiguration<PassTemplate>
{
    public void Configure(EntityTypeBuilder<PassTemplate> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(p => p.HTMLText).IsRequired();
    }
}
