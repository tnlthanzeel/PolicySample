namespace Facets.Core.Security.Dtos;

public record UpdateRoleClaimsDto(IEnumerable<string> Permissions);
