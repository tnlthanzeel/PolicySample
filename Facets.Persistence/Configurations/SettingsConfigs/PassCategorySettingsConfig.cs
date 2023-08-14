using Facets.Core.Passes.Entities;
using Facets.Persistence.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.SettingsConfigs;

internal sealed class PassCategorySettingsConfig : IEntityTypeConfiguration<PassCategorySettings>
{
    public void Configure(EntityTypeBuilder<PassCategorySettings> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(a => a.PassCategory)
               .WithMany(a => a.PassCategorySettings)
               .IsRequired(false)
               .HasForeignKey(a => a.PassCategoryId);

        builder.Property(p => p.Rate).DecimalPrecision();
        builder.Property(p => p.DiscountedRate).DecimalPrecision();

        builder.ToTable(name: nameof(PassCategorySettings),
                       table => table.IsTemporal());

    }
}
