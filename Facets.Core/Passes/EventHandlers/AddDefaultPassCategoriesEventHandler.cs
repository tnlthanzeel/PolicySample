using Facets.Core.Events.Events;
using Facets.Core.Passes.Entities;
using Facets.Core.Passes.Interfaces;
using MediatR;

namespace Facets.Core.Passes.EventHandlers;

public sealed class AddDefaultPassCategoriesEventHandler : INotificationHandler<EventCreatingEvent>
{
    private readonly IPassCategoryRepository _passCategoryRepository;

    public AddDefaultPassCategoriesEventHandler(IPassCategoryRepository passCategoryRepository)
    {
        _passCategoryRepository = passCategoryRepository;
    }

    public Task Handle(EventCreatingEvent notification, CancellationToken cancellationToken)
    {
        var eventDefaultPassCategories = PassCategory.GetDefaultData(notification.Event.Id);

        foreach (var eventDefaultPassCategory in eventDefaultPassCategories)
        {
            _passCategoryRepository.AddPassCategory(eventDefaultPassCategory);
        }

        return Task.CompletedTask;
    }
}
