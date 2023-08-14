namespace Facets.Core.Passes.DTOs;

public sealed record PassCategoryDto(Guid Id,
                                     Guid EventId,
                                     string Name,
                                     string? Description);
