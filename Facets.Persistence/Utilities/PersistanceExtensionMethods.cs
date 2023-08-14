using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Utilities;

internal static class PersistanceExtensionMethods
{
    public static PropertyBuilder<decimal> DecimalPrecision(this PropertyBuilder<decimal> propertyBuilder)
    {
        return propertyBuilder.HasPrecision(18, 2);
    }
}
