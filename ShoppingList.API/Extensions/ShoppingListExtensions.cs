using ShoppingList.API.Application.Commands;
using ShoppingList.API.Models;
namespace ShoppingList.API.Extensions
{
    public static class ShoppingListExtensions
    {
        public static IEnumerable<ShoppingListItemDTO> ToShoppingListItemsDTO(this IEnumerable<ShoppingListItem> Items)
        {
            foreach (var item in Items)
            {
                yield return item.ToShoppingListItemDTO();
            }
        }
        public static ShoppingListItemDTO ToShoppingListItemDTO(this ShoppingListItem item)
        {
            return new ShoppingListItemDTO()
            {
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                Quantity = item.Quantity
            };
        }
    }
}
