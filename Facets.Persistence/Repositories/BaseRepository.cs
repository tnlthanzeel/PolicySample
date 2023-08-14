using Facets.SharedKernal.Models;
using Microsoft.EntityFrameworkCore;
using MR.EntityFrameworkCore.KeysetPagination;

namespace Facets.Persistence.Repositories;

public abstract class BaseRepository
{
    protected readonly AppDbContext _dbContext;

    public BaseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        return await _dbContext.SaveChangesAsync(token);
    }

    protected async Task<IReadOnlyList<TEntity>> ApplyKeysetPageInfo<TEntity>(Action<KeysetPaginationBuilder<TEntity>> builderAction, KeysetPaginator paginator, KeysetPageInfo pageInfo, IQueryable<TEntity> query, TEntity? reference, CancellationToken token) where TEntity : EntityBase
    {
        var keysetContext = query.KeysetPaginate(builderAction,
                                                 paginator.Next ? KeysetPaginationDirection.Forward : KeysetPaginationDirection.Backward,
                                                 reference);

        var entities = await keysetContext.Query
                                          .Take(paginator.PageSize)
                                          .ToListAsync(token);

        keysetContext.EnsureCorrectOrder(entities);

        pageInfo.HasPreviousPage = paginator.IsFirstPage is true ? false : await keysetContext.HasPreviousAsync(entities);

        pageInfo.HasNextPage = await keysetContext.HasNextAsync(entities);

        pageInfo.NextPageReference = pageInfo.HasNextPage is true ? entities.LastOrDefault()?.Id.ToString() : null;

        pageInfo.PreviousPageReference = pageInfo.HasPreviousPage is true ? entities.FirstOrDefault()?.Id.ToString() : null;

        return entities;
    }
}

