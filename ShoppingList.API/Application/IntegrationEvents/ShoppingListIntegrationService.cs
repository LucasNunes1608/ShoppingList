namespace ShoppingList.API.Application.IntegrationEvents
{
    public class ShoppingListIntegrationService : IShoppingListIntegrationService
    {
        public Task PublishThroughEventBusAsync(Guid messageId)
        {
            throw new NotImplementedException();
        }
    }
}
