using Facets.Core.Common.Interfaces;
using Facets.Core.Passes.Entities;

namespace Facets.Core.Passes.Interfaces;

public interface IPassCategoryRepository : IBaseRepository
{
    PassCategory AddPassCategory(PassCategory entity); 
    
    Task<bool> IsPassCategoryNameTaken(string name, CancellationToken cancellationToken);
}
