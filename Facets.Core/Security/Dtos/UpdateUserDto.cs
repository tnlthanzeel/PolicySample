namespace Facets.Core.Security.Dtos;

public sealed record UpdateUserDto
    (
    string Email,
    string FirstName,
    string LastName,
    string Role,
    string TimeZone,
    IEnumerable<string> Permissions,
    IEnumerable<Guid> EventIds);
