using ShoppingList.API.Application.Commands;
using ShoppingList.Domain.AggregatesModel;
namespace ShoppingList.API.Extensions
{
    public static class ShoppingListExtensions
    {
        public static IEnumerable<ShoppingListItem> ToShoppingListItems(this IEnumerable<UpdateShoppingListCommand.UpdateShoppingListItemDTO> Items)
        {
            foreach (var item in Items)
            {
                yield return item.ToShoppingListItem();
            }
        }
        public static ShoppingListItem ToShoppingListItem(this UpdateShoppingListCommand.UpdateShoppingListItemDTO item)
        {
            var shoppingListItem = new ShoppingListItem(item.Description, item.Quantity, item.IsCompleted);
            return shoppingListItem;
        }
    }
}
