using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Facets.Core.Counters.Entities;
using Facets.Core.Counters.Interfaces;
using Facets.SharedKernal.Models;
using Microsoft.EntityFrameworkCore;

namespace Facets.Persistence.Repositories.Counters;

public sealed class RegistrationCounterRepository : BaseRepository, IRegistrationCounterRepository
{
    private readonly DbSet<VisitorRegistrationCounter> _table;

    public RegistrationCounterRepository(AppDbContext dbContext) : base(dbContext)
    {
        _table = _dbContext.Set<VisitorRegistrationCounter>();
    }

    public VisitorRegistrationCounter AddRegistrationCounter(VisitorRegistrationCounter entity)
    {
        _table.Add(entity);
        return entity;
    }

    public async Task<TResult?> GetProjectedRegistrationCounterBySpec<TResult>(ISpecification<VisitorRegistrationCounter, TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var projectedResult = await query.FirstOrDefaultAsync(cancellationToken: token);

        return projectedResult;
    }

    public async Task<bool> IsRegistrationCounterNameTaken(string name, Guid? id = null, CancellationToken cancellationToken = default)
    {
        return await _table.AnyAsync(f => f.Name == name && (id == null || f.Id != id.Value), cancellationToken);
    }

    public async Task<(IReadOnlyList<TResult> list, int totalRecords)> GetProjectedListBySpec<TResult>(Paginator paginator, ISpecification<VisitorRegistrationCounter, TResult> specification, CancellationToken token)
    {
        var query = _table.WithSpecification(specification);

        var totalRecords = await query.CountAsync(cancellationToken: token);

        var projectedResult = await query.Skip((paginator.PageNumber - 1) * paginator.PageSize)
                                         .Take(paginator.PageSize)
                                         .ToListAsync(cancellationToken: token);

        return (projectedResult, totalRecords);
    }

    public async Task<VisitorRegistrationCounter?> GetRegistrationCounterBySpec(ISpecification<VisitorRegistrationCounter> specification, CancellationToken token, bool asTracking = false)
    {
        var query = asTracking ? _table.AsTracking() : _table;

        return await query.WithSpecification(specification).FirstOrDefaultAsync(cancellationToken: token);

    }
}
