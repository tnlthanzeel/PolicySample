using Microsoft.AspNetCore.Http;

namespace Facets.Core.Events.DTOs;

public sealed record CreateEventDto(string Name,
                                    string? Description,
                                    IEnumerable<DateTimeOffset> EventDates,
                                    DateTimeOffset VisitorRegistrationStartDate,
                                    DateTimeOffset VisitorRegistrationEndDate);
