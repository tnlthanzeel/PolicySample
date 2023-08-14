namespace Facets.Core.Events.DTOs;

public sealed record UpdateEventDto(string Name,
                                    string? Description,
                                    IEnumerable<DateTimeOffset> EventDates,
                                    DateTimeOffset VisitorRegistrationStartDate,
                                    DateTimeOffset VisitorRegistrationEndDate);
