using Ardalis.Specification;
using Facets.Core.Common.Interfaces;
using Facets.Core.Events.Entities;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Events.Interfaces;

public interface IEventRepository : IBaseRepository
{
    Event AddEvent(Event entity);
    Task<TResult?> GetProjectedEventBySpec<TResult>(ISpecification<Event, TResult> specification, CancellationToken token);

    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<Event, TResult> specification, CancellationToken token);

    Task<Event?> GetEventBySpec(ISpecification<Event> specification, CancellationToken token, bool asTracking = false);

    Task<bool> IsEventNameTaken(string email, Guid? companyId = null, CancellationToken cancellationToken = default);
    Task<ResponseResult> CanDeleteEvent(Guid id, CancellationToken token);

    Task<bool> DoesAllProvidedEventsExist(IEnumerable<Guid> eventId, CancellationToken cancellation);
}
