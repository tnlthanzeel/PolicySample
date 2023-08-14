using Facets.Core.Common.Validators;
using Facets.Core.Passes.DTOs;
using Facets.Core.Passes.Entities;
using Facets.Core.Passes.Interfaces;
using Facets.Core.Passes.Validators;
using Facets.SharedKernal.Interfaces;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Passes.Services;

public sealed class PassCategoryService : IPassCategoryService

{
    private readonly IModelValidator _validator;
    private readonly IPassCategoryRepository _passCategoryRepository;
    private readonly ILoggedInUserService _loggedInUser;

    public PassCategoryService(IModelValidator modelValidator, IPassCategoryRepository passCategoryRepository, ILoggedInUserService loggedInUserService)
    {
        _validator = modelValidator;
        _passCategoryRepository = passCategoryRepository;
        _loggedInUser = loggedInUserService;
    }

    public async Task<ResponseResult<PassCategoryDto>> CreatePassCategory(CreatePassCategoryDto model, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync<CreatePassCategoryDtoValidator, CreatePassCategoryDto>(model, cancellationToken);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        PassCategory passCategory = new(_loggedInUser.FacetsEventId, model.Name, model.Description);

        _passCategoryRepository.AddPassCategory(passCategory);

        await _passCategoryRepository.SaveChangesAsync(cancellationToken);

        return new(new PassCategoryDto(passCategory.Id, passCategory.EventId, passCategory.Name, passCategory.Description));
    }
}
