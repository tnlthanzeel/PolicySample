using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Facets.Core.Events.Entities;
using Facets.Core.Events.Interfaces;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;
using Microsoft.EntityFrameworkCore;

namespace Facets.Persistence.Repositories.Events;

internal sealed class EventRepository : BaseRepository, IEventRepository
{
    private readonly DbSet<Event> _table;

    public EventRepository(AppDbContext dbContext) : base(dbContext)
    {

        _table = _dbContext.Set<Event>();
    }


    public Event AddEvent(Event @event)
    {
        _table.Add(@event);

        return @event;
    }

    public Task<ResponseResult> CanDeleteEvent(Guid id, CancellationToken token)
    {
        return Task.FromResult(new ResponseResult());
        // TODO: once visitor and team member implementations are done, need to validate if any visitor or team member exist for the event before deletion
    }

    public async Task<Event?> GetEventBySpec(ISpecification<Event> specification, CancellationToken token, bool asTracking = false)
    {
        var query = asTracking ? _table.AsTracking() : _table;

        var @event = await query.WithSpecification(specification).FirstOrDefaultAsync(cancellationToken: token);

        return @event;
    }

    public async Task<TResult?> GetProjectedEventBySpec<TResult>(ISpecification<Event, TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var projectedResult = await query.FirstOrDefaultAsync(cancellationToken: token);

        return projectedResult;
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<Event, TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var totalRecords = await query.CountAsync(cancellationToken: token);

        var projectedResult = await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                         .Take(paginator.PageSize)
                                         .ToListAsync(cancellationToken: token);

        return (projectedResult, totalRecords);
    }

    public async Task<bool> IsEventNameTaken(string name, Guid? eventId = null, CancellationToken cancellationToken = default)
    {
        var isEmailTaken = await _table.AnyAsync(f => f.Name == name && (eventId == null || f.Id != eventId.Value), cancellationToken);

        return isEmailTaken;
    }

    public async Task<bool> DoesAllProvidedEventsExist(IEnumerable<Guid> eventId, CancellationToken cancellation)
    {
        eventId = eventId.Distinct().ToList();

        var eventIds = await _table.AsNoTracking()
                                   .Where(w => eventId.Contains(w.Id))
                                   .Select(s => s.Id)
                                   .ToListAsync(cancellationToken: cancellation);

        var doesAllProvidedEventsExists = eventId.All(a => eventIds.Contains(a));

        return doesAllProvidedEventsExists;
    }
}
