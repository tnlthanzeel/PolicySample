using Facets.Core.Passes.Entities;
using Facets.Core.Passes.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Facets.Persistence.Repositories.Passes;

internal sealed class PassCategoryRepository : BaseRepository, IPassCategoryRepository
{
    private readonly DbSet<PassCategory> _table;

    public PassCategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
        _table = _dbContext.Set<PassCategory>();
    }

    public PassCategory AddPassCategory(PassCategory entity)
    {
        _table.Add(entity);

        return entity;
    }
    
    public async Task<bool> IsPassCategoryNameTaken(string name, CancellationToken cancellationToken)
    {
        return await _table.AnyAsync(f => f.Name.ToLower() == name.ToLower(), cancellationToken: cancellationToken);
    }
}
