using Facets.Core.Events.DTOs;
using Facets.Core.Events.Interfaces;
using Facets.SharedKernal;
using FluentValidation;

namespace Facets.Core.Events.Validators;

public sealed class UpdateEventDtoValidator : AbstractValidator<UpdateEventDto>
{
    public UpdateEventDtoValidator(IEventRepository eventRepository)
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(AppConstants.StringLengths.Description)
            .WithMessage($"Name must be less than {AppConstants.StringLengths.Description} characters");

        RuleFor(r => r.Description)
            .MaximumLength(AppConstants.StringLengths.Description)
            .WithMessage($"Description must be less than {AppConstants.StringLengths.Description} characters");

        RuleFor(r => r.EventDates)
            .NotEmpty().WithMessage("Atleast one event date is required");

        RuleFor(r => r.EventDates)
            .Must((eventDates) =>
            {
                var anyDuplicate = eventDates.GroupBy(x => x.Date).Any(g => g.Count() > 1);

                return !anyDuplicate;
            }).WithMessage("Event dates cannot be duplicated");

        RuleFor(r => r.VisitorRegistrationStartDate)
            .Must((model, visitorRegistrationStartDate) =>
            {
                return model.EventDates.All(x => x.Date >= visitorRegistrationStartDate.Date);
            }).WithMessage("Event dates should be on or after visitor registration start date");

        RuleFor(r => r.VisitorRegistrationStartDate)
           .NotEmpty()
           .WithMessage("Visitor registration start date is required")
           .Must((model, visitorRegistrationStartDate) =>
           {
               DateTimeOffset firstEventDate = model.EventDates.OrderBy(x => x).First();

               return visitorRegistrationStartDate.Date <= firstEventDate.Date;
           })
           .WithMessage("Visitor registration start date must be on or before the first event date");

        RuleFor(r => r.VisitorRegistrationEndDate)
           .Cascade(CascadeMode.Stop)
           .NotEmpty()
           .WithMessage("Visitor registration end date is required")
           .Must((model, visitorRegistrationEndDate) =>
            {
                DateTimeOffset lastEventDate = model.EventDates.OrderByDescending(x => x).First();

                return (visitorRegistrationEndDate.Date >= model.VisitorRegistrationStartDate.Date) &&
                       (visitorRegistrationEndDate.Date < lastEventDate.Date);
            })
           .WithMessage("Visitor registration end date must be on or after the visitor registration start date & It cannot be after the last event date");

    }
}
