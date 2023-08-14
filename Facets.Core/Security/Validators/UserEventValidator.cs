using Facets.Core.Events.Interfaces;
using FluentValidation;

namespace Facets.Core.Security.Validators;

public static class UserEventValidator
{
    public static IRuleBuilderOptions<T, IEnumerable<Guid>> UserAssignedEventValidation<T>(this IRuleBuilderInitial<T, IEnumerable<Guid>> rule, IEventRepository eventRepository)
    {
        return rule
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Event IDs cannot be null")
                .NotEmpty()
                .WithMessage("Event IDs cannot be an empty list")
                .Must(eventIds => eventIds.All(eventId => Guid.Empty != eventId))
                    .WithMessage("Event IDs cannot contain empty guids")
                 .MustAsync(async (eventIds, cancellation) =>
                 {
                     bool doesAllProvidedEventsExist = await eventRepository.DoesAllProvidedEventsExist(eventIds, cancellation);
                     return doesAllProvidedEventsExist;
                 }).WithMessage("Some Event Ids does not exist");
    }
}
