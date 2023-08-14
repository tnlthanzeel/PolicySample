using Facets.Core.Common.Validators;
using Facets.Core.Counters.DTOs;
using Facets.Core.Counters.Interfaces;
using Facets.SharedKernal;
using Facets.SharedKernal.Interfaces;
using FluentValidation;

namespace Facets.Core.Counters.Validators;

public sealed class CreateRegistrationCounterDtoValidator : AbstractValidator<CreateRegistrationCounterDto>
{
    public CreateRegistrationCounterDtoValidator(IRegistrationCounterRepository registrationCounterRepository, ILoggedInUserService loggedInUser)
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(AppConstants.StringLengths.FirstName)
            .WithMessage($"Name must be less than {AppConstants.StringLengths.FirstName} characters")
            .MustAsync(async (registrationCounterName, cancellation) =>
                {
                    bool exists = await registrationCounterRepository.IsRegistrationCounterNameTaken(registrationCounterName,
                                                                                                     cancellationToken: cancellation);
                    return !exists;
                })
            .WithMessage("Registration counter name is already taken");

        RuleFor(r => r.Description)
            .MaximumLength(AppConstants.StringLengths.Description)
            .WithMessage($"Description must be less than {AppConstants.StringLengths.Description} characters");


        RuleFor(_ => _).ValidateEventId(loggedInUser);
    }
}
