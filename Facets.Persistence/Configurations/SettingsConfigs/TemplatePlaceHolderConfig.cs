using Facets.Core.Settings.Common;
using Facets.SharedKernal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.SettingsConfigs;

internal sealed class TemplatePlaceHolderConfig : IEntityTypeConfiguration<TemplatePlaceHolder>
{
    public void Configure(EntityTypeBuilder<TemplatePlaceHolder> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.DisplayName).IsRequired().HasMaxLength(AppConstants.StringLengths.Notes);

        builder.Property(x => x.Token).IsRequired().HasMaxLength(AppConstants.StringLengths.FirstName);

        builder.HasIndex(x => x.DisplayName).IsUnique();
        builder.HasIndex(x => x.Token).IsUnique();

        builder.HasData(TemplatePlaceHolder.GetDefaultData());
    }
}
