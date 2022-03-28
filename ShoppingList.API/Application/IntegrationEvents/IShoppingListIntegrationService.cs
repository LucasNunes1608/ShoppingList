using EventBus.Events;

namespace ShoppingList.API.Application.IntegrationEvents
{
    public interface IShoppingListIntegrationService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent @event);
    }
}
