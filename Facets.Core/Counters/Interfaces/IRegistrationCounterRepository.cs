using Ardalis.Specification;
using Facets.Core.Common.Interfaces;
using Facets.Core.Counters.Entities;
using Facets.SharedKernal.Models;

namespace Facets.Core.Counters.Interfaces;

public interface IRegistrationCounterRepository : IBaseRepository
{
    VisitorRegistrationCounter AddRegistrationCounter(VisitorRegistrationCounter entity);

    Task<bool> IsRegistrationCounterNameTaken(string name, Guid? id = null, CancellationToken cancellationToken = default);

    Task<TResult?> GetProjectedRegistrationCounterBySpec<TResult>(ISpecification<VisitorRegistrationCounter, TResult> specification, CancellationToken token);

    Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<VisitorRegistrationCounter, TResult> specification, CancellationToken token);

    Task<VisitorRegistrationCounter?> GetRegistrationCounterBySpec(ISpecification<VisitorRegistrationCounter> specification, CancellationToken token, bool asTracking = false);
}
