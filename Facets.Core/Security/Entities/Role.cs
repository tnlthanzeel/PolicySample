using Facets.SharedKernal.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Facets.Core.Security.Entities;

public sealed class Role : IdentityRole<Guid>, IAggregateRoot
{
    public Role() { }

    public Role(string roleName) : base(roleName) { }

    public bool IsDefault { get; set; } = false;
}

