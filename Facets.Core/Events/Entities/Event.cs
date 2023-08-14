using Facets.Core.Events.Events;
using Facets.SharedKernal.Interfaces;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;
using static Facets.SharedKernal.AppEnums;

namespace Facets.Core.Events.Entities;

public sealed class Event : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public bool PublishedToPublicSite { get; private set; }
    public DateTimeOffset? PublishedToPublicSiteOn { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }
    public string? LogoUrl { get; private set; }

    private readonly List<EventDate> _eventDates = new();
    public IReadOnlyCollection<EventDate> EventDates => _eventDates.AsReadOnly();

    public DateTimeOffset VisitorRegistrationStartsOn { get; private set; }
    public DateTimeOffset VisitorRegistrationEndsOn { get; private set; }

    public EventStatus Status { get; private set; } = EventStatus.Inactive;

    public DateTimeOffset EventStatusLastUpdatedOn { get; private set; }

    private Event() { }

    public Event(string name,
                 string? description,
                 IEnumerable<DateTimeOffset> eventDates,
                 DateTimeOffset visitorRegistrationStartDate,
                 DateTimeOffset visitorRegistrationEndDate)
    {
        PublishedToPublicSite = false;
        Name = name;
        Description = description;
        VisitorRegistrationStartsOn = visitorRegistrationStartDate;
        VisitorRegistrationEndsOn = visitorRegistrationEndDate;
        Status = EventStatus.Inactive;
        EventStatusLastUpdatedOn = DateTimeOffset.UtcNow;

        foreach (var eventDate in eventDates)
        {
            _eventDates.Add(new(eventDate));
        }

        RegisterDomainEvent(new EventCreatingEvent(isPreDomainEvent: true, this));
    }

    internal void SetLogoUrl(string uri) => LogoUrl = uri;

    internal void RemoveEventLogo() => LogoUrl = null;

    internal ResponseResult UpdateEventInfo(string name,
                                            string? description,
                                            IEnumerable<DateTimeOffset> eventDates,
                                            DateTimeOffset visitorRegistrationStartDate,
                                            DateTimeOffset visitorRegistrationEndDate)
    {
        Name = name;
        Description = description;
        VisitorRegistrationStartsOn = visitorRegistrationStartDate;
        VisitorRegistrationEndsOn = visitorRegistrationEndDate;

        _eventDates.Clear();

        foreach (var eventDate in eventDates)
        {
            _eventDates.Add(new(eventDate));
        }

        return new();
    }

    internal void Delete() => IsDeleted = true;

    internal void UpdateEventStatus(EventStatus eventStatus)
    {
        Status = eventStatus;
        EventStatusLastUpdatedOn = DateTimeOffset.UtcNow;

        if (EventStatus.Active == Status)
        {
            PublishedToPublicSite = true;
            PublishedToPublicSiteOn = DateTimeOffset.UtcNow;
        }
        else
        {
            PublishedToPublicSite = false;
        }
    }
}
