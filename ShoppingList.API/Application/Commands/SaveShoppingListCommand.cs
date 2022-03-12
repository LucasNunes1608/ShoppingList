using MediatR;
using ShoppingList.API.Extensions;
using ShoppingList.API.Models;

namespace ShoppingList.API.Application.Commands
{
    public class SaveShoppingListCommand : IRequest<bool>
    {
        public string Title { get; private set; }
        private readonly List<ShoppingListItemDTO> _shoppingListItems;
        public IEnumerable<ShoppingListItemDTO> ShoppingListItems => _shoppingListItems;

        public SaveShoppingListCommand()
        {
            _shoppingListItems = new List<ShoppingListItemDTO>();
        }

        public SaveShoppingListCommand(string title, List<ShoppingListItemDTO> shoppingListItems) : this()
        {
            Title = title;
            _shoppingListItems = shoppingListItems;//shoppingListItems.ToShoppingListItemsDTO().ToList();
        }

       
    }

    public record ShoppingListItemDTO
    {
        public int Id { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }
        public bool IsCompleted { get; init; }
    }
}
