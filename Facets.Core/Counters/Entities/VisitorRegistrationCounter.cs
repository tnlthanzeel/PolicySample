using Facets.Core.Events.Entities;
using Facets.SharedKernal.Exceptions;
using Facets.SharedKernal.Interfaces;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Counters.Entities;

public sealed class VisitorRegistrationCounter : EntityBase, ICreatedAudit, IUpdatedAudit, IDeletedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsLocked { get; private set; }

    public Guid EventId { get; set; }
    public Event Event { get; private set; } = null!;

    private VisitorRegistrationCounter() { }

    public VisitorRegistrationCounter(string name, string? description, Guid eventId)
    {
        Name = name;
        Description = description;
        IsLocked = false;
        EventId = eventId;
    }
    internal ResponseResult UpdateRegistrationCounterInfo(string name, string? description)
    {
        Name = name;
        Description = description;
        return new();
    }

    internal ResponseResult Delete()
    {
        if (IsLocked is true) return new(new OperationFailedException(nameof(IsLocked), "Registration counter is locked"));

        IsDeleted = true;
        return new();
    }
}
