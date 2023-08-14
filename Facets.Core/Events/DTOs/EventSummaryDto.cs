using static Facets.SharedKernal.AppEnums;

namespace Facets.Core.Events.DTOs;

public sealed record EventSummaryDto(Guid Id,
                                     string Name,
                                     string? Description,
                                     string? LogoURL,
                                     DateTimeOffset VisitorRegistrationStartDate,
                                     DateTimeOffset VisitorRegistrationEndDate,
                                     EventStatus EventStatus);
