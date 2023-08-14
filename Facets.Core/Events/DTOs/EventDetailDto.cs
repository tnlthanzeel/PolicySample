using static Facets.SharedKernal.AppEnums;

namespace Facets.Core.Events.DTOs;

public sealed record EventDetailDto(Guid Id,
                                    string Name,
                                    string? Description,
                                    string? LogoURL,
                                    IReadOnlyList<DateTimeOffset> EventDates,
                                    DateTimeOffset VisitorRegistrationStartDate,
                                    DateTimeOffset VisitorRegistrationEndDate,
                                    EventStatus Status,
                                    bool PublishedToPublicSite,
                                    DateTimeOffset EventStatusLastUpdatedOn,
                                    DateTimeOffset? PublishedToPublicSiteOn
                                    );
