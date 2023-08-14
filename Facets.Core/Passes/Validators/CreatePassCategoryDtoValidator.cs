using Facets.Core.Common.Validators;
using Facets.Core.Passes.DTOs;
using Facets.Core.Passes.Interfaces;
using Facets.SharedKernal;
using Facets.SharedKernal.Interfaces;
using FluentValidation;


namespace Facets.Core.Passes.Validators;

public class CreatePassCategoryDtoValidator : AbstractValidator<CreatePassCategoryDto>
{
    public CreatePassCategoryDtoValidator(IPassCategoryRepository passCategoryRepository,ILoggedInUserService loggedInUser)
    {
        RuleFor(r => r.Name)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithMessage("Pass category name is required")
                    .MaximumLength(AppConstants.StringLengths.Description)
                    .WithMessage($"Pass category name must be less than {AppConstants.StringLengths.Description} characters")
                    .MustAsync(async (passCategoryName, cancellation) =>
                    {
                        bool exists = await passCategoryRepository.IsPassCategoryNameTaken(passCategoryName, cancellationToken: cancellation);
                        return !exists;
                    })
                    .WithMessage("Pass category name name is already taken");

        RuleFor(r => r.Description)
                    .MaximumLength(AppConstants.StringLengths.Description)
                    .WithMessage($"Pass category description must be less than {AppConstants.StringLengths.Description} characters");

        RuleFor(_ => _).ValidateEventId(loggedInUser);
    }
}