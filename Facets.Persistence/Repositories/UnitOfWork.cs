using Facets.Core.Common.Interfaces;

namespace Facets.Persistence.Repositories;

internal sealed class UnitOfWork : BaseRepository, IUnitOfWork
{
    public UnitOfWork(AppDbContext dbContext) : base(dbContext) { }
}
