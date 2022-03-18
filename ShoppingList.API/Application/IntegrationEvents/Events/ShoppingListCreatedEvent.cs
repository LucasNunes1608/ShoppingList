using EventBus.Events;

namespace ShoppingList.API.Application.IntegrationEvents.Events
{
    public record ShoppingListCreatedEvent : IntegrationEvent
    {
        public ShoppingListCreatedEvent(string shoppingListTitle, List<ListItem> listItems)
        {
            ShoppingListTitle = shoppingListTitle;
            ListItems = listItems;
        }

        public string ShoppingListTitle { get; set; }
        public List<ListItem> ListItems { get; set; }
    }
}
