using EventBus.Events;

namespace ShoppingList.API.Application.IntegrationEvents.Events
{
    public record ShoppingListCreatedIntegrationEvent : IntegrationEvent
    {
        public ShoppingListCreatedIntegrationEvent(string shoppingListTitle, List<ListItem> listItems)
        {
            ShoppingListTitle = shoppingListTitle;
            ListItems = listItems;
        }

        public string ShoppingListTitle { get; set; }
        public List<ListItem> ListItems { get; set; }
    }
}
