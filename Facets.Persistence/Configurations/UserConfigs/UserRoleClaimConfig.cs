using Facets.Core.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.UserConfigs;

internal sealed class UserRoleClaimConfig : IEntityTypeConfiguration<IdentityRoleClaim<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<Guid>> builder)
    {
        builder.HasData(new IdentityRoleClaim<Guid>
        {
            Id = 1,
            RoleId = Guid.Parse("e24f4cd1-0759-440e-9a2b-6072880392b6"),
            ClaimType = CustomClaimTypes.Permission,
            ClaimValue = ApplicationClaimValues.SuperAdmin.All
        });
    }
}
