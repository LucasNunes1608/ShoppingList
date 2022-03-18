using EventBus.Abstractions;
using MediatR;
using ShoppingList.API.Application.IntegrationEvents.Events;
using ShoppingList.Domain.Events;

namespace ShoppingList.API.Application.IntegrationEvents.EventHandling.ShoppingListCreated
{
    public class PublishToEventBus : INotificationHandler<ShoppingListCreatedDomainEvent>
    {
        private readonly IEventBus _eventBus;

        public PublishToEventBus(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
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
            var eventMessage = new ShoppingListCreatedEvent(notification.ShoppingList.Title, listItem);
            _eventBus.Publish(eventMessage);
            return Task.CompletedTask;
        }
    }
}
