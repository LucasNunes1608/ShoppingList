using EventBus.Abstractions;
using EventBus.Events;

namespace ShoppingList.API.Application.IntegrationEvents
{
    public class ShoppingListIntegrationService : IShoppingListIntegrationService
    {
        private readonly IEventBus _eventBus;

        public ShoppingListIntegrationService(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public Task PublishThroughEventBusAsync(IntegrationEvent @event)
        {
            _eventBus.Publish(@event);
            return Task.CompletedTask;
        }
    }
}
