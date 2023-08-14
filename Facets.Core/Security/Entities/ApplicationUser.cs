using Facets.SharedKernal.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Facets.Core.Security.Entities;

public sealed class ApplicationUser : IdentityUser<Guid>, IAggregateRoot, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public UserProfile UserProfile { get; set; } = null!;

    public void Deleted()
    {
        IsDeleted = true;
    }

    public void UpdateNames(string firstName, string lastName)
    {
        UserProfile.FirstName = firstName;
        UserProfile.LastName = lastName;
    }

    public void UpdateTimeZone(string timeZone)
    {
        UserProfile.TimeZone = timeZone;
    }
}
