using Facets.Core.Events.DTOs;
using FluentValidation;
using static Facets.SharedKernal.AppEnums;

namespace Facets.Core.Events.Validators;

public sealed class UpdateEventDtoStatusDtoValidator : AbstractValidator<UpdateEventStatusDto>
{
    public UpdateEventDtoStatusDtoValidator()
    {
        RuleFor(r => r.EventStatus)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Invalid event status")
            .IsInEnum()
            .WithMessage("Invalid event status")
            .NotEqual(EventStatus.None)
            .WithMessage("Invalid event status cannot be None");
    }
}
