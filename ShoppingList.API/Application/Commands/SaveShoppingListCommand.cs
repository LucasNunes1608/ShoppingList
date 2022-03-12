using MediatR;
using ShoppingList.API.Extensions;
using ShoppingList.API.Models;

namespace ShoppingList.API.Application.Commands
{
    public class SaveShoppingListCommand : IRequest<bool>
    {
        public string Title { get; private set; }
        public IEnumerable<ShoppingListItemDTO> ShoppingListItems { get; private set; }

        public SaveShoppingListCommand(string title, IEnumerable<ShoppingListItemDTO> shoppingListItems) 
        {
            Title = title;
            ShoppingListItems = shoppingListItems;
        }

       
    }

    public record ShoppingListItemDTO
    {
        public string Description { get; init; }
        public int Quantity { get; init; }
    }
}
