using static Facets.SharedKernal.AppEnums;

namespace Facets.Core.Events.DTOs;

public sealed record EventDto(Guid Id,
                              string Name,
                              string? Description,
                              string? LogoURL,
                              IReadOnlyList<DateTimeOffset> EventDates,
                              DateTimeOffset VisitorRegistrationStartDate,
                              DateTimeOffset VisitorRegistrationEndDate,
                              EventStatus Status);
