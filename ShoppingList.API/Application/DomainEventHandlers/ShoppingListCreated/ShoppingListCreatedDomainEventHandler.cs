using EventBus.Abstractions;
using MediatR;
using ShoppingList.API.Application.IntegrationEvents;
using ShoppingList.API.Application.IntegrationEvents.Events;
using ShoppingList.Domain.Events;

namespace ShoppingList.API.Application.DomainEventHandlers.ShoppingListCreated
{
    public class ShoppingListCreatedDomainEventHandler : INotificationHandler<ShoppingListCreatedDomainEvent>
    {
        private readonly IShoppingListIntegrationService _shoppingListIntegrationService;

        public ShoppingListCreatedDomainEventHandler(IShoppingListIntegrationService shoppingListIntegrationService)
        {
            _shoppingListIntegrationService = shoppingListIntegrationService ?? throw new ArgumentNullException(nameof(shoppingListIntegrationService));
        }

        public Task Handle(ShoppingListCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var listItem = new List<ListItem>();
            foreach (var item in notification.ShoppingList.ShoppingListItems)
            {
                listItem.Add(new ListItem()
                {
                    Description = item.Description,
                    Quantity = item.Quantity
                });
            }
            var eventMessage = new ShoppingListCreatedIntegrationEvent(notification.ShoppingList.Title, listItem);
            _shoppingListIntegrationService.PublishThroughEventBusAsync(eventMessage);
            return Task.CompletedTask;
        }
    }
}
