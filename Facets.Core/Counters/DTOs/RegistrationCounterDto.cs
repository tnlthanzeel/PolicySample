namespace Facets.Core.Counters.DTOs;

public sealed record RegistrationCounterDto(Guid Id, string Name, string? Description, bool IsLocked, Guid EventId);
