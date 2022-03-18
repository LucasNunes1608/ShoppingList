namespace ShoppingList.API.Application.IntegrationEvents
{
    public interface IShoppingListIntegrationService
    {
        Task PublishThroughEventBusAsync(Guid messageId);
    }
}
