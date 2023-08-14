using Facets.Core.Security.Claims;
using Facets.Core.Security.Entities;
using Facets.SharedKernal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facets.Persistence.Configurations.UserConfigs;

internal sealed class IdentityUserClaimConfig : IEntityTypeConfiguration<UserClaim>
{
    public void Configure(EntityTypeBuilder<UserClaim> builder)
    {
        builder.HasData(new UserClaim()
        {
            Id = 1,
            ClaimType = CustomClaimTypes.Permission,
            ClaimValue = ApplicationClaimValues.SuperAdmin.All,
            UserId = AppConstants.SuperAdmin.SuperUserId,
            UserRoleClaimId = 1,
            UserRoleId = Guid.Parse("e24f4cd1-0759-440e-9a2b-6072880392b6")
        });
    }
}
